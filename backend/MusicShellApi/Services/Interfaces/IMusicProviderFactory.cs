namespace MusicShellApi.Services.Interfaces
{
    /// <summary>
    /// Factory interface for creating music providers.
    /// </summary>
    public interface IMusicProviderFactory
    {
        /// <summary>
        /// Creates and returns a new music provider.
        /// </summary>
        /// <returns>An instance of <see cref="IMusicProvider"/>.</returns>
        IMusicProvider CreateMusicProvider();
    }
}
