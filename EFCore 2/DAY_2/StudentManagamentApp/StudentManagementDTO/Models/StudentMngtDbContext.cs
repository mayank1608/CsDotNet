using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementDTO.Models
{
    public class StudentMngtDbContext:DbContext
    {
        private readonly string _dbConnectionString;
        public DbSet<Students> Students { get; set; }
        public DbSet<Grades> Grades { get; set; }
        
        public StudentMngtDbContext()
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
            modelBuilder.Entity<Grades>()
            .HasData(
               new Grades { GradeId = 1,GradeName="1st", Section = "A" },
               new Grades { GradeId = 2, GradeName = "2nd", Section = "A" },
               new Grades { GradeId = 3, GradeName = "3rd", Section = "A" },
               new Grades { GradeId = 4, GradeName = "1st", Section = "B" }
              );
        }
    }
}
