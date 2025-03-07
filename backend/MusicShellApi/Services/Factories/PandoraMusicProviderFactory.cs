using MusicShellApi.Services.Config;
using MusicShellApi.Services.Interfaces;

namespace MusicShellApi.Services.Factories
{
    /// <summary>
    /// Factory class for creating Pandora music providers.
    /// </summary>
    public class PandoraMusicProviderFactory : IMusicProviderFactory
    {
        private readonly PandoraConfig pandoraConfig;

        /// <summary>
        /// Initializes a new instance of the <see cref="PandoraMusicProviderFactory"/> class.
        /// </summary>
        /// <param name="pandoraConfig">The configuration settings for Pandora.</param>
        public PandoraMusicProviderFactory(PandoraConfig pandoraConfig)
        {
            this.pandoraConfig = pandoraConfig;
        }

        /// <summary>
        /// Creates and returns a new Pandora music provider.
        /// </summary>
        /// <returns>An instance of <see cref="IMusicProvider"/>.</returns>
        /// <exception cref="NotImplementedException">Thrown when Pandora integration is not implemented yet.</exception>
        public IMusicProvider CreateMusicProvider()
        {
            // Placeholder for creating a Pandora music provider
            throw new NotImplementedException("Pandora integration not implemented yet.");
        }
    }
}
