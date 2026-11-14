
using System.ComponentModel.DataAnnotations;

namespace CinemaFind.Models
{
    public class UserRating
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        public ApplicationUser User { get; set; } = null!;

        public int FilmId { get; set; }

        public Film Film { get; set; } = null!;

        [Range(1, 10)]
        public int Score { get; set; }

        public DateTime RatedAt { get; set; } = DateTime.UtcNow;
    }
}
