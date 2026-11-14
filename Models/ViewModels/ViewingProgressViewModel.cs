
using System.ComponentModel.DataAnnotations;

namespace CinemaFind.Models.ViewModels
{
    public class ViewingProgressViewModel
    {
        [Required]
        public int FilmId { get; set; }

        [Range(0, 100, ErrorMessage = "Progress must be between 0 and 100.")]
        public int Progress { get; set; }
    }
}
