using HotelBookingDTO.DTOs;
using HotelBookingDTO.Enums;
using HotelBookingDTO.Models;

namespace HotelBookingBL.Interfaces;

public interface IHotelBookingService
{
    Task<List<Booking>> GetBookingsByStatusAsync(BookingStatus status);

    Task<List<Hotel>> GetAvailableHotelsAsync(DateTime checkInDate, DateTime checkOutDate);

    Task<Room> AddRoomAsync(AddRoomRequestDto addRoomRequestDto);

    Task<Booking> AddBookingAsync(AddBookingRequestDto addBookingRequestDto);

    Task<bool> CancelBookingAsync(int bookingId);
}