using MusicShellApi.Services.Interfaces;
using MusicShellApi.Models;
using System.Collections.Generic;
using MusicShellApi.Mediators.Interfaces;

namespace MusicShellApi.Mediators.Implementation
{
    public class MusicMediator : IMusicMediator
    {
        private readonly IMusicService _musicService;

        public MusicMediator(IMusicService musicService)
        {
            _musicService = musicService;
        }

        public string Play()
        {
            return _musicService.Play();
        }

        public string Pause()
        {
            return _musicService.Pause();
        }

        public string Stop()
        {
            return _musicService.Stop();
        }

        public string Next()
        {
            return _musicService.Next();
        }

        public string Previous()
        {
            return _musicService.Previous();
        }

        public string List()
        {
            return _musicService.List();
        }

        public SongInfo GetSongInfo(int index)
        {
            return _musicService.GetSongInfo(index);
        }

        public List<SongInfo> GetAllSongInfos()
        {
            return _musicService.GetAllSongInfos();
        }
    }
}
