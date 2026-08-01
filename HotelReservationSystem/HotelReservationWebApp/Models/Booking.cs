using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HotelReservationWebApp.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Customer"), JsonIgnore]
        public int CustomerId { get; set; }
        [ForeignKey("Room"), JsonIgnore]
        public int RoomId { get; set; }

        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string Status { get; set; } = "Pending";

        //[JsonIgnore]
        //public Customer? Customer { get; set; }
        //[JsonIgnore]
        //public Room? Room { get; set; }
    }
}
