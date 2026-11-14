namespace CinemaFind.Models
{
    public class FilmPerson
    {
        public int FilmId { get; set; }
        public Film Film { get; set; } = null!;

        public int PersonId { get; set; }
        public Person Person { get; set; } = null!;

        public required string Role { get; set; }

        public int? CastOrder { get; set; }
        public string? CharacterName { get; set; }
    }
}