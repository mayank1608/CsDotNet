
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBookingDTO.Models;
public class Room
{
    public int Id { get; set; }

    public int HotelId { get; set; }

    [Required]
    [MaxLength(20)]
    public string RoomNumber { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Type { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18,2)")]
    public decimal PricePerNight { get; set; }

    public Hotel Hotel { get; set; } = null!;

    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
