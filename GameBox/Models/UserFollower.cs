namespace GameBox.Models
{
    public class UserFollower
    {
        public string UserID { get; set; } = string.Empty;     //The user being followed
        public ApplicationUser? User { get; set; }

        public string FollowerID { get; set; } = string.Empty; //The follower
        public ApplicationUser? Follower { get; set; }
    }
}
