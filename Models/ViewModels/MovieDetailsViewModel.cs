
using CinemaFind.Models;

namespace CinemaFind.Models.ViewModels
{
    public class MovieDetailsViewModel
    {
        public Film Film { get; set; } = null!;

        public double? AverageRating { get; set; }

        public int RatingCount { get; set; }

        public int? UserRating { get; set; }

        public int? ViewingProgress { get; set; }

        public bool HasViewingHistory { get; set; }
    }
}
