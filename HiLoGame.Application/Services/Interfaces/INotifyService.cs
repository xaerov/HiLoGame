namespace HiLoGame.Application.Services.Interfaces
{
    public interface INotifyService
    {
        Task NotifyNewTurn(int gameId, string playerName);
        Task NotifyEndGame(int gameId, string winnerPlayerName);
    }
}
