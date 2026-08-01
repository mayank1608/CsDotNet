using EcommWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EcommWebAPI.Models
{
    public class EcommDbContext : DbContext
    {
        public EcommDbContext(DbContextOptions<EcommDbContext> options) : base(options)
        {
        }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<UserAccount> UserAccounts { get; set; }
    }
}
