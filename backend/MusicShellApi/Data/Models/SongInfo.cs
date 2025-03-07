using System;
using MusicShellApi.Utils;
using MusicShellApi.Data.Dtos;

namespace MusicShellApi.Data.Models
{
    /// <summary>
    /// Represents information about a song.
    /// </summary>
    public class SongInfo
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
        /// Gets or sets the duration of the song.
        /// </summary>
        public TimeSpan Duration { get; set; } = TimeSpan.Zero;

        /// <summary>
        /// Gets or sets the artist of the song.
        /// </summary>
        public string Artist { get; set; } = "Unknown Artist";

        /// <summary>
        /// Gets the formatted duration of the song as "mm:ss".
        /// </summary>
        public string FormattedDuration => DurationFormatter.Format(Duration);

        /// <summary>
        /// Converts the SongInfo object to a SongInfoDto.
        /// </summary>
        /// <returns>A SongInfoDto object representing the song information.</returns>
        public SongInfoDto ToDto()
        {
            return new SongInfoDto
            {
                Title = Title,
                FilePath = FilePath,
                Duration = FormattedDuration,
                Artist = Artist
            };
        }
    }
}
