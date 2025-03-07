namespace MusicShellApi.Services.Config
{
    /// <summary>
    /// Configuration settings for Spotify.
    /// </summary>
    public class SpotifyConfig
    {
        /// <summary>
        /// Gets or sets the client ID for Spotify.
        /// </summary>
        public required string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the client secret for Spotify.
        /// </summary>
        public required string ClientSecret { get; set; }

        /// <summary>
        /// Gets or sets the redirect URI for Spotify.
        /// </summary>
        public required string RedirectUri { get; set; }
    }
}
