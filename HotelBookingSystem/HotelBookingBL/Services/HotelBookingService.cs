using HotelBookingBL.Interfaces;
using HotelBookingDAL.Interfaces;
using HotelBookingDTO.DTOs;
using HotelBookingDTO.Enums;
using HotelBookingDTO.Models;

namespace HotelBookingBL.Services;

public class HotelBookingService : IHotelBookingService
{
    private readonly IHotelBookingRepository hotelBookingRepository;

    public HotelBookingService(IHotelBookingRepository hotelBookingRepository)
    {
        this.hotelBookingRepository = hotelBookingRepository;
    }

    public async Task<List<Booking>> GetBookingsByStatusAsync(BookingStatus status)
    {
        try
        {
            return await hotelBookingRepository.GetBookingsByStatusAsync(status);
        }
        catch (Exception exception)
        {
            throw new ApplicationException("Business layer error while fetching bookings.", exception);
        }
    }

    public async Task<List<Hotel>> GetAvailableHotelsAsync(DateTime checkInDate, DateTime checkOutDate)
    {
        try
        {
            if (checkInDate >= checkOutDate)
            {
                throw new ArgumentException("Check-out date must be greater than check-in date.");
            }

            return await hotelBookingRepository.GetAvailableHotelsAsync(checkInDate, checkOutDate);
        }
        catch (Exception exception)
        {
            throw new ApplicationException("Business layer error while fetching available hotels.", exception);
        }
    }

    public async Task<Room> AddRoomAsync(AddRoomRequestDto addRoomRequestDto)
    {
        try
        {
            if (addRoomRequestDto.HotelId <= 0)
            {
                throw new ArgumentException("Hotel ID must be valid.");
            }

            if (string.IsNullOrWhiteSpace(addRoomRequestDto.RoomNumber))
            {
                throw new ArgumentException("Room number is required.");
            }

            if (addRoomRequestDto.PricePerNight <= 0)
            {
                throw new ArgumentException("Price per night must be greater than zero.");
            }

            return await hotelBookingRepository.AddRoomAsync(addRoomRequestDto);
        }
        catch (Exception exception)
        {
            throw new ApplicationException("Business layer error while adding a room.", exception);
        }
    }

    public async Task<Booking> AddBookingAsync(AddBookingRequestDto addBookingRequestDto)
    {
        try
        {
            if (addBookingRequestDto.CustomerId <= 0)
            {
                throw new ArgumentException("Customer ID must be valid.");
            }

            if (addBookingRequestDto.RoomId <= 0)
            {
                throw new ArgumentException("Room ID must be valid.");
            }

            if (addBookingRequestDto.CheckInDate >= addBookingRequestDto.CheckOutDate)
            {
                throw new ArgumentException("Check-out date must be greater than check-in date.");
            }

            return await hotelBookingRepository.AddBookingAsync(addBookingRequestDto);
        }
        catch (Exception exception)
        {
            throw new ApplicationException("Business layer error while adding a booking.", exception);
        }
    }

    public async Task<bool> CancelBookingAsync(int bookingId)
    {
        try
        {
            if (bookingId <= 0)
            {
                throw new ArgumentException("Booking ID must be valid.");
            }

            return await hotelBookingRepository.CancelBookingAsync(bookingId);
        }
        catch (Exception exception)
        {
            throw new ApplicationException("Business layer error while canceling booking.", exception);
        }
    }
}