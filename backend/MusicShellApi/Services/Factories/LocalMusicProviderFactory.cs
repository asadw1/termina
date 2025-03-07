using MusicShellApi.Services.Interfaces;
using MusicShellApi.Services.Providers;
using MusicShellApi.Utils;

namespace MusicShellApi.Services.Factories
{
    /// <summary>
    /// Factory class for creating local music providers.
    /// </summary>
    public class LocalMusicProviderFactory : IMusicProviderFactory
    {
        private readonly IFileSystem fileSystem;
        private readonly string musicFilesPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalMusicProviderFactory"/> class.
        /// </summary>
        /// <param name="fileSystem">The file system interface for accessing music files.</param>
        /// <param name="musicFilesPath">The path to the music files directory.</param>
        public LocalMusicProviderFactory(IFileSystem fileSystem, string musicFilesPath)
        {
            this.fileSystem = fileSystem;
            this.musicFilesPath = musicFilesPath;
        }

        /// <summary>
        /// Creates and returns a new local music provider.
        /// </summary>
        /// <returns>An instance of <see cref="LocalMusicProvider"/>.</returns>
        public IMusicProvider CreateMusicProvider()
        {
            return new LocalMusicProvider(fileSystem, musicFilesPath);
        }
    }
}
