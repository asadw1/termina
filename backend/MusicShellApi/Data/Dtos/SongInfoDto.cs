namespace MusicShellApi.Data.Dtos
{
    /// <summary>
    /// Represents the data to be returned in the API response for song information.
    /// </summary>
    public class SongInfoDto
    {
        /// <summary>
        /// Gets or sets the title of the song.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the file path of the song.
        /// </summary>
        public string FilePath { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the formatted duration of the song as "mm:ss".
        /// </summary>
        public string Duration { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the artist of the song.
        /// </summary>
        public string Artist { get; set; } = "Unknown Artist";
    }
}
