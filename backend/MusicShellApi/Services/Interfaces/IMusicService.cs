using System.Collections.Generic;
using MusicShellApi.Data.Dtos;
using MusicShellApi.Data.Models;

namespace MusicShellApi.Services.Interfaces
{
    /// <summary>
    /// Interface for music service, providing methods to control music playback and fetch song information.
    /// </summary>
    public interface IMusicService
    {
        /// <summary>
        /// Play the currently selected song.
        /// </summary>
        /// <returns>Message indicating the playback status.</returns>
        string Play();

        /// <summary>
        /// Pause the currently playing song.
        /// </summary>
        /// <returns>Message indicating the pause status.</returns>
        string Pause();

        /// <summary>
        /// Stop the currently playing song.
        /// </summary>
        /// <returns>Message indicating the stop status.</returns>
        string Stop();

        /// <summary>
        /// Play the next song in the playlist.
        /// </summary>
        /// <returns>Message indicating the next song status.</returns>
        string Next();

        /// <summary>
        /// Play the previous song in the playlist.
        /// </summary>
        /// <returns>Message indicating the previous song status.</returns>
        string Previous();

        /// <summary>
        /// List all songs in the playlist.
        /// </summary>
        /// <returns>String listing all songs.</returns>
        List<string> ListSongs();

        /// <summary>
        /// Get information about a specific song by index.
        /// </summary>
        /// <param name="index">Index of the song.</param>
        /// <returns>SongInfo object.</returns>
        SongInfo GetSongInfo(int index);

        /// <summary>
        /// Get information about all songs in the playlist.
        /// </summary>
        /// <returns>List of SongInfo objects.</returns>
        List<SongInfo> GetAllSongInfos();

        /// <summary>
        /// Get information about the currently playing song.
        /// </summary>
        /// <returns>SongInfo object for the currently playing song.</returns>
        SongInfo GetCurrentSong();

        /// <summary>
        /// Get information about all songs in the playlist as DTOs.
        /// </summary>
        /// <returns>List of SongInfoDto objects.</returns>
        List<SongInfoDto> GetAllSongInfoDtos();
    }
}
