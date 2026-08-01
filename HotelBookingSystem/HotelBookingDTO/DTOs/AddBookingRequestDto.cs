using HotelBookingDTO.Enums;

namespace HotelBookingDTO.DTOs;

public class AddBookingRequestDto
{
    public int CustomerId { get; set; }

    public int RoomId { get; set; }

    public DateTime CheckInDate { get; set; }

    public DateTime CheckOutDate { get; set; }

    public BookingStatus Status { get; set; }
}