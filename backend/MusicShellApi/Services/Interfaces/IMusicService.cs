using MusicShellApi.Models;
using System.Collections.Generic;

namespace MusicShellApi.Services.Interfaces
{
    public interface IMusicService
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
