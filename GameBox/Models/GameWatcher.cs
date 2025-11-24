namespace GameBox.Models
{
    public class GameWatcher
    {
        public int GameId { get; set; }
        public GameModel? Game { get; set; }

        public string UserId { get; set; } = string.Empty;
        public ApplicationUser? User { get; set; }
    }
}
