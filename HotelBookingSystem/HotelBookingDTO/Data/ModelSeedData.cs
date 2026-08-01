using HotelBookingDTO.Enums;
using HotelBookingDTO.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingDTO.Data;

public static class ModelSeedData
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Hotel>().HasData(
            new Hotel
            {
                Id = 1,
                Name = "Grand Hotel",
                Location = "New York"
            },
            new Hotel
            {
                Id = 2,
                Name = "New York",
                Location = "California"
            },
            new Hotel
            {
                Id = 3,
                Name = "Mountain Inn",
                Location = "Colorado"
            }
        );

        modelBuilder.Entity<Room>().HasData(
            new Room
            {
                Id = 1,
                HotelId = 1,
                RoomNumber = "101",
                Type = "Single",
                PricePerNight = 100
            },
            new Room
            {
                Id = 2,
                HotelId = 1,
                RoomNumber = "102",
                Type = "Double",
                PricePerNight = 150
            },
            new Room
            {
                Id = 3,
                HotelId = 2,
                RoomNumber = "201",
                Type = "Suite",
                PricePerNight = 250
            },
            new Room
            {
                Id = 4,
                HotelId = 2,
                RoomNumber = "202",
                Type = "Single",
                PricePerNight = 120
            },
            new Room
            {
                Id = 5,
                HotelId = 3,
                RoomNumber = "301",
                Type = "Double",
                PricePerNight = 180
            },
            new Room
            {
                Id = 6,
                HotelId = 3,
                RoomNumber = "302",
                Type = "Suite",
                PricePerNight = 300
            }
        );

        modelBuilder.Entity<Customer>().HasData(
            new Customer
            {
                Id = 1,
                Name = "John Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "123-456-7890"
            },
            new Customer
            {
                Id = 2,
                Name = "Jane Smith",
                Email = "jane.smith@example.com",
                PhoneNumber = "987-654-3210"
            },
            new Customer
            {
                Id = 3,
                Name = "Alice Johnson",
                Email = "alice.johnson@example.com",
                PhoneNumber = "555-123-4567"
            }
        );

        modelBuilder.Entity<Booking>().HasData(
            new Booking
            {
                Id = 1,
                CustomerId = 1,
                RoomId = 1,
                CheckInDate = new DateTime(2026, 8, 1),
                CheckOutDate = new DateTime(2026, 8, 5),
                Status = BookingStatus.Confirmed
            },
            new Booking
            {
                Id = 2,
                CustomerId = 2,
                RoomId = 3,
                CheckInDate = new DateTime(2026, 8, 10),
                CheckOutDate = new DateTime(2026, 8, 12),
                Status = BookingStatus.Pending
            },
            new Booking
            {
                Id = 3,
                CustomerId = 3,
                RoomId = 5,
                CheckInDate = new DateTime(2026, 8, 15),
                CheckOutDate = new DateTime(2026, 8, 20),
                Status = BookingStatus.Canceled
            }
        );
    }
}