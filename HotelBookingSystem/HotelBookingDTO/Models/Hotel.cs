using System.ComponentModel.DataAnnotations;

namespace HotelBookingDTO.Models;

public class Hotel
{
    public int Id { get; set; }

    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    public string Location { get; set; } = string.Empty;

    public ICollection<Room> Rooms { get; set; } = new List<Room>();
}