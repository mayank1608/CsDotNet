using HotelBookingDAL.Interfaces;
using HotelBookingDTO.Data;
using HotelBookingDTO.DTOs;
using HotelBookingDTO.Enums;
using HotelBookingDTO.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingDAL.Repositories;

public class HotelBookingRepository : IHotelBookingRepository
{
    private readonly HotelBookingDbContext hotelBookingDbContext;

    public HotelBookingRepository(HotelBookingDbContext hotelBookingDbContext)
    {
        this.hotelBookingDbContext = hotelBookingDbContext;
    }

    public async Task<List<Booking>> GetBookingsByStatusAsync(BookingStatus status)
    {
        try
        {
            return await hotelBookingDbContext.Bookings
                .Include(booking => booking.Customer)
                .Include(booking => booking.Room)
                .ThenInclude(room => room.Hotel)
                .Where(booking => booking.Status == status)
                .ToListAsync();
        }
        catch (Exception exception)
        {
            throw new ApplicationException("An error occurred while fetching bookings by status.", exception);
        }
    }

    public async Task<List<Hotel>> GetAvailableHotelsAsync(DateTime checkInDate, DateTime checkOutDate)
    {
        try
        {
            return await hotelBookingDbContext.Hotels
                .Include(hotel => hotel.Rooms)
                .ThenInclude(room => room.Bookings)
                .Where(hotel => hotel.Rooms.Any(room =>
                    !room.Bookings.Any(booking =>
                        booking.Status == BookingStatus.Confirmed &&
                        checkInDate < booking.CheckOutDate &&
                        checkOutDate > booking.CheckInDate)))
                .ToListAsync();
        }
        catch (Exception exception)
        {
            throw new ApplicationException("An error occurred while fetching available hotels.", exception);
        }
    }

    public async Task<Room> AddRoomAsync(AddRoomRequestDto addRoomRequestDto)
    {
        try
        {
            Room room = new()
            {
                HotelId = addRoomRequestDto.HotelId,
                RoomNumber = addRoomRequestDto.RoomNumber,
                Type = addRoomRequestDto.Type,
                PricePerNight = addRoomRequestDto.PricePerNight
            };

            await hotelBookingDbContext.Rooms.AddAsync(room);
            await hotelBookingDbContext.SaveChangesAsync();

            return room;
        }
        catch (Exception exception)
        {
            throw new ApplicationException("An error occurred while adding a room.", exception);
        }
    }

    public async Task<Booking> AddBookingAsync(AddBookingRequestDto addBookingRequestDto)
    {
        try
        {
            bool isRoomAlreadyBooked = await hotelBookingDbContext.Bookings
                .AnyAsync(booking =>
                    booking.RoomId == addBookingRequestDto.RoomId &&
                    booking.Status == BookingStatus.Confirmed &&
                    addBookingRequestDto.CheckInDate < booking.CheckOutDate &&
                    addBookingRequestDto.CheckOutDate > booking.CheckInDate);

            if (isRoomAlreadyBooked)
            {
                throw new InvalidOperationException("Room is already booked for the selected date range.");
            }

            Booking booking = new()
            {
                CustomerId = addBookingRequestDto.CustomerId,
                RoomId = addBookingRequestDto.RoomId,
                CheckInDate = addBookingRequestDto.CheckInDate,
                CheckOutDate = addBookingRequestDto.CheckOutDate,
                Status = addBookingRequestDto.Status
            };

            await hotelBookingDbContext.Bookings.AddAsync(booking);
            await hotelBookingDbContext.SaveChangesAsync();

            return booking;
        }
        catch (Exception exception)
        {
            throw new ApplicationException("An error occurred while adding a booking.", exception);
        }
    }

    public async Task<bool> CancelBookingAsync(int bookingId)
    {
        try
        {
            Booking? booking = await hotelBookingDbContext.Bookings
                .FirstOrDefaultAsync(existingBooking => existingBooking.Id == bookingId);

            if (booking is null)
            {
                return false;
            }

            booking.Status = BookingStatus.Canceled;

            await hotelBookingDbContext.SaveChangesAsync();

            return true;
        }
        catch (Exception exception)
        {
            throw new ApplicationException("An error occurred while canceling the booking.", exception);
        }
    }
}