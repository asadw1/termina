using MusicShellApi.Models;
using MusicShellApi.Services.Interfaces;
using System.Collections.Generic;
using System.IO;
using TagLib;

namespace MusicShellApi.Services.Providers
{
    public class LocalMusicProvider : IMusicProvider
    {
        private readonly IFileSystem fileSystem;
        private readonly List<string> playlist = new List<string>();

        public LocalMusicProvider(IFileSystem fileSystem, string musicFilesPath)
        {
            this.fileSystem = fileSystem;

            if (!string.IsNullOrEmpty(musicFilesPath))
            {
                LoadPlaylist(musicFilesPath);
            }
        }

        private void LoadPlaylist(string folderPath)
        {
            if (fileSystem.DirectoryExists(folderPath))
            {
                var files = fileSystem.GetFiles(folderPath, "*.mp3");
                playlist.AddRange(files);
            }
        }

        public List<SongInfo> GetPlaylist()
        {
            var songInfos = new List<SongInfo>();
            for (int i = 0; i < playlist.Count; i++)
            {
                songInfos.Add(GetSongInfo(i));
            }
            return songInfos;
        }

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
