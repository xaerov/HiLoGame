using Microsoft.AspNetCore.SignalR;

namespace HiLoGame.Infrastructure.Hubs
{
    public class GameHub : Hub
    {
        public async Task JoinGameGroup(int gameId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, gameId.ToString());
        }
    }
}
