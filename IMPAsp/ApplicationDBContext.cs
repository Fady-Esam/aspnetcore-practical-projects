using IMPAsp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IMPAsp
{
    //public class ApplicationDBContext : IdentityDbContext<IdentityUser>
    //{
    //    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
    //    {
    //    }
    //}
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Laptop", Price = 999.99m, Description = "High-performance laptop", StockQuantity = 50 },
                new Product { Id = 2, Name = "Smartphone", Price = 699.99m, Description = "Latest model smartphone", StockQuantity = 100 },
                new Product { Id = 3, Name = "Headphones", Price = 199.99m, Description = "Noise-cancelling headphones", StockQuantity = 150 },
                new Product { Id = 4, Name = "Smartwatch", Price = 299.99m, Description = "Fitness tracking smartwatch", StockQuantity = 75 },
                new Product { Id = 5, Name = "Tablet", Price = 499.99m, Description = "10-inch display tablet", StockQuantity = 60 }
            );
        }
        public DbSet<Product> Proudcts { get; set; }
    }
}
