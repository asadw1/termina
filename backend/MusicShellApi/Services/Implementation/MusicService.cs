using Microsoft.Extensions.Configuration;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using MusicShellApi.Models;
using MusicShellApi.Services.Interfaces;

namespace MusicShellApi.Services.Implementation
{
    public class MusicService : IMusicService
    {
        private IWavePlayer wavePlayer;
        private AudioFileReader? audioFileReader;
        private int currentSongIndex;
        private readonly IMusicProvider musicProvider;

        public MusicService(IMusicProviderFactory musicProviderFactory)
        {
            this.musicProvider = musicProviderFactory.CreateMusicProvider();
            wavePlayer = new WaveOutEvent();
            currentSongIndex = 0;
            audioFileReader = null;
        }

        public List<SongInfo> GetAllSongInfos()
        {
            return musicProvider.GetPlaylist();
        }

        public SongInfo GetSongInfo(int index)
        {
            return musicProvider.GetSongInfo(index);
        }

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

        public string List()
        {
            try
            {
                var playlist = musicProvider.GetPlaylist();
                return playlist.Count == 0 ? "No songs available." : $"Available songs: {string.Join(", ", playlist)}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error listing songs: {ex.Message}");
                return "Error listing songs.";
            }
        }
    }
}
