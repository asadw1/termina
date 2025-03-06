namespace MusicShellApi.Services.Config
{
    public class SpotifyConfig
    {
        public required string ClientId { get; set; }
        public required string ClientSecret { get; set; }
        public required string RedirectUri { get; set; }
    }
}
