using Microsoft.AspNetCore.Mvc;
using MusicShellApi.Mediators.Interfaces;
using MusicShellApi.Data.Dtos;

namespace MusicShellApi.Controllers
{
    /// <summary>
    /// Controller for managing music playback and song information.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MusicController : ControllerBase
    {
        private readonly IMusicMediator _musicMediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicController"/> class.
        /// </summary>
        /// <param name="musicMediator">The music mediator to control music playback and fetch song information.</param>
        public MusicController(IMusicMediator musicMediator)
        {
            _musicMediator = musicMediator;
        }

        /// <summary>
        /// Get all songs in the playlist.
        /// </summary>
        /// <returns>List of SongInfoDto objects.</returns>
        [HttpGet("songs")]
        public ActionResult<List<SongInfoDto>> GetAllSongs()
        {
            try
            {
                var songs = _musicMediator.GetAllSongInfoDtos();
                return Ok(songs);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching songs: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Get a specific song by index.
        /// </summary>
        /// <param name="index">Index of the song.</param>
        /// <returns>SongInfoDto object.</returns>
        [HttpGet("songs/{index}")]
        public ActionResult<SongInfoDto> GetSong(int index)
        {
            try
            {
                var song = _musicMediator.GetSongInfo(index).ToDto();
                return Ok(song);
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
        /// Play the currently selected song.
        /// </summary>
        /// <returns>Message indicating the playback status.</returns>
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

        /// <summary>
        /// Pause the currently playing song.
        /// </summary>
        /// <returns>Message indicating the pause status.</returns>
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

        /// <summary>
        /// Stop the currently playing song.
        /// </summary>
        /// <returns>Message indicating the stop status.</returns>
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

        /// <summary>
        /// Play the next song in the playlist.
        /// </summary>
        /// <returns>Message indicating the next song status.</returns>
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

        /// <summary>
        /// Play the previous song in the playlist.
        /// </summary>
        /// <returns>Message indicating the previous song status.</returns>
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

        /// <summary>
        /// List all song titles in the playlist.
        /// </summary>
        /// <returns>JSON array of song titles.</returns>
        [HttpGet("list")]
        public ActionResult<List<string>> List()
        {
            try
            {
                var songTitles = _musicMediator.List();
                return Ok(songTitles);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error listing songs: {ex.Message}");
                return NotFound(ex.Message);
            }
            catch (ApplicationException ex)
            {
                Console.WriteLine($"Error listing songs: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }


        /// <summary>
        /// Get the currently playing song.
        /// </summary>
        /// <returns>SongInfoDto object for the currently playing song.</returns>
        [HttpGet("current")]
        public ActionResult<SongInfoDto> GetCurrentSong()
        {
            try
            {
                var song = _musicMediator.GetCurrentSong().ToDto();
                return Ok(song);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching currently playing song: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
