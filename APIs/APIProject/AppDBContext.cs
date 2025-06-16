
using APIProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace APIProject
{
    public class AppDBContext : IdentityDbContext<AppUser>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Clothes"},
                new Category { Id = 2, Name = "Phones"},
                new Category { Id = 3, Name = "Laptops"},
                new Category { Id = 4, Name = "Watches"},
                new Category { Id = 5, Name = "ACs"}
            );

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Items)
                .WithOne(i => i.Category)
                .HasForeignKey(i => i.CategoryId);
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }

    }
}
