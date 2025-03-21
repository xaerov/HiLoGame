using HiLoGame.Application.Repositories.Interfaces;
using HiLoGame.Application.Services;
using HiLoGame.Application.Services.Interfaces;
using HiLoGame.Domain;
using Moq;

namespace HiLoGame.Application.UnitTests
{
    public class GameServiceTests
    {
        private readonly Mock<IGameRepository> _mockGameRepository;
        private readonly Mock<INotifyService> _mockNotifyService;
        private readonly GameService _gameService;

        public GameServiceTests()
        {
            _mockGameRepository = new Mock<IGameRepository>();
            _mockNotifyService = new Mock<INotifyService>();
            _gameService = new GameService(_mockGameRepository.Object, _mockNotifyService.Object);
        }

        [Fact]
        public async Task CreateGameAsync_ReturnsCreatedGame()
        {
            // Arrange
            var playerName = "Player1";
            var min = 1;
            var max = 100;
            var game = new Game
            {
                Id = 1,
                Player1Name = playerName,
                Min = min,
                Max = max,
                MysteryNumber = 50,
                IsStarted = false,
                CurrentPlayerName = playerName
            };
            _mockGameRepository.Setup(repo => repo.CreateGameAsync(It.IsAny<Game>())).ReturnsAsync(game);

            // Act
            var result = await _gameService.CreateGameAsync(playerName, min, max);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(game, result);
        }

        [Fact]
        public async Task JoinGameAsync_ReturnsJoinedGame()
        {
            // Arrange
            var gameId = 1;
            var playerName = "Player2";
            var game = new Game
            {
                Id = gameId,
                Player1Name = "Player1",
                Min = 1,
                Max = 100,
                MysteryNumber = 50,
                IsStarted = false,
                CurrentPlayerName = "Player1"
            };
            _mockGameRepository.Setup(repo => repo.GetGameAsync(gameId)).ReturnsAsync(game);
            _mockGameRepository.Setup(repo => repo.UpdateGameAsync(It.IsAny<Game>())).Returns(Task.CompletedTask);
            _mockNotifyService.Setup(service => service.NotifyNewTurn(gameId, game.CurrentPlayerName)).Returns(Task.CompletedTask);

            // Act
            var result = await _gameService.JoinGameAsync(gameId, playerName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(playerName, result.Player2Name);
            Assert.True(result.IsStarted);
        }

        [Fact]
        public async Task MakeGuessAsync_ReturnsCorrectResult_WhenGuessIsCorrect()
        {
            // Arrange
            var gameId = 1;
            var guess = 50;
            var playerName = "Player1";
            var game = new Game
            {
                Id = gameId,
                Player1Name = playerName,
                Min = 1,
                Max = 100,
                MysteryNumber = guess,
                IsStarted = true,
                CurrentPlayerName = playerName
            };
            _mockGameRepository.Setup(repo => repo.GetGameAsync(gameId)).ReturnsAsync(game);
            _mockGameRepository.Setup(repo => repo.UpdateGameAsync(It.IsAny<Game>())).Returns(Task.CompletedTask);
            _mockNotifyService.Setup(service => service.NotifyEndGame(gameId, playerName)).Returns(Task.CompletedTask);

            // Act
            var result = await _gameService.MakeGuessAsync(gameId, guess, playerName);

            // Assert
            Assert.Equal("Game over! We have a winner!", result);
        }

        [Fact]
        public async Task MakeGuessAsync_ReturnsLo_WhenGuessIsLower()
        {
            // Arrange
            var gameId = 1;
            var guess = 25;
            var playerName = "Player1";
            var game = new Game
            {
                Id = gameId,
                Player1Name = playerName,
                Min = 1,
                Max = 100,
                MysteryNumber = 50,
                IsStarted = true,
                CurrentPlayerName = playerName
            };
            _mockGameRepository.Setup(repo => repo.GetGameAsync(gameId)).ReturnsAsync(game);
            _mockGameRepository.Setup(repo => repo.UpdateGameAsync(It.IsAny<Game>())).Returns(Task.CompletedTask);
            _mockNotifyService.Setup(service => service.NotifyNewTurn(gameId, It.IsAny<string>())).Returns(Task.CompletedTask);

            // Act
            var result = await _gameService.MakeGuessAsync(gameId, guess, playerName);

            // Assert
            Assert.Equal("LO", result);
        }

        [Fact]
        public async Task MakeGuessAsync_ReturnsHi_WhenGuessIsHigher()
        {
            // Arrange
            var gameId = 1;
            var guess = 75;
            var playerName = "Player1";
            var game = new Game
            {
                Id = gameId,
                Player1Name = playerName,
                Min = 1,
                Max = 100,
                MysteryNumber = 50,
                IsStarted = true,
                CurrentPlayerName = playerName
            };
            _mockGameRepository.Setup(repo => repo.GetGameAsync(gameId)).ReturnsAsync(game);
            _mockGameRepository.Setup(repo => repo.UpdateGameAsync(It.IsAny<Game>())).Returns(Task.CompletedTask);
            _mockNotifyService.Setup(service => service.NotifyNewTurn(gameId, It.IsAny<string>())).Returns(Task.CompletedTask);

            // Act
            var result = await _gameService.MakeGuessAsync(gameId, guess, playerName);

            // Assert
            Assert.Equal("HI", result);
        }
    }
}
