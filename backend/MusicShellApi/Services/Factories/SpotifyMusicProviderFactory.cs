using MusicShellApi.Services.Config;
using MusicShellApi.Services.Interfaces;

namespace MusicShellApi.Services.Factories
{
    public class SpotifyMusicProviderFactory : IMusicProviderFactory
    {
        private readonly SpotifyConfig spotifyConfig;

        public SpotifyMusicProviderFactory(SpotifyConfig spotifyConfig)
        {
            this.spotifyConfig = spotifyConfig;
        }

        public IMusicProvider CreateMusicProvider()
        {
            // Placeholder for creating a Spotify music provider
            throw new NotImplementedException("Spotify integration not implemented yet.");
        }
    }
}
