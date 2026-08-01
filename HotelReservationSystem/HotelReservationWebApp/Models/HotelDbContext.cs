using Microsoft.EntityFrameworkCore;

namespace HotelReservationWebApp.Models
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext(){ }

        public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options)
        {

        }


        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Booking> Bookings { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            modelBuilder.Entity<Room>()
                    .HasOne<Hotel>()
                    .WithMany(h => h.Rooms)
                    .HasForeignKey(r => r.HotelId);

            modelBuilder.Entity<Booking>()
                .HasOne<Customer>()
                .WithMany(c => c.Bookings)
                .HasForeignKey(b => b.CustomerId);

            modelBuilder.Entity<Booking>()
                .HasOne<Room>()
                .WithMany(r => r.Bookings)
                .HasForeignKey(b => b.RoomId);




            modelBuilder.Entity<Booking>()
                .HasCheckConstraint(
                    "CHK_Booking_Status",
                    "[Status] IN ('Confirmed', 'Pending', 'Canceled')"
                );


        }


    }
}
