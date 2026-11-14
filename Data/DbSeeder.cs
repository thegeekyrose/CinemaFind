
using CinemaFind.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaFind.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(
            ApplicationDbContext context)
        {
            if (!await context.Genres.AnyAsync())
            {
                context.Genres.AddRange(
                    new Genre { Name = "Action" },
                    new Genre { Name = "Comedy" },
                    new Genre { Name = "Drama" },
                    new Genre { Name = "Science Fiction" },
                    new Genre { Name = "Adventure" },
                    new Genre { Name = "Thriller" }
                );

                await context.SaveChangesAsync();
            }

            if (!await context.Films.AnyAsync())
            {
                context.Films.AddRange(
                    new Film
                    {
                        Title = "Inception",
                        ReleaseDate = new DateTime(2010, 7, 16),
                        RuntimeMinutes = 148,
                        Synopsis =
                            "A skilled thief enters dreams to steal secrets."
                    },
                    new Film
                    {
                        Title = "The Matrix",
                        ReleaseDate = new DateTime(1999, 3, 31),
                        RuntimeMinutes = 136,
                        Synopsis =
                            "A hacker discovers that reality is a simulation."
                    },
                    new Film
                    {
                        Title = "Interstellar",
                        ReleaseDate = new DateTime(2014, 11, 7),
                        RuntimeMinutes = 169,
                        Synopsis =
                            "Explorers travel through space to seek a future for humanity."
                    },
                    new Film
                    {
                        Title = "The Dark Knight",
                        ReleaseDate = new DateTime(2008, 7, 18),
                        RuntimeMinutes = 152,
                        Synopsis =
                            "Batman faces a criminal who threatens Gotham."
                    },
                    new Film
                    {
                        Title = "The Truman Show",
                        ReleaseDate = new DateTime(1998, 6, 5),
                        RuntimeMinutes = 103,
                        Synopsis =
                            "A man begins to question the reality of his life."
                    },
                    new Film
                    {
                        Title = "Toy Story",
                        ReleaseDate = new DateTime(1995, 11, 22),
                        RuntimeMinutes = 81,
                        Synopsis =
                            "Toys come to life when humans are not around."
                    }
                );

                await context.SaveChangesAsync();
            }

            if (!await context.FilmGenres.AnyAsync())
            {
                var films = await context.Films.ToListAsync();
                var genres = await context.Genres.ToListAsync();

                int FilmId(string title) =>
                    films.Single(f => f.Title == title).Id;

                int GenreId(string name) =>
                    genres.Single(g => g.Name == name).Id;

                context.FilmGenres.AddRange(
                    new FilmGenre
                    {
                        FilmId = FilmId("Inception"),
                        GenreId = GenreId("Science Fiction")
                    },
                    new FilmGenre
                    {
                        FilmId = FilmId("Inception"),
                        GenreId = GenreId("Action")
                    },
                    new FilmGenre
                    {
                        FilmId = FilmId("The Matrix"),
                        GenreId = GenreId("Science Fiction")
                    },
                    new FilmGenre
                    {
                        FilmId = FilmId("The Matrix"),
                        GenreId = GenreId("Action")
                    },
                    new FilmGenre
                    {
                        FilmId = FilmId("Interstellar"),
                        GenreId = GenreId("Science Fiction")
                    },
                    new FilmGenre
                    {
                        FilmId = FilmId("Interstellar"),
                        GenreId = GenreId("Drama")
                    },
                    new FilmGenre
                    {
                        FilmId = FilmId("The Dark Knight"),
                        GenreId = GenreId("Action")
                    },
                    new FilmGenre
                    {
                        FilmId = FilmId("The Dark Knight"),
                        GenreId = GenreId("Thriller")
                    },
                    new FilmGenre
                    {
                        FilmId = FilmId("The Truman Show"),
                        GenreId = GenreId("Comedy")
                    },
                    new FilmGenre
                    {
                        FilmId = FilmId("The Truman Show"),
                        GenreId = GenreId("Drama")
                    },
                    new FilmGenre
                    {
                        FilmId = FilmId("Toy Story"),
                        GenreId = GenreId("Comedy")
                    },
                    new FilmGenre
                    {
                        FilmId = FilmId("Toy Story"),
                        GenreId = GenreId("Adventure")
                    }
                );

                await context.SaveChangesAsync();
            }
        }
    }
}
