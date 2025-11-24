using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;

namespace GameBox.Models
{
    public class ApplicationUser : IdentityUser
    {
        // user who follow me
        public ICollection<ApplicationUser> Followers { get; set; } = new List<ApplicationUser>();

        // user who I follow
        public ICollection<ApplicationUser> Following { get; set; } = new List<ApplicationUser>();

        // games I watch
        public ICollection<GameModel> WatchedGames { get; set; } = new List<GameModel>();
    }
}
