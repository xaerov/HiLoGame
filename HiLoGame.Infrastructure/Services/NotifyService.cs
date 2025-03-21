using HiLoGame.Application.Services.Interfaces;
using HiLoGame.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace HiLoGame.Infrastructure.Services
{
    public class NotifyService : INotifyService
    {
        private readonly IHubContext<GameHub> _hubContext;

        public NotifyService(IHubContext<GameHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyNewTurn(int gameId, string playerName)
        {
            // Notify all players in the game group
            await _hubContext.Clients.Group(gameId.ToString()).SendAsync("NewTurn", playerName);
        }

        public async Task NotifyEndGame(int gameId, string winnerPlayerName)
        {
            // Notify all players in the game group
            await _hubContext.Clients.Group(gameId.ToString()).SendAsync("EndGame", winnerPlayerName);
        }
    }
}
