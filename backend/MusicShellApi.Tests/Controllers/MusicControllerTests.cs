using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MusicShellApi.Controllers;
using MusicShellApi.Data.Dtos;
using MusicShellApi.Data.Models;
using MusicShellApi.Mediators.Interfaces;
using Xunit;

namespace MusicShellApi.Tests.Controllers
{
    public class MusicControllerTests
    {
        private readonly Mock<IMusicMediator> _musicMediatorMock;
        private readonly MusicController _controller;

        public MusicControllerTests()
        {
            _musicMediatorMock = new Mock<IMusicMediator>();
            _controller = new MusicController(_musicMediatorMock.Object);
        }

        private List<SongInfoDto> ConvertToDtoList(List<SongInfo> songs)
        {
            var dtoList = new List<SongInfoDto>();
            foreach (var song in songs)
            {
                dtoList.Add(song.ToDto());
            }
            return dtoList;
        }

        private bool AreMessagesEqual(object expected, object actual)
        {
            var expectedMessage = expected.GetType().GetProperty("message").GetValue(expected, null);
            var actualMessage = actual.GetType().GetProperty("message").GetValue(actual, null);
            return expectedMessage.Equals(actualMessage);
        }

        [Fact]
        public void GetAllSongs_ReturnsOkResult_WithListOfSongs()
        {
            // Arrange
            var songsDto = new List<SongInfoDto>
            {
                new SongInfoDto { Title = "Song 1", Artist = "Artist 1", FilePath = "path1", Duration = "03:00" },
                new SongInfoDto { Title = "Song 2", Artist = "Artist 2", FilePath = "path2", Duration = "04:00" }
            };
            _musicMediatorMock.Setup(m => m.GetAllSongInfoDtos()).Returns(songsDto);

            // Act
            var result = _controller.GetAllSongs();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<SongInfoDto>>(okResult.Value);
            Assert.Equal(songsDto.Count, returnValue.Count);

            for (int i = 0; i < songsDto.Count; i++)
            {
                Assert.Equal(songsDto[i].Title, returnValue[i].Title);
                Assert.Equal(songsDto[i].Artist, returnValue[i].Artist);
                Assert.Equal(songsDto[i].FilePath, returnValue[i].FilePath);
                Assert.Equal(songsDto[i].Duration, returnValue[i].Duration);
            }
        }

        [Fact]
        public void GetSong_ReturnsOkResult_WithSpecificSong()
        {
            // Arrange
            var song = new SongInfo { Title = "Song 1", Artist = "Artist 1" };
            _musicMediatorMock.Setup(m => m.GetSongInfo(It.IsAny<int>())).Returns(song);

            // Act
            var result = _controller.GetSong(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<SongInfoDto>(okResult.Value);
            Assert.Equal(song.Title, returnValue.Title);
        }

        [Fact]
        public void GetSong_ReturnsBadRequest_WhenIndexIsOutOfRange()
        {
            // Arrange
            _musicMediatorMock.Setup(m => m.GetSongInfo(It.IsAny<int>())).Throws(new ArgumentOutOfRangeException());

            // Act
            var result = _controller.GetSong(-1);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void Play_ReturnsOkResult_WithMessage()
        {
            // Arrange
            var message = "Playing song";
            _musicMediatorMock.Setup(m => m.Play()).Returns(message);

            // Act
            var result = _controller.Play();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var expectedValue = new { message };
            Assert.True(AreMessagesEqual(expectedValue, okResult.Value));
        }

        [Fact]
        public void Pause_ReturnsOkResult_WithMessage()
        {
            // Arrange
            var message = "Pausing song";
            _musicMediatorMock.Setup(m => m.Pause()).Returns(message);

            // Act
            var result = _controller.Pause();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var expectedValue = new { message };
            Assert.True(AreMessagesEqual(expectedValue, okResult.Value));
        }

        [Fact]
        public void Stop_ReturnsOkResult_WithMessage()
        {
            // Arrange
            var message = "Stopping song";
            _musicMediatorMock.Setup(m => m.Stop()).Returns(message);

            // Act
            var result = _controller.Stop();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var expectedValue = new { message };
            Assert.True(AreMessagesEqual(expectedValue, okResult.Value));
        }

        [Fact]
        public void Next_ReturnsOkResult_WithMessage()
        {
            // Arrange
            var message = "Playing next song";
            _musicMediatorMock.Setup(m => m.Next()).Returns(message);

            // Act
            var result = _controller.Next();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var expectedValue = new { message };
            Assert.True(AreMessagesEqual(expectedValue, okResult.Value));
        }

        [Fact]
        public void Previous_ReturnsOkResult_WithMessage()
        {
            // Arrange
            var message = "Playing previous song";
            _musicMediatorMock.Setup(m => m.Previous()).Returns(message);

            // Act
            var result = _controller.Previous();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var expectedValue = new { message };
            Assert.True(AreMessagesEqual(expectedValue, okResult.Value));
        }

        [Fact]
        public void List_ReturnsOkResult_WithListOfSongTitles()
        {
            // Arrange
            var songTitles = new List<string> { "Song 1", "Song 2" };
            _musicMediatorMock.Setup(m => m.List()).Returns(songTitles);

            // Act
            var result = _controller.List();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<string>>(okResult.Value);
            Assert.Equal(songTitles.Count, returnValue.Count);
        }

        [Fact]
        public void GetCurrentSong_ReturnsOkResult_WithCurrentSong()
        {
            // Arrange
            var song = new SongInfo { Title = "Current Song", Artist = "Artist 1" };
            _musicMediatorMock.Setup(m => m.GetCurrentSong()).Returns(song);

            // Act
            var result = _controller.GetCurrentSong();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<SongInfoDto>(okResult.Value);
            Assert.Equal(song.Title, returnValue.Title);
        }
    }
}
