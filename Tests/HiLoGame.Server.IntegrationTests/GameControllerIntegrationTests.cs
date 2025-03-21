using HiLoGame.Domain;
using HiLoGame.Server.Requests;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace HiLoGame.Server.IntegrationTests
{
    public class GameControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public GameControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateGame_ReturnsOkResult_WithCreatedGame()
        {
            // Arrange
            var request = new CreateGameRequest { PlayerName = "Player1", Min = 1, Max = 100 };

            // Act
            var response = await _client.PostAsJsonAsync("/api/game/create", request);

            // Assert
            response.EnsureSuccessStatusCode();
            var game = await response.Content.ReadFromJsonAsync<Game>();
            Assert.NotNull(game);
            Assert.Equal("Player1", game.Player1Name);
            Assert.Equal(1, game.Min);
            Assert.Equal(100, game.Max);
        }

        [Fact]
        public async Task JoinGame_ReturnsOkResult_WithJoinedGame()
        {
            // Arrange
            var createRequest = new CreateGameRequest { PlayerName = "Player1", Min = 1, Max = 100 };
            var createResponse = await _client.PostAsJsonAsync("/api/game/create", createRequest);
            createResponse.EnsureSuccessStatusCode();
            var createdGame = await createResponse.Content.ReadFromJsonAsync<Game>();

            var joinRequest = new JoinGameRequest { GameId = createdGame.Id, PlayerName = "Player2" };

            // Act
            var response = await _client.PostAsJsonAsync("/api/game/join", joinRequest);

            // Assert
            response.EnsureSuccessStatusCode();
            var game = await response.Content.ReadFromJsonAsync<Game>();
            Assert.NotNull(game);
            Assert.Equal("Player2", game.Player2Name);
            Assert.True(game.IsStarted);
        }

        [Fact]
        public async Task MakeGuess_ReturnsOkResult_WithGuessResult()
        {
            // Arrange
            var createRequest = new CreateGameRequest { PlayerName = "Player1", Min = 1, Max = 100 };
            var createResponse = await _client.PostAsJsonAsync("/api/game/create", createRequest);
            createResponse.EnsureSuccessStatusCode();
            var createdGame = await createResponse.Content.ReadFromJsonAsync<Game>();

            var joinRequest = new JoinGameRequest { GameId = createdGame.Id, PlayerName = "Player2" };
            var joinResponse = await _client.PostAsJsonAsync("/api/game/join", joinRequest);
            joinResponse.EnsureSuccessStatusCode();

            var guessRequest = new MakeGuessRequest { GameId = createdGame.Id, Guess = 50, PlayerName = "Player1" };

            // Act
            var response = await _client.PostAsJsonAsync("/api/game/guess", guessRequest);

            // Assert
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            Assert.NotNull(result);
        }
    }
}