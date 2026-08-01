using System.Configuration;
using HotelBookingDTO.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingDTO.Models;

public class HotelBookingDbContext : DbContext
{
    private readonly string _dbConnectionString;    
    public DbSet<Hotel> Hotels { get; set; }

    public DbSet<Room> Rooms { get; set; }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<Booking> Bookings { get; set; }

    public HotelBookingDbContext()
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
        base.OnModelCreating(modelBuilder);

        ConfigureHotel(modelBuilder);
        ConfigureRoom(modelBuilder);
        ConfigureCustomer(modelBuilder);
        ConfigureBooking(modelBuilder);

        ModelSeedData.Seed(modelBuilder);
    }


    private static void ConfigureHotel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Hotel>()
            .HasMany(hotel => hotel.Rooms)
            .WithOne(room => room.Hotel)
            .HasForeignKey(room => room.HotelId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private static void ConfigureRoom(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Room>()
            .Property(room => room.PricePerNight)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Room>()
            .HasIndex(room => new { room.HotelId, room.RoomNumber })
            .IsUnique();
    }

    private static void ConfigureCustomer(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>()
            .HasIndex(customer => customer.Email)
            .IsUnique();

        modelBuilder.Entity<Customer>()
            .HasMany(customer => customer.Bookings)
            .WithOne(booking => booking.Customer)
            .HasForeignKey(booking => booking.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    private static void ConfigureBooking(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>()
            .HasOne(booking => booking.Room)
            .WithMany(room => room.Bookings)
            .HasForeignKey(booking => booking.RoomId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Booking>()
            .Property(booking => booking.Status)
            .HasConversion<string>()
            .HasMaxLength(20);
    }
}