// namespace GoodReadsWebApp;

// public class AppDbContext
// {

// }

using GoodReadsWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace GoodReadsWebApp.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // -------------------------------
            // Table Relationships
            // -------------------------------
            // Author
            modelBuilder.Entity<Author>()
            .HasKey(a => a.Author_id);

            // Book
            modelBuilder.Entity<Book>()
            .HasKey(b => b.Book_isbn);

            // Review
            modelBuilder.Entity<Review>()
            .HasKey(r => r.Review_id);

            // Relationships
            modelBuilder.Entity<Book>()
            .HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.Author_id);

            modelBuilder.Entity<Review>()
            .HasOne(r => r.Book)
            .WithMany(b => b.Reviews)
            .HasForeignKey(r => r.Book_isbn);

            // ====================================
            // SEED DATA
            // ====================================
            modelBuilder.Entity<Author>().HasData(
                new Author
                {
                    Author_id = 1,
                    Author_name = "Robert Martin"
                },
                new Author
                {
                    Author_id = 2,
                    Author_name = "Martin Fowler"
                },
                new Author
                {
                    Author_id = 3,
                    Author_name = "Andrew Hunt"
                }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Book_isbn = 1001,
                    Title = "Clean Code",
                    Book_edition = 1,
                    Author_id = 1
                },
                new Book
                {
                    Book_isbn = 1002,
                    Title = "Clean Architecture",
                    Book_edition = 1,
                    Author_id = 1
                },
                new Book
                {
                    Book_isbn = 1003,
                    Title = "Refactoring",
                    Book_edition = 2,
                    Author_id = 2
                },
                new Book
                {
                    Book_isbn = 1004,
                    Title = "The Pragmatic Programmer",
                    Book_edition = 2,
                    Author_id = 3
                }
            );

            modelBuilder.Entity<Review>().HasData(
                new Review
                {
                    Review_id = 1,
                    Book_isbn = 1001,
                    Rating = 5,
                    ReviewText = "Excellent book for developers"
                },
                new Review
                {
                    Review_id = 2,
                    Book_isbn = 1001,
                    Rating = 4,
                    ReviewText = "Very informative"
                },
                new Review
                {
                    Review_id = 3,
                    Book_isbn = 1003,
                    Rating = 5,
                    ReviewText = "Must read for legacy code"
                },
                new Review
                {
                    Review_id = 4,
                    Book_isbn = 1004,
                    Rating = 4,
                    ReviewText = "Good practical examples"
                }
            );
        }
    }
}
