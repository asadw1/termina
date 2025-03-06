using MusicShellApi.Models;

namespace MusicShellApi.Mediators.Interfaces
{
    public interface IMusicMediator
    {
        string Play();
        string Pause();
        string Stop();
        string Next();
        string Previous();
        string List();
        SongInfo GetSongInfo(int index);
        List<SongInfo> GetAllSongInfos();
    }
}
