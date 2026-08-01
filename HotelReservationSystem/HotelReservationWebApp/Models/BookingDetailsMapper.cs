namespace HotelReservationWebApp.Models
{
    public class BookingDetailsMapper
    {
        public int BookingId { get; set; }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }

        public int RoomId { get; set; }
        public string RoomType { get; set; }

        public string HotelName { get; set; }
        public string Location { get; set; }

        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public string Status { get; set; }

    }
}
