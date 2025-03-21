using HiLoGame.Application.Services.Interfaces;
using HiLoGame.Domain;
using HiLoGame.Server.Controllers;
using HiLoGame.Server.Requests;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HiLoGame.Server.UnitTests
{
    public class GameControllerTests
    {
        private readonly Mock<IGameService> _mockGameService;
        private readonly GameController _controller;

        public GameControllerTests()
        {
            _mockGameService = new Mock<IGameService>();
            _controller = new GameController(_mockGameService.Object);
        }

        [Fact]
        public async Task CreateGame_ReturnsOkResult_WithCreatedGame()
        {
            // Arrange
            var request = new CreateGameRequest { PlayerName = "Player1", Min = 1, Max = 100 };
            var game = new Game
            {
                Id = 1,
                Player1Name = "Player1",
                Min = 1,
                Max = 100,
                MysteryNumber = 50,
                IsStarted = false,
                CurrentPlayerName = "Player1"
            };
            _mockGameService.Setup(service => service.CreateGameAsync(request.PlayerName, request.Min, request.Max))
                .ReturnsAsync(game);

            // Act
            var result = await _controller.CreateGame(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<Game>(okResult.Value);
            Assert.Equal(game, returnValue);
        }

        [Fact]
        public async Task JoinGame_ReturnsOkResult_WithJoinedGame()
        {
            // Arrange
            var request = new JoinGameRequest { GameId = 1, PlayerName = "Player2" };
            var game = new Game
            {
                Id = 1,
                Player1Name = "Player1",
                Player2Name = "Player2",
                Min = 1,
                Max = 100,
                MysteryNumber = 50,
                IsStarted = true,
                CurrentPlayerName = "Player1"
            };
            _mockGameService.Setup(service => service.JoinGameAsync(request.GameId, request.PlayerName))
                .ReturnsAsync(game);

            // Act
            var result = await _controller.JoinGame(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<Game>(okResult.Value);
            Assert.Equal(game, returnValue);
        }

        [Fact]
        public async Task MakeGuess_ReturnsOkResult_WithGuessResult()
        {
            // Arrange
            var request = new MakeGuessRequest { GameId = 1, Guess = 50, PlayerName = "Player1" };
            var guessResult = "Game over! We have a winner!";
            _mockGameService.Setup(service => service.MakeGuessAsync(request.GameId, request.Guess, request.PlayerName))
                .ReturnsAsync(guessResult);

            // Act
            var result = await _controller.MakeGuess(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<string>(okResult.Value);
            Assert.Equal(guessResult, returnValue);
        }
    }
}
