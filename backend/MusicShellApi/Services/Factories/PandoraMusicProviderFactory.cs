using MusicShellApi.Services.Config;
using MusicShellApi.Services.Interfaces;

namespace MusicShellApi.Services.Factories
{
    public class PandoraMusicProviderFactory : IMusicProviderFactory
    {
        private readonly PandoraConfig pandoraConfig;

        public PandoraMusicProviderFactory(PandoraConfig pandoraConfig)
        {
            this.pandoraConfig = pandoraConfig;
        }

        public IMusicProvider CreateMusicProvider()
        {
            // Placeholder for creating a Pandora music provider
            throw new NotImplementedException("Pandora integration not implemented yet.");
        }
    }
}
