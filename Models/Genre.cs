namespace CinemaFind.Models
{
    public class Genre
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public ICollection<FilmGenre> FilmGenres { get; set; }
            = new List<FilmGenre>();
    }
}