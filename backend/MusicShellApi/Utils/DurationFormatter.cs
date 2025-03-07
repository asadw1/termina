namespace MusicShellApi.Utils
{
    /// <summary>
    /// Provides utility methods for formatting song durations.
    /// </summary>
    public static class DurationFormatter
    {
        /// <summary>
        /// Formats the duration as "mm:ss".
        /// </summary>
        /// <param name="duration">The duration to format.</param>
        /// <returns>The formatted duration as a string.</returns>
        public static string Format(TimeSpan duration)
        {
            return $"{duration.Minutes:D2}:{duration.Seconds:D2}";
        }
    }
}
