using HiLoGame.Application.Repositories.Interfaces;
using HiLoGame.Application.Services.Interfaces;
using HiLoGame.Domain;

namespace HiLoGame.Application.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly INotifyService _notifyService;

        public GameService(IGameRepository gameRepository, INotifyService notifyService)
        {
            _gameRepository = gameRepository;
            _notifyService = notifyService;
        }

        public async Task<Game> CreateGameAsync(string playerName, int min, int max)
        {
            var mysteryNumber = new Random().Next(min, max + 1);
            var game = new Game
            {
                Player1Name = playerName,
                Min = min,
                Max = max,
                MysteryNumber = mysteryNumber,
                IsStarted = false,
                CurrentPlayerName = playerName
            };

            var createdGame = await _gameRepository.CreateGameAsync(game);

            return createdGame;
        }

        public async Task<Game> JoinGameAsync(int gameId, string playerName)
        {
            var game = await _gameRepository.GetGameAsync(gameId);
            if (game == null)
            {
                throw new Exception("Game not found.");
            }

            if (!string.IsNullOrEmpty(game.Player2Name))
            {
                throw new Exception("The game is full already");
            }

            game.Player2Name = playerName;
            game.IsStarted = true;
            game.CurrentPlayerName = game.Player1Name; // Player 1 starts
            await _gameRepository.UpdateGameAsync(game);

            await _notifyService.NotifyNewTurn(game.Id, game.CurrentPlayerName);

            return game;
        }

        public async Task<string> MakeGuessAsync(int gameId, int guess, string playerName)
        {
            var game = await _gameRepository.GetGameAsync(gameId);
            if (game == null)
            {
                return "Game not found.";
            }

            if (!game.IsStarted)
            {
                return "Game has not started yet.";
            }

            if (playerName != game.CurrentPlayerName)
            {
                return "It's not your turn!";
            }

            // Check if the guess is correct
            if (guess == game.MysteryNumber)
            {
                game.IsStarted = false;
                await _gameRepository.UpdateGameAsync(game);
                await _notifyService.NotifyEndGame(gameId, playerName);
                return "Game over! We have a winner!";
            }

            // Switch the turn
            game.CurrentPlayerName = game.CurrentPlayerName == game.Player1Name ? game.Player2Name! : game.Player1Name;
            await _gameRepository.UpdateGameAsync(game); // Save changes after each guess
            await _notifyService.NotifyNewTurn(gameId, game.CurrentPlayerName);

            return guess < game.MysteryNumber ? "LO" : "HI";
        }
    }
}
