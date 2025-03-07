namespace MusicShellApi.Services.Interfaces
{
    /// <summary>
    /// Interface for file system operations.
    /// </summary>
    public interface IFileSystem
    {
        /// <summary>
        /// Checks if the specified directory exists.
        /// </summary>
        /// <param name="path">The path to the directory.</param>
        /// <returns><c>true</c> if the directory exists; otherwise, <c>false</c>.</returns>
        bool DirectoryExists(string path);

        /// <summary>
        /// Gets the files in the specified directory that match the search pattern.
        /// </summary>
        /// <param name="path">The path to the directory.</param>
        /// <param name="searchPattern">The search pattern to match against the names of files in the path.</param>
        /// <returns>An array of file names that match the search pattern.</returns>
        string[] GetFiles(string path, string searchPattern);
    }
}
