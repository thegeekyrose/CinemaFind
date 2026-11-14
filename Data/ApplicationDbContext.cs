using CinemaFind.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CinemaFind.Data
{
    public class ApplicationDbContext
        : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Film> Films => Set<Film>();
        public DbSet<Person> People => Set<Person>();
        public DbSet<Genre> Genres => Set<Genre>();
        public DbSet<FilmGenre> FilmGenres => Set<FilmGenre>();
        public DbSet<FilmPerson> FilmPeople => Set<FilmPerson>();
        public DbSet<ViewingHistory> ViewingHistories
            => Set<ViewingHistory>();
        public DbSet<UserRating> UserRatings => Set<UserRating>();

        protected override void OnModelCreating(
            ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<FilmGenre>()
                .HasKey(fg => new { fg.FilmId, fg.GenreId });

            builder.Entity<FilmPerson>()
                .HasKey(fp => new
                {
                    fp.FilmId,
                    fp.PersonId,
                    fp.Role
                });

            builder.Entity<UserRating>()
                .HasIndex(r => new { r.UserId, r.FilmId })
                .IsUnique();

            builder.Entity<Genre>()
                .HasIndex(g => g.Name)
                .IsUnique();

            builder.Entity<UserRating>()
                 .HasIndex(r => new { r.UserId, r.FilmId })
                 .IsUnique();

            builder.Entity<ViewingHistory>()
                .HasIndex(h => new { h.UserId, h.FilmId })
                .IsUnique();
        }
    }
}