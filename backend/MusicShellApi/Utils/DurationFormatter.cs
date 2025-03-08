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
        /// <exception cref="ArgumentException">Thrown when the duration is negative.</exception>
        public static string Format(TimeSpan duration)
        {
            try
            {
                // Validate that the duration is not negative
                if (duration < TimeSpan.Zero)
                {
                    throw new ArgumentException("Duration cannot be negative.");
                }

                return $"{duration.Minutes:D2}:{duration.Seconds:D2}";
            }
            catch (ArgumentException ex)
            {
                // Handle invalid duration
                Console.WriteLine($"Error formatting duration: {ex.Message}");
                return "00:00"; // Return a default value
            }
        }
    }
}
