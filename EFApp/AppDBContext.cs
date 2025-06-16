
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFApp
{
    internal class AppDBContext : DbContext
    {
     
        public DbSet<GeneTypes> geneTypes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server =DESKTOP-2CS7U6C\\SQLSERVERTEST; DataBase = HR_DB; User Id = sa; Password = 123; TrustServerCertificate=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<GeneTypes>().HasKey(g => g.GeneTypeID);
        }
    }
}
