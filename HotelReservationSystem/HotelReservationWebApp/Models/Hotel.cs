using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HotelReservationWebApp.Models
{
    public class Hotel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        [JsonIgnore]
        public ICollection<Room>? Rooms { get; set; }


        //public List<Room>? Rooms { get; set; }

    }
}
