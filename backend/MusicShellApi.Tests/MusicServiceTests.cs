using Moq;
using MusicShellApi.Services;
using Xunit;

namespace MusicShellApi.Tests
{
    public class MusicServiceTests
    {
        private readonly Mock<IMusicService> _mockMusicService;
        private readonly MusicService _musicService;

        public MusicServiceTests()
        {
            _mockMusicService = new Mock<IMusicService>();
            _musicService = new MusicService();
        }

        [Fact]
        public void Play_ShouldReturnPlayingMessage()
        {
            // Arrange
            _mockMusicService.Setup(m => m.Play()).Returns("Playing: song1.mp3");

            // Act
            var result = _musicService.Play();

            // Assert
            Assert.StartsWith("Playing: ", result);
        }

        [Fact]
        public void Pause_ShouldReturnPausedMessage()
        {
            // Arrange
            _mockMusicService.Setup(m => m.Pause()).Returns("Music paused.");

            // Act
            var result = _musicService.Pause();

            // Assert
            Assert.Equal("Music paused.", result);
        }

        [Fact]
        public void Stop_ShouldReturnStoppedMessage()
        {
            // Arrange
            _mockMusicService.Setup(m => m.Stop()).Returns("Music stopped.");

            // Act
            var result = _musicService.Stop();

            // Assert
            Assert.Equal("Music stopped.", result);
        }

        [Fact]
        public void Next_ShouldReturnPlayingNextMessage()
        {
            // Arrange
            _mockMusicService.Setup(m => m.Next()).Returns("Playing next song: song2.mp3");

            // Act
            var result = _musicService.Next();

            // Assert
            Assert.StartsWith("Playing next song: ", result);
        }

        [Fact]
        public void Previous_ShouldReturnPlayingPreviousMessage()
        {
            // Arrange
            _mockMusicService.Setup(m => m.Previous()).Returns("Playing previous song: song1.mp3");

            // Act
            var result = _musicService.Previous();

            // Assert
            Assert.StartsWith("Playing previous song: ", result);
        }

        [Fact]
        public void List_ShouldReturnListOfSongs()
        {
            // Arrange
            _mockMusicService.Setup(m => m.List()).Returns("Available songs: song1.mp3, song2.mp3");

            // Act
            var result = _musicService.List();

            // Assert
            Assert.StartsWith("Available songs: ", result);
        }
    }
}
