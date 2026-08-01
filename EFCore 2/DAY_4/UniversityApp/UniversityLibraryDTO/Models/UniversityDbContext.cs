using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityLibraryDTO.Models
{
    public class UniversityDbContext : DbContext
    {
        private string connectionString;
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }

        public UniversityDbContext()
        {
            connectionString = ConfigurationManager.ConnectionStrings["ConnectString"].ConnectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=(localdb)\\ProjectModels;Database=UniversityDb;Trusted_Connection=True;");
            optionsBuilder.UseSqlServer(connectionString);
            //optionsBuilder.LogTo(Console.WriteLine);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
            .HasData(
               new Department { DepartmentId = 1, DepartmentName = "HR" },
               new Department { DepartmentId = 2, DepartmentName = "Admin" },
               new Department { DepartmentId = 3, DepartmentName = "Production" });
        }
    }
}
