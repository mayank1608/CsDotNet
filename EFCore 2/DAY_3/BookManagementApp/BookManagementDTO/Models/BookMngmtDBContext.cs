using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace BookManagementDTO.Models
{
    public class BookMngmtDBContext:DbContext
    {
        private readonly string _dbConnectionString;
        public DbSet<Authors> Authors { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public BookMngmtDBContext()
        {
            _dbConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(_dbConnectionString);
            //optionsBuilder.LogTo(Console.WriteLine);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Authors>()
            .HasData(
               new Authors { AuthorID = "A100", AuthorName = "Rajat"},
                new Authors { AuthorID = "A101", AuthorName = "Raj" },
                 new Authors { AuthorID = "A102", AuthorName = "Ved" },
                  new Authors { AuthorID = "A103", AuthorName = "Kavya" }
              );

            modelBuilder.Entity<Reviews>()
            .Property(s => s.ReviewID)
            .UseIdentityColumn(500, 2);
        }
    }
}
