using MusicShellApi.Services.Interfaces;
using System.Collections.Generic;
using MusicShellApi.Mediators.Interfaces;
using MusicShellApi.Data.Models;

namespace MusicShellApi.Mediators.Implementation
{
    /// <summary>
    /// Mediates interactions between the API controllers and the music service.
    /// </summary>
    public class MusicMediator : IMusicMediator
    {
        private readonly IMusicService _musicService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicMediator"/> class.
        /// </summary>
        /// <param name="musicService">The music service to control music playback and fetch song information.</param>
        public MusicMediator(IMusicService musicService)
        {
            _musicService = musicService;
        }

        /// <summary>
        /// Play the currently selected song.
        /// </summary>
        /// <returns>Message indicating the playback status.</returns>
        public string Play()
        {
            return _musicService.Play();
        }

        /// <summary>
        /// Pause the currently playing song.
        /// </summary>
        /// <returns>Message indicating the pause status.</returns>
        public string Pause()
        {
            return _musicService.Pause();
        }

        /// <summary>
        /// Stop the currently playing song.
        /// </summary>
        /// <returns>Message indicating the stop status.</returns>
        public string Stop()
        {
            return _musicService.Stop();
        }

        /// <summary>
        /// Play the next song in the playlist.
        /// </summary>
        /// <returns>Message indicating the next song status.</returns>
        public string Next()
        {
            return _musicService.Next();
        }

        /// <summary>
        /// Play the previous song in the playlist.
        /// </summary>
        /// <returns>Message indicating the previous song status.</returns>
        public string Previous()
        {
            return _musicService.Previous();
        }

        /// <summary>
        /// List all songs in the playlist.
        /// </summary>
        /// <returns>String listing all songs.</returns>
        public List<string> List()
        {
            return _musicService.ListSongs();
        }

        /// <summary>
        /// Get information about a specific song by index.
        /// </summary>
        /// <param name="index">Index of the song.</param>
        /// <returns>SongInfo object.</returns>
        public SongInfo GetSongInfo(int index)
        {
            return _musicService.GetSongInfo(index);
        }

        /// <summary>
        /// Get information about all songs in the playlist.
        /// </summary>
        /// <returns>List of SongInfo objects.</returns>
        public List<SongInfo> GetAllSongInfos()
        {
            return _musicService.GetAllSongInfos();
        }

        /// <summary>
        /// Get information about the currently playing song.
        /// </summary>
        /// <returns>SongInfo object for the currently playing song.</returns>
        public SongInfo GetCurrentSong()
        {
            return _musicService.GetCurrentSong();
        }
    }
}
