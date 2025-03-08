using Microsoft.Extensions.Configuration;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using MusicShellApi.Services.Interfaces;
using MusicShellApi.Data.Models;
using MusicShellApi.Data.Dtos;

namespace MusicShellApi.Services.Implementation
{
    /// <summary>
    /// Provides music playback services and fetches song information.
    /// </summary>
    public class MusicService : IMusicService
    {
        private IWavePlayer wavePlayer;
        private AudioFileReader? audioFileReader;
        private int currentSongIndex;
        private readonly IMusicProvider musicProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicService"/> class.
        /// </summary>
        /// <param name="musicProviderFactory">Factory to create the music provider.</param>
        public MusicService(IMusicProviderFactory musicProviderFactory)
        {
            this.musicProvider = musicProviderFactory.CreateMusicProvider();
            wavePlayer = new WaveOutEvent();
            currentSongIndex = 0;
            audioFileReader = null;
        }

        /// <summary>
        /// Gets all song information in the playlist.
        /// </summary>
        /// <returns>List of SongInfo objects.</returns>
        public List<SongInfo> GetAllSongInfos()
        {
            return musicProvider.GetPlaylist();
        }

        /// <summary>
        /// Gets song information by index.
        /// </summary>
        /// <param name="index">Index of the song.</param>
        /// <returns>SongInfo object.</returns>
        public SongInfo GetSongInfo(int index)
        {
            return musicProvider.GetSongInfo(index);
        }

        /// <summary>
        /// Plays the currently selected song.
        /// </summary>
        /// <returns>Message indicating the playback status.</returns>
        public string Play()
        {
            try
            {
                var playlist = musicProvider.GetPlaylist();
                if (playlist.Count == 0)
                {
                    return "No songs available in the playlist.";
                }

                if (audioFileReader == null)
                {
                    audioFileReader = new AudioFileReader(playlist[currentSongIndex].FilePath);
                    wavePlayer.Init(audioFileReader);
                }

                wavePlayer.Play();
                return $"Playing: {playlist[currentSongIndex].Title}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing song: {ex.Message}");
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
                wavePlayer.Pause();
                return "Music paused.";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error pausing song: {ex.Message}");
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
                wavePlayer.Stop();
                audioFileReader?.Dispose();
                audioFileReader = null;
                return "Music stopped.";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error stopping song: {ex.Message}");
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
                Stop();
                var playlist = musicProvider.GetPlaylist();
                currentSongIndex = (currentSongIndex + 1) % playlist.Count;
                return Play();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing next song: {ex.Message}");
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
                var playlist = musicProvider.GetPlaylist();
                currentSongIndex = (currentSongIndex - 1 + playlist.Count) % playlist.Count;
                return Play();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing previous song: {ex.Message}");
                return "Error playing previous song.";
            }
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
                var playlist = musicProvider.GetPlaylist();
                if (playlist.Count == 0)
                {
                    throw new InvalidOperationException("No songs available.");
                }
                return [.. playlist.Select(song => song.Title)];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error listing songs: {ex.Message}");
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
            var playlist = musicProvider.GetPlaylist();
            if (playlist.Count == 0)
            {
                throw new InvalidOperationException("No songs available in the playlist.");
            }

            return playlist[currentSongIndex];
        }
        /// <summary>
        /// Get information about all songs in the playlist as DTOs.
        /// </summary>
        /// <returns>List of SongInfoDto objects.</returns>
        public List<SongInfoDto> GetAllSongInfoDtos()
        {
            var songs = GetAllSongInfos(); // Assuming this method already exists
            var songsDto = new List<SongInfoDto>();

            foreach (var song in songs)
            {
                songsDto.Add(song.ToDto());
            }

            return songsDto;
        }
    }
}
