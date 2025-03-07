using MusicShellApi.Services.Config;
using MusicShellApi.Services.Interfaces;

namespace MusicShellApi.Services.Factories
{
    /// <summary>
    /// Factory class for creating Spotify music providers.
    /// </summary>
    public class SpotifyMusicProviderFactory : IMusicProviderFactory
    {
        private readonly SpotifyConfig spotifyConfig;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyMusicProviderFactory"/> class.
        /// </summary>
        /// <param name="spotifyConfig">The configuration settings for Spotify.</param>
        public SpotifyMusicProviderFactory(SpotifyConfig spotifyConfig)
        {
            this.spotifyConfig = spotifyConfig;
        }

        /// <summary>
        /// Creates and returns a new Spotify music provider.
        /// </summary>
        /// <returns>An instance of <see cref="IMusicProvider"/>.</returns>
        /// <exception cref="NotImplementedException">Thrown when Spotify integration is not implemented yet.</exception>
        public IMusicProvider CreateMusicProvider()
        {
            // Placeholder for creating a Spotify music provider
            throw new NotImplementedException("Spotify integration not implemented yet.");
        }
    }
}
