using HotelBookingDTO.Enums;

namespace HotelBookingDTO.Models;

public class Booking
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int RoomId { get; set; }

    public DateTime CheckInDate { get; set; }

    public DateTime CheckOutDate { get; set; }

    public BookingStatus Status { get; set; }

    public Customer Customer { get; set; } = null!;

    public Room Room { get; set; } = null!;
}