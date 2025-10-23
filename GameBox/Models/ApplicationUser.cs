using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;

namespace GameBox.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<ApplicationUser> Followers { get; set; } = new List<ApplicationUser>();
    }
}
