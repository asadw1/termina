using MusicShellApi.Services.Interfaces;
using MusicShellApi.Services.Providers;
using MusicShellApi.Utils;

namespace MusicShellApi.Services.Factories
{
    public class LocalMusicProviderFactory : IMusicProviderFactory
    {
        private readonly IFileSystem fileSystem;
        private readonly string musicFilesPath;

        public LocalMusicProviderFactory(IFileSystem fileSystem, string musicFilesPath)
        {
            this.fileSystem = fileSystem;
            this.musicFilesPath = musicFilesPath;
        }

        public IMusicProvider CreateMusicProvider()
        {
            return new LocalMusicProvider(fileSystem, musicFilesPath);
        }
    }
}
