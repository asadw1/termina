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

        /// <summary>
        /// Retrieves information about all songs in the playlist.
        /// </summary>
        /// <returns>A list of SongInfo objects containing metadata for all songs.</returns>
        /// <response code="200">Returns the list of songs</response>
        /// <response code="500">If there is an internal server error</response>
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

        /// <summary>
        /// Retrieves information about a specific song by index.
        /// </summary>
        /// <param name="index">The zero-based index of the song.</param>
        /// <returns>A SongInfo object containing metadata for the specified song.</returns>
        /// <response code="200">Returns the song information</response>
        /// <response code="400">If the index is out of range</response>
        /// <response code="500">If there is an internal server error</response>
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

        /// <summary>
        /// Starts playing the current song.
        /// </summary>
        /// <returns>A string message indicating the current playing song.</returns>
        /// <response code="200">Returns a message indicating the song being played</response>
        /// <response code="500">If there is an internal server error</response>
        [HttpPost("play")]
        public ActionResult<string> Play()
        {
            try
            {
                return Ok(_musicMediator.Play());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing song: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Pauses the current song.
        /// </summary>
        /// <returns>A string message indicating the song is paused.</returns>
        /// <response code="200">Returns a message indicating the song is paused</response>
        /// <response code="500">If there is an internal server error</response>
        [HttpPost("pause")]
        public ActionResult<string> Pause()
        {
            try
            {
                return Ok(_musicMediator.Pause());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error pausing song: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Stops the current song.
        /// </summary>
        /// <returns>A string message indicating the song is stopped.</returns>
        /// <response code="200">Returns a message indicating the song is stopped</response>
        /// <response code="500">If there is an internal server error</response>
        [HttpPost("stop")]
        public ActionResult<string> Stop()
        {
            try
            {
                return Ok(_musicMediator.Stop());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error stopping song: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Plays the next song in the playlist.
        /// </summary>
        /// <returns>A string message indicating the next song being played.</returns>
        /// <response code="200">Returns a message indicating the next song being played</response>
        /// <response code="500">If there is an internal server error</response>
        [HttpPost("next")]
        public ActionResult<string> Next()
        {
            try
            {
                return Ok(_musicMediator.Next());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing next song: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Plays the previous song in the playlist.
        /// </summary>
        /// <returns>A string message indicating the previous song being played.</returns>
        /// <response code="200">Returns a message indicating the previous song being played</response>
        /// <response code="500">If there is an internal server error</response>
        [HttpPost("previous")]
        public ActionResult<string> Previous()
        {
            try
            {
                return Ok(_musicMediator.Previous());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing previous song: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Lists all songs in the playlist.
        /// </summary>
        /// <returns>A string message listing all available songs in the playlist.</returns>
        /// <response code="200">Returns a message listing all songs</response>
        /// <response code="500">If there is an internal server error</response>
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
