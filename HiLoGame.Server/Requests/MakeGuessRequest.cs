namespace HiLoGame.Server.Requests
{
    public class MakeGuessRequest
    {
        public int GameId { get; set; }
        public int Guess { get; set; }
        public required string PlayerName { get; set; }
    }
}
