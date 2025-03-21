using HiLoGame.Domain;

namespace HiLoGame.Application.Repositories.Interfaces
{
    public interface IGameRepository
    {
        Task<Game> CreateGameAsync(Game game);
        Task<Game?> GetGameAsync(int gameId);
        Task UpdateGameAsync(Game game);
    }

}
