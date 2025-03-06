using MusicShellApi.Models;
using System.Collections.Generic;

namespace MusicShellApi.Services.Interfaces
{
    public interface IMusicProvider
    {
        List<SongInfo> GetPlaylist();
        SongInfo GetSongInfo(int index);
    }
}
