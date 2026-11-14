namespace CinemaFind.Models
{
    public class Film
    {
        public int Id { get; set; }

        public required string Title { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? RuntimeMinutes { get; set; }
        public string? Synopsis { get; set; }
        public string? PosterUrl { get; set; }

        public ICollection<FilmGenre> FilmGenres { get; set; }
            = new List<FilmGenre>();

        public ICollection<FilmPerson> FilmPeople { get; set; }
            = new List<FilmPerson>();

        public ICollection<UserRating> UserRatings { get; set; }
            = new List<UserRating>();

        public ICollection<ViewingHistory> ViewingHistory { get; set; }
            = new List<ViewingHistory>();
    }
}