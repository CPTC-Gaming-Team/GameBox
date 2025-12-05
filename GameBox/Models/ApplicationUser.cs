using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;

namespace GameBox.Models
{
    /// <summary>
    /// Represents an application user with extended profile and social features.
    /// Extends the base IdentityUser with additional GameBox-specific properties.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Gets or sets the collection of users who follow this user.
        /// </summary>
        public ICollection<ApplicationUser> Followers { get; set; } = new List<ApplicationUser>();
    }
}
