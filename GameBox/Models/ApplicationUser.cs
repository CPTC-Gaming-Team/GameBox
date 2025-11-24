using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;

namespace GameBox.Models
{
    public class ApplicationUser : IdentityUser
    {
        // user who follow me
        public ICollection<UserFollower> Followers { get; set; } = new List<UserFollower>();

        // user who I follow
        public ICollection<UserFollower> Following { get; set; } = new List<UserFollower>();

        // games I watch
        public ICollection<GameWatcher> WatchedGames { get; set; } = new List<GameWatcher>();
    }
}
