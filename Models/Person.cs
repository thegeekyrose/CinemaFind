namespace CinemaFind.Models
{
    public class Person
    {
        public int Id { get; set; }

        public required string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? PlaceOfBirth { get; set; }
        public string? Biography { get; set; }
        public string? PhotoUrl { get; set; }

        public ICollection<FilmPerson> FilmPeople { get; set; }
            = new List<FilmPerson>();
    }
}