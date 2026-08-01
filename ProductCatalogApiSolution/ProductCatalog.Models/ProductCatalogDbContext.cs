using Microsoft.EntityFrameworkCore;

namespace ProductCatalog.Models;

public class ProductCatalogDbContext : DbContext
{
    public ProductCatalogDbContext(DbContextOptions<ProductCatalogDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Category table configuration
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(c => c.CategoryId);

            entity.Property(c => c.CategoryName)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(c => c.Description)
                  .HasMaxLength(500);
        });

        // Product table configuration
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.ProductId);

            entity.Property(p => p.ProductName)
                  .IsRequired()
                  .HasMaxLength(150);

            entity.Property(p => p.Price)
                  .HasColumnType("decimal(18,2)");

            entity.Property(p => p.Quantity)
                  .IsRequired();

            entity.Property(p => p.IsActive)
                  .HasDefaultValue(true);

            entity.HasOne(p => p.Category)
                  .WithMany(c => c.Products)
                  .HasForeignKey(p => p.CategoryId);
        });

        // Seed dummy categories
        modelBuilder.Entity<Category>().HasData(
            new Category
            {
                CategoryId = 1,
                CategoryName = "Electronics",
                Description = "Electronic items and gadgets"
            },
            new Category
            {
                CategoryId = 2,
                CategoryName = "Books",
                Description = "Books and study materials"
            },
            new Category
            {
                CategoryId = 3,
                CategoryName = "Clothing",
                Description = "Men and women clothing"
            }
        );

        // Seed dummy products
        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                ProductId = 1,
                ProductName = "Laptop",
                Price = 55000,
                Quantity = 10,
                IsActive = true,
                CategoryId = 1
            },
            new Product
            {
                ProductId = 2,
                ProductName = "Mobile Phone",
                Price = 25000,
                Quantity = 25,
                IsActive = true,
                CategoryId = 1
            },
            new Product
            {
                ProductId = 3,
                ProductName = "C# Beginner Book",
                Price = 799,
                Quantity = 50,
                IsActive = true,
                CategoryId = 2
            },
            new Product
            {
                ProductId = 4,
                ProductName = "T-Shirt",
                Price = 499,
                Quantity = 100,
                IsActive = true,
                CategoryId = 3
            }
        );
    }
}