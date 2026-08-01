using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityDTO.Models
{
    public class UniversityDBContext : DbContext
    {
        private string _dbConnectionString;
        public DbSet<Students> Students { get; set; }
        public DbSet<Departments> Departments { get; set; }

        public UniversityDBContext() {
            _dbConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           // optionsBuilder.UseSqlServer("Server=(localdb)\\ProjectModels;Database=UniversityDb;Trusted_Connection=True;");
            optionsBuilder.UseSqlServer(_dbConnectionString);
            optionsBuilder.LogTo(Console.WriteLine);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Departments>()
            .HasData(
               new Departments { DepartmentId = 1, DepartmentName = "HR" },
               new Departments { DepartmentId = 2, DepartmentName = "Admin" },
               new Departments { DepartmentId = 3, DepartmentName = "Production" });
        }
    }
}
