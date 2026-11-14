
using CinemaFind.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CinemaFind.Models.ViewModels
{
    public class MovieIndexViewModel
    {
        public List<Film> Films { get; set; } = new();

        public string? SearchQuery { get; set; }

        public int? SelectedGenreId { get; set; }

        [Range(1888, 2100, ErrorMessage = "Enter a valid release year.")]
        public int? FromYear { get; set; }

        [Range(1888, 2100, ErrorMessage = "Enter a valid release year.")]
        public int? ToYear { get; set; }

        public string SortOrder { get; set; } = "title";

        public List<SelectListItem> Genres { get; set; } = new();
    }
}
