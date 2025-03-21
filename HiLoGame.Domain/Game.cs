namespace HiLoGame.Domain
{
    public class Game
    {
        public int Id { get; set; }
        public required string Player1Name { get; set; }
        public string? Player2Name { get; set; }
        public int MysteryNumber { get; set; }
        public bool IsStarted { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public required string CurrentPlayerName { get; set; }
    }
}
