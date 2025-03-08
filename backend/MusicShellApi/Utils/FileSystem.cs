using MusicShellApi.Services.Interfaces;

namespace MusicShellApi.Utils
{
    /// <summary>
    /// Implementation of IFileSystem for accessing the file system.
    /// </summary>
    public class FileSystem : IFileSystem
    {
        /// <summary>
        /// Checks if the specified directory exists.
        /// </summary>
        /// <param name="path">The path to the directory.</param>
        /// <returns><c>true</c> if the directory exists; otherwise, <c>false</c>.</returns>
        public bool DirectoryExists(string path)
        {
            try
            {
                return Directory.Exists(path);
            }
            catch (ArgumentException ex)
            {
                // Handle invalid path
                Console.WriteLine($"Invalid path: {ex.Message}");
                return false;
            }
            catch (UnauthorizedAccessException ex)
            {
                // Handle access denied
                Console.WriteLine($"Access denied: {ex.Message}");
                return false;
            }
            catch (IOException ex)
            {
                // Handle I/O errors
                Console.WriteLine($"I/O error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Gets the files in the specified directory that match the search pattern.
        /// </summary>
        /// <param name="path">The path to the directory.</param>
        /// <param name="searchPattern">The search pattern to match against the names of files in the path.</param>
        /// <returns>An array of file names that match the search pattern.</returns>
        public string[] GetFiles(string path, string searchPattern)
        {
            try
            {
                return Directory.GetFiles(path, searchPattern);
            }
            catch (DirectoryNotFoundException ex)
            {
                // Handle directory not found
                Console.WriteLine($"Directory not found: {ex.Message}");
                return Array.Empty<string>();
            }
            catch (ArgumentException ex)
            {
                // Handle invalid path or search pattern
                Console.WriteLine($"Invalid path or search pattern: {ex.Message}");
                return Array.Empty<string>();
            }
            catch (UnauthorizedAccessException ex)
            {
                // Handle access denied
                Console.WriteLine($"Access denied: {ex.Message}");
                return Array.Empty<string>();
            }
            catch (IOException ex)
            {
                // Handle I/O errors
                Console.WriteLine($"I/O error: {ex.Message}");
                return Array.Empty<string>();
            }
        }
    }
}
