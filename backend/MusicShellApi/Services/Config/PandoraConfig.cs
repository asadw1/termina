namespace MusicShellApi.Services.Config
{
    /// <summary>
    /// Configuration settings for Pandora.
    /// </summary>
    public class PandoraConfig
    {
        /// <summary>
        /// Gets or sets the API key for Pandora.
        /// </summary>
        public required string ApiKey { get; set; }

        /// <summary>
        /// Gets or sets the secret key for Pandora.
        /// </summary>
        public required string Secret { get; set; }
    }
}
