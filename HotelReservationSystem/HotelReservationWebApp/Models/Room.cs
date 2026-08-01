using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HotelReservationWebApp.Models
{
    public class Room
    {
        [Key]
        //[JsonIgnore]
        public int Id { get; set; }
        [ForeignKey("Hotel")]
        public int HotelId { get; set; }
        public string RoomNumber { get; set; }
        public string Type { get; set; }
        public decimal PricePerNight { get; set; }

        //[JsonIgnore]
        //public Hotel? Hotel { get; set; }


        [JsonIgnore]
        public ICollection<Booking>? Bookings { get; set; }

        //[JsonIgnore]
        //public List<Booking>? Bookings { get; set; }
    }
}
