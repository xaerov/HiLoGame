namespace HiLoGame.Server.Requests
{
    public class JoinGameRequest
    {
        public int GameId { get; set; }
        public required string PlayerName { get; set; }
    }
}
