
using System.ComponentModel.DataAnnotations;

namespace CinemaFind.Models.ViewModels
{
    public class RateMovieViewModel
    {
        [Required]
        public int FilmId { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "Rating must be between 1 and 10.")]
        public int Score { get; set; }
    }
}
