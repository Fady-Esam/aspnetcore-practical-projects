using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using StateManagementProject.Models;

namespace StateManagementProject
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the Product entity
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");

                // Seed dummy data
                entity.HasData(
                    new Product
                    {
                        Id = 1,
                        Name = "Laptop",
                        Description = "High-performance laptop for professionals",
                        Price = 1200.99m,
                        StockQuantity = 10,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = null
                    },
                    new Product
                    {
                        Id = 2,
                        Name = "Smartphone",
                        Description = "Latest 5G-enabled smartphone",
                        Price = 799.49m,
                        StockQuantity = 50,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = null
                    },
                    new Product
                    {
                        Id = 3,
                        Name = "Headphones",
                        Description = "Noise-cancelling wireless headphones",
                        Price = 199.99m,
                        StockQuantity = 25,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = null
                    },
                    new Product
                    {
                        Id = 4,
                        Name = "Smartwatch",
                        Description = "Fitness and health tracking smartwatch",
                        Price = 299.99m,
                        StockQuantity = 15,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = null
                    },
                    new Product
                    {
                        Id = 5,
                        Name = "Tablet",
                        Description = "Lightweight and portable tablet",
                        Price = 499.99m,
                        StockQuantity = 20,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = null
                    }
                );
            });
        }
        public DbSet<Product> Products { get; set; }

    }
}
