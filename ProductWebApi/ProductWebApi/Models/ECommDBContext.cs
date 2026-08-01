using Microsoft.EntityFrameworkCore;

namespace ProductWebApi.Models
{
    public class ECommDBContext : DbContext
    {
        public ECommDBContext()
        {

        }
        public ECommDBContext(DbContextOptions<ECommDBContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .IsRequired();

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 101, CategoryName = "Mobile" },
                new Category { CategoryId = 102, CategoryName = "Cars" },
                new Category { CategoryId = 103, CategoryName = "Bikes" },
                new Category { CategoryId = 104, CategoryName = "Bats" }
                );
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 1001,
                    CategoryId = 104,
                    ProductName = "MRF",
                    Description = "Kashmir Willow Cricket Bat",
                    Price = 15000
                },
                new Product
                {
                    ProductId = 1002,
                    CategoryId = 104,
                    ProductName = "CAET",
                    Description = "Rohith Sharma Cricket Bat",
                    Price = 15000
                }
                );
        }
    }
}
