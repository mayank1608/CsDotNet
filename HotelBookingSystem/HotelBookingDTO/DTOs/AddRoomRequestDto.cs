namespace HotelBookingDTO.DTOs;

public class AddRoomRequestDto
{
    public int HotelId { get; set; }

    public string RoomNumber { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;

    public decimal PricePerNight { get; set; }
}