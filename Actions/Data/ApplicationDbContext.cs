using Actions.Models;
using Actions.Models.OldModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Actions.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Action"},
                new Genre { Id = 2, Name = "Horror"},
                new Genre { Id = 3, Name = "Romnace"},
                new Genre { Id = 4, Name = "Myster"},
                new Genre { Id = 5, Name = "Sience Fiction"}
            );
            builder.Entity<Movie>().HasData(
                new Movie {Id = 1, Name = "Inception", Price = 351.53M, GenreId = 4, ReleaseDate = new DateOnly(2003, 1, 17)},
                new Movie {Id = 2, Name = "Shutter Island", Price = 294.1M, GenreId = 4, ReleaseDate = new DateOnly(2012, 5, 22)},
                new Movie {Id = 3, Name = "The Matrix", Price = 81.51M, GenreId = 1, ReleaseDate = new DateOnly(2009, 2, 13)},
                new Movie {Id = 4, Name = "God Father", Price = 351.53M, GenreId = 3, ReleaseDate = new DateOnly(1995, 10, 3)},
                new Movie {Id = 5, Name = "The Maze Runner", Price = 192.5M, GenreId = 5, ReleaseDate = new DateOnly(2018, 4, 10)}
            );
        }
    }

}

