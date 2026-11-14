
using System.ComponentModel.DataAnnotations;

namespace CinemaFind.Models
{
    public class ViewingHistory
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        public ApplicationUser User { get; set; } = null!;

        public int FilmId { get; set; }

        public Film Film { get; set; } = null!;

        public DateTime ViewedAt { get; set; } = DateTime.UtcNow;

        [Range(0, 100)]
        public int? Progress { get; set; }

        public bool Completed { get; set; }
    }
}
