namespace MusicShellApi.Models
{
    public class SongInfo
    {
        public string Title { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public TimeSpan Duration { get; set; } = TimeSpan.Zero;
        public string Artist { get; set; } = "Unknown Artist";
    }
}
