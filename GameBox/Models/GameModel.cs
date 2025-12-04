using GameBox.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;

namespace GameBox.Models
{
    /// <summary>
    /// Represents a game in the GameBox application.
    /// </summary>
    public class GameModel
    {
        /// <summary>
        /// Gets or sets the unique identifier for the game.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the game.
        /// </summary>
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Game name cannot exceed 100 characters.")]
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the game.
        /// </summary>
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public required string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the rating for the game on a scale of 1 to 10.
        /// </summary>
        [Range(1, 10, ErrorMessage = "Ratings must be between 1 and 10")]
        public int? Rating { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who owns this game.
        /// </summary>
        // Shouldn't this work either way?
        [ForeignKey("ApplicationUser")]
        public string? OwnerId { get; set; }

        /// <summary>
        /// Gets or sets the navigation property to the application user who owns this game.
        /// </summary>
        [ForeignKey(nameof(OwnerId))]
        public ApplicationUser? Owner { get; set; }
    }
}

/// <summary>
/// View model for displaying a paginated list of games with search capabilities.
/// </summary>
public class GameListViewModel
{
    /// <summary>
    /// Gets or sets the collection of games to display.
    /// </summary>
    public required IEnumerable<GameModel> Games { get; set; }
    
    /// <summary>
    /// Gets or sets the current page number in the pagination.
    /// </summary>
    public int CurrentPage { get; set; }
    
    /// <summary>
    /// Gets or sets the total number of pages available.
    /// </summary>
    public int TotalPages { get; set; }
    
    /// <summary>
    /// Gets or sets the number of items to display per page.
    /// </summary>
    public int PageSize { get; set; }
    
    /// <summary>
    /// Gets or sets the total number of items across all pages.
    /// </summary>
    public int TotalItems { get; set; }
    
    /// <summary>
    /// Gets or sets the search term used to filter the games list.
    /// </summary>
    public string? SearchTerm { get; set; }
}