using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HotelReservationWebApp.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        [JsonIgnore]
        public ICollection<Booking>? Bookings { get; set; }

    }
}
