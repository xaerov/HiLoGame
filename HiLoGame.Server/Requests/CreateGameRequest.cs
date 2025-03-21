namespace HiLoGame.Server.Requests
{
    public class CreateGameRequest
    {
        public required string PlayerName { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
    }
}
