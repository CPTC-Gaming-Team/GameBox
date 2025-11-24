namespace GameBox.Models
{
    public class UserFollower
    {
        public string UserId { get; set; } = string.Empty;     //The user being followed
        public ApplicationUser? User { get; set; }

        public string FollowerId { get; set; } = string.Empty; //The follower
        public ApplicationUser? Follower { get; set; }
    }
}
