using MusicShellApi.Data.Models;
using MusicShellApi.Services.Interfaces;
using System.Collections.Generic;
using System.IO;
using TagLib;

namespace MusicShellApi.Services.Providers
{
    /// <summary>
    /// Provides local music playback functionality.
    /// </summary>
    public class LocalMusicProvider : IMusicProvider
    {
        private readonly IFileSystem fileSystem;
        private readonly List<string> playlist = new List<string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalMusicProvider"/> class.
        /// </summary>
        /// <param name="fileSystem">The file system interface for accessing music files.</param>
        /// <param name="musicFilesPath">The path to the music files directory.</param>
        public LocalMusicProvider(IFileSystem fileSystem, string musicFilesPath)
        {
            this.fileSystem = fileSystem;

            if (!string.IsNullOrEmpty(musicFilesPath))
            {
                LoadPlaylist(musicFilesPath);
            }
        }

        /// <summary>
        /// Loads the playlist from the specified folder path.
        /// </summary>
        /// <param name="folderPath">The path to the folder containing music files.</param>
        private void LoadPlaylist(string folderPath)
        {
            if (fileSystem.DirectoryExists(folderPath))
            {
                var files = fileSystem.GetFiles(folderPath, "*.mp3");
                playlist.AddRange(files);
            }
        }

        /// <summary>
        /// Gets the playlist of songs.
        /// </summary>
        /// <returns>A list of SongInfo objects representing the playlist.</returns>
        public List<SongInfo> GetPlaylist()
        {
            var songInfos = new List<SongInfo>();
            for (int i = 0; i < playlist.Count; i++)
            {
                songInfos.Add(GetSongInfo(i));
            }
            return songInfos;
        }

        /// <summary>
        /// Gets information about a specific song by index.
        /// </summary>
        /// <param name="index">The index of the song in the playlist.</param>
        /// <returns>A SongInfo object representing the song information.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the index is out of range.</exception>
        public SongInfo GetSongInfo(int index)
        {
            if (index < 0 || index >= playlist.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
            }

            var filePath = playlist[index];
            using (var tagFile = TagLib.File.Create(filePath))
            {
                return new SongInfo
                {
                    Title = tagFile.Tag.Title ?? Path.GetFileNameWithoutExtension(filePath),
                    FilePath = filePath,
                    Duration = tagFile.Properties.Duration,
                    Artist = tagFile.Tag.FirstPerformer ?? "Unknown Artist"
                };
            }
        }
    }
}
