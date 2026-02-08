using Microsoft.Extensions.Logging;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MusicShellApi.Services.Interfaces;
using MusicShellApi.Data.Models;
using MusicShellApi.Data.Dtos;

namespace MusicShellApi.Services.Implementation
{
    /// <summary>
    /// Provides music playback services and fetches song information.
    /// </summary>
    public class MusicService : IMusicService, IDisposable
    {
        private readonly SemaphoreSlim _lock = new(1, 1);
        private IWavePlayer? _wavePlayer;
        private AudioFileReader? _audioFileReader;
        private volatile int _currentSongIndex;
        private readonly IMusicProvider _musicProvider;
        private readonly ILogger<MusicService> _logger;
        private bool _disposed;
        private CancellationTokenSource? _cts;
        private bool _isManualSkip;

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicService"/> class.
        /// </summary>
        /// <param name="musicProviderFactory">Factory to create the music provider.</param>
        /// <param name="logger">Logger for service operations.</param>
        public MusicService(IMusicProviderFactory musicProviderFactory, ILogger<MusicService> logger)
        {
            _musicProvider = musicProviderFactory.CreateMusicProvider();
            _logger = logger;
            _cts = new CancellationTokenSource();
            _wavePlayer = new WaveOutEvent();
            
            // FIXED: Auto-advance with proper cancellation & locking
            _wavePlayer.PlaybackStopped += OnPlaybackStopped;
            _currentSongIndex = 0;
        }

        private async void OnPlaybackStopped(object? sender, StoppedEventArgs args)
        {
            if (args.Exception != null || _disposed || _isManualSkip) return;

            try
            {
                await Task.Delay(200, _cts!.Token); // Grace period for pending ops
                
                await _lock.WaitAsync(TimeSpan.FromSeconds(1), _cts.Token);
                try
                {
                    if (_disposed || _cts.Token.IsCancellationRequested) return;
                    
                    var playlist = _musicProvider.GetPlaylist();
                    if (_currentSongIndex < playlist.Count - 1)
                    {
                        _currentSongIndex++;
                        _ = Task.Run(() => Play(), _cts.Token); // Fire-and-forget safe
                    }
                }
                finally { _lock.Release(); }
            }
            catch (OperationCanceledException) { }
            catch (Exception ex) { _logger.LogWarning(ex, "Auto-advance failed"); }
        }

        /// <summary>
        /// Plays the currently selected song.
        /// </summary>
        /// <returns>Message indicating the playback status.</returns>
        public string Play()
        {
            try
            {
                if (!_lock.Wait(TimeSpan.FromSeconds(2))) 
                    return "Playback locked - try again.";

                try
                {
                    CleanupPlayer(); // ALWAYS cleanup first
                    
                    var playlist = _musicProvider.GetPlaylist();
                    if (playlist.Count == 0) return "No songs available.";

                    if (_currentSongIndex >= playlist.Count)
                        _currentSongIndex = 0; // Safety reset

                    var song = playlist[_currentSongIndex];
                    _audioFileReader = new AudioFileReader(song.FilePath);
                    _wavePlayer!.Init(_audioFileReader);
                    _wavePlayer.Play();
                    
                    _logger.LogInformation("Playing: {Title} (Index: {Index})", song.Title, _currentSongIndex);
                    return $"Playing: {song.Title}";
                }
                finally { _lock.Release(); }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Play failed at index {Index}", _currentSongIndex);
                CleanupPlayer(); // Emergency cleanup
                return "Error playing song.";
            }
        }

        /// <summary>
        /// Pauses the currently playing song.
        /// </summary>
        /// <returns>Message indicating the pause status.</returns>
        public string Pause()
        {
            try
            {
                _wavePlayer?.Pause();
                return "Music paused.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Pause failed");
                return "Error pausing song.";
            }
        }

        /// <summary>
        /// Stops the currently playing song.
        /// </summary>
        /// <returns>Message indicating the stop status.</returns>
        public string Stop()
        {
            try
            {
                if (!_lock.Wait(TimeSpan.FromSeconds(1))) 
                    return "Stop locked - retrying.";

                try
                {
                    _wavePlayer?.Stop();
                    CleanupPlayer(); // FIXED: Full cleanup including wavePlayer
                    _logger.LogInformation("Music stopped");
                    return "Music stopped.";
                }
                finally { _lock.Release(); }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Stop failed");
                CleanupPlayer(); // Emergency cleanup
                return "Error stopping song.";
            }
        }

        /// <summary>
        /// Plays the next song in the playlist.
        /// </summary>
        /// <returns>Message indicating the next song status.</returns>
        public string Next()
        {
            try
            {
                _isManualSkip = true; // Flag to indicate user-initiated skip
                Stop(); // Chains to full cleanup
                var playlist = _musicProvider.GetPlaylist();
                if (playlist.Count == 0) return "No songs available.";
                
                _currentSongIndex = (_currentSongIndex + 1) % playlist.Count;

                var result = Play();
                _isManualSkip = false; // Reset flag after operation
                return result;
            }
            catch (Exception ex)
            {
                _isManualSkip = false; // Ensure flag reset on error
                _logger.LogError(ex, "Next failed");
                return "Error playing next song.";
            }
        }

        /// <summary>
        /// Plays the previous song in the playlist.
        /// </summary>
        /// <returns>Message indicating the previous song status.</returns>
        public string Previous()
        {
            try
            {
                Stop();
                var playlist = _musicProvider.GetPlaylist();
                if (playlist.Count == 0) return "No songs available.";
                
                _currentSongIndex = (_currentSongIndex - 1 + playlist.Count) % playlist.Count;
                return Play();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Previous failed");
                return "Error playing previous song.";
            }
        }

        /// <summary>
        /// Gets all song information in the playlist.
        /// </summary>
        /// <returns>List of SongInfo objects.</returns>
        public List<SongInfo> GetAllSongInfos()
        {
            return _musicProvider.GetPlaylist();
        }

        /// <summary>
        /// Gets song information by index.
        /// </summary>
        /// <param name="index">Index of the song.</param>
        /// <returns>SongInfo object.</returns>
        public SongInfo GetSongInfo(int index)
        {
            // COMPATIBLE: Controller expects ArgumentOutOfRangeException
            var playlist = _musicProvider.GetPlaylist();
            if (index < 0 || index >= playlist.Count)
                throw new ArgumentOutOfRangeException(nameof(index), $"Index {index} out of range (0-{playlist.Count-1})");
            return playlist[index];
        }

        /// <summary>
        /// Lists all song titles in the playlist.
        /// </summary>
        /// <returns>List of song titles.</returns>
        /// <exception cref="InvalidOperationException">Thrown when there are no songs available.</exception>
        /// <exception cref="ApplicationException">Thrown when an error occurs while listing songs.</exception>
        public List<string> ListSongs()
        {
            try
            {
                var playlist = _musicProvider.GetPlaylist();
                if (playlist.Count == 0)
                {
                    throw new InvalidOperationException("No songs available.");
                }
                return [.. playlist.Select(song => song.Title)];
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ListSongs failed");
                throw new ApplicationException("Error listing songs.", ex);
            }
        }

        /// <summary>
        /// Gets the currently playing song.
        /// </summary>
        /// <returns>SongInfo object for the currently playing song.</returns>
        /// <exception cref="InvalidOperationException">Thrown when there are no songs in the playlist.</exception>
        public SongInfo GetCurrentSong()
        {
            var playlist = _musicProvider.GetPlaylist();
            if (playlist.Count == 0)
            {
                throw new InvalidOperationException("No songs available in the playlist.");
            }
            // Check if the player is actually initialized and not stopped
            if (_wavePlayer == null || _wavePlayer.PlaybackState == PlaybackState.Stopped)
            {
                // Throwing here allows your Controller to return a 404 or 204
                throw new InvalidOperationException("No song is currently playing or paused.");
            }
            if (_currentSongIndex < 0 || _currentSongIndex >= playlist.Count)
            {
                throw new InvalidOperationException("No song currently selected.");
            }
            return playlist[_currentSongIndex];
        }

        /// <summary>
        /// Get information about all songs in the playlist as DTOs.
        /// </summary>
        /// <returns>List of SongInfoDto objects.</returns>
        public List<SongInfoDto> GetAllSongInfoDtos()
        {
            var songs = GetAllSongInfos();
            var songsDto = new List<SongInfoDto>();

            foreach (var song in songs)
            {
                songsDto.Add(song.ToDto());
            }

            return songsDto;
        }

        /// <summary>
        /// Cleans up audio resources to prevent memory leaks and ensure proper disposal of NAudio components.
        /// </summary>
        private void CleanupPlayer()
        {
            try
            {
                _audioFileReader?.Dispose();
                _audioFileReader = null;
                
                // FIXED: WaveOutEvent disposal (critical leak)
                if (_wavePlayer is not null)
                {
                    _wavePlayer.Dispose();
                    _wavePlayer = new WaveOutEvent(); // Fresh instance
                    _wavePlayer.PlaybackStopped += OnPlaybackStopped; // Re-hook
                }
            }
            catch (Exception ex) 
            { 
                _logger.LogWarning(ex, "Cleanup warning"); 
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (_disposed) return;
            
            _lock.Wait(TimeSpan.FromSeconds(3));
            try
            {
                _cts?.Cancel();
                _cts?.Dispose();
                _cts = null;

                CleanupPlayer();
                _lock.Dispose();
                _disposed = true;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Dispose warning");
            }
            finally 
            { 
                if (_lock is not null && _lock.Wait(0)) 
                    _lock.Release(); 
            }
        }
    }
}
