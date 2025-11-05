using GameBox.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;

namespace GameBox.Models
{

    public class GameModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 1, ErrorMessage = "Game name cannot exceed 100 characters.")]
        public required string Name { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public required string Description { get; set; } = string.Empty;

        [Range(1, 10, ErrorMessage = "Ratings must be between 1 and 10")]
        public int? Rating { get; set; }

        [ForeignKey("ApplicationUser")]
        public required string CreatorId { get; set; }
        public required ApplicationUser Creator { get; set; }

    }
}

public class GameListViewModel
{
    public required IEnumerable<GameModel> Games { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public string? SearchTerm { get; set; }

}
