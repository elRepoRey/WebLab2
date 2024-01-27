using Microsoft.EntityFrameworkCore;
using TheFlowerplace.Models;

namespace TheFlowerplace.Data
{
    
    public class ApplicationDbContext : DbContext
    {        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
          
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
           
            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Product>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Product>()
           .Property(p => p.Price)
           .HasPrecision(18, 2);

            modelBuilder.Entity<Product>().HasData(ProductSeedData.GetPreconfiguredProducts());
        }
    }
}
