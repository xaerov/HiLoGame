using HiLoGame.Domain;

namespace HiLoGame.Application.Services.Interfaces
{
    public interface IGameService
    {
        Task<Game> CreateGameAsync(string playerName, int min, int max);
        Task<Game> JoinGameAsync(int gameId, string playerName);
        Task<string> MakeGuessAsync(int gameId, int guess, string playerName);
    }
}
