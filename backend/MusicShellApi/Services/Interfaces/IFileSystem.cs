namespace MusicShellApi.Services.Interfaces
{
    public interface IFileSystem
    {
        bool DirectoryExists(string path);
        string[] GetFiles(string path, string searchPattern);
    }
}
