using HiLoGame.Application.Repositories.Interfaces;
using HiLoGame.Domain;
using Microsoft.EntityFrameworkCore;

namespace HiLoGame.Infrastructure.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly HiLoGameContext _dbContext;

        public GameRepository(HiLoGameContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Game> CreateGameAsync(Game game)
        {
            _dbContext.Games.Add(game);
            await _dbContext.SaveChangesAsync();
            return game;
        }

        public async Task<Game?> GetGameAsync(int gameId)
        {
            return await _dbContext.Games.FirstOrDefaultAsync(g => g.Id == gameId);
        }

        public async Task UpdateGameAsync(Game game)
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}

