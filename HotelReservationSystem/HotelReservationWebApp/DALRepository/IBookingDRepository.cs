using HotelReservationWebApp.Models;

namespace HotelReservationWebApp.DALRepository
{
    public interface IBookingDRepository
    {

        public Task<List<BookingDetailsMapper>> GetAllBookings();

        public Task<List<HotelRoomMapper>> GetAvailableHotels(DateTime checkIn, DateTime checkOut);
        public Task<bool> AddRoom(Room room);
        public Task<string> AddBooking(Booking booking);
        public Task<string> UpdateBookingStatus(int bookingId, string status);

    }
}
