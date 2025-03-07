using Microsoft.AspNetCore.Mvc;
using MusicShellApi.Models;
using MusicShellApi.Mediators.Interfaces;
using System;
using System.Collections.Generic;

namespace MusicShellApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MusicController : ControllerBase
    {
        private readonly IMusicMediator _musicMediator;

        public MusicController(IMusicMediator musicMediator)
        {
            _musicMediator = musicMediator;
        }

        [HttpGet("songs")]
        public ActionResult<List<SongInfo>> GetAllSongs()
        {
            try
            {
                return Ok(_musicMediator.GetAllSongInfos());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching songs: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("songs/{index}")]
        public ActionResult<SongInfo> GetSong(int index)
        {
            try
            {
                return Ok(_musicMediator.GetSongInfo(index));
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Error fetching song: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching song: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("play")]
        public ActionResult<string> Play()
        {
            try
            {
                var message = _musicMediator.Play();
                return Ok(new { message = message });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing song: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("pause")]
        public ActionResult<string> Pause()
        {
            try
            {
                var message = _musicMediator.Pause();
                return Ok(new { message = message });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error pausing song: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("stop")]
        public ActionResult<string> Stop()
        {
            try
            {
                var message = _musicMediator.Stop();
                return Ok(new { message = message });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error stopping song: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("next")]
        public ActionResult<string> Next()
        {
            try
            {
                var message = _musicMediator.Next();
                return Ok(new { message = message });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing next song: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("previous")]
        public ActionResult<string> Previous()
        {
            try
            {
                var message = _musicMediator.Previous();
                return Ok(new { message = message });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing previous song: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("list")]
        public ActionResult<string> List()
        {
            try
            {
                return Ok(_musicMediator.List());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error listing songs: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
