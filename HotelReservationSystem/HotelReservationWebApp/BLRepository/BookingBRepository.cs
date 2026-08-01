using HotelReservationWebApp.DALRepository;
using HotelReservationWebApp.Models;

namespace HotelReservationWebApp.BLRepository
{
    public class BookingBRepository : IBookingBRepository
    {
        private readonly IBookingDRepository _repository;

        public BookingBRepository(IBookingDRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<BookingDetailsMapper>> GetAllBookings()
        {
            try
            {
                return await _repository.GetAllBookings();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<List<HotelRoomMapper>> GetAvailableHotels(DateTime checkIn, DateTime checkOut)
        {
            try
            {
                return await _repository.GetAvailableHotels(checkIn, checkOut);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }



        public async Task<bool> AddRoom(Room room)
        {
            try
            {
                return await _repository.AddRoom(room);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        public async Task<string> AddBooking(Booking booking)
        {
            try
            {
                return await _repository.AddBooking(booking);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        public async Task<string> UpdateBookingStatus(int bookingId, string status)
        {
            try
            {
                return await _repository.UpdateBookingStatus(bookingId, status);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }




    }
}
