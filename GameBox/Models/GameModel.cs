using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        //Who uploaded this game
        public string? UploaderID { get; set; } = string.Empty;
        public ApplicationUser? Uploader { get; set; }

        //Watchers
        public ICollection<GameWatcher> Watchers { get; set; } = new List<GameWatcher>();
    }
}
