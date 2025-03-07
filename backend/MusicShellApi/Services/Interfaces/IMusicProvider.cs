using System.Collections.Generic;
using MusicShellApi.Data.Models;

namespace MusicShellApi.Services.Interfaces
{
    /// <summary>
    /// Interface for music providers.
    /// </summary>
    public interface IMusicProvider
    {
        /// <summary>
        /// Gets the playlist of songs.
        /// </summary>
        /// <returns>A list of SongInfo objects representing the playlist.</returns>
        List<SongInfo> GetPlaylist();

        /// <summary>
        /// Gets information about a specific song by index.
        /// </summary>
        /// <param name="index">The index of the song in the playlist.</param>
        /// <returns>A SongInfo object representing the song information.</returns>
        SongInfo GetSongInfo(int index);
    }
}
