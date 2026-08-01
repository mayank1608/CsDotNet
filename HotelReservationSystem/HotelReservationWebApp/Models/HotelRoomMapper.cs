namespace HotelReservationWebApp.Models
{
    public class HotelRoomMapper
    {
        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public string Location { get; set; }

        public int RoomId { get; set; }
        public string RoomNumber { get; set; }
        public string Type { get; set; }
        public decimal PricePerNight { get; set; }

    }
}
