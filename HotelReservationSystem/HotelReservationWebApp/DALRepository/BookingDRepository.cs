using HotelReservationWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationWebApp.DALRepository
{
    public class BookingDRepository : IBookingDRepository
    {
        private readonly HotelDbContext _context;

        public BookingDRepository(HotelDbContext context)
        {
            _context = context;
        }


        public async Task<List<BookingDetailsMapper>> GetAllBookings()
        {
            try
            {
                var query = from b in _context.Bookings
                            join c in _context.Customers
                                on b.CustomerId equals c.Id
                            join r in _context.Rooms on b.RoomId equals r.Id
                            join h in _context.Hotels on r.HotelId equals h.Id
                            where b.Status == "Confirmed"
                               || b.Status == "Pending"
                               || b.Status == "Canceled"
                            select new BookingDetailsMapper
                            {

                                BookingId = b.Id,
                                CustomerId = c.Id,
                                CustomerName = c.Name,

                                RoomId = r.Id,
                                RoomType = r.Type,

                                HotelName = h.Name,
                                Location = h.Location,

                                CheckInDate = b.CheckInDate,
                                CheckOutDate = b.CheckOutDate,

                                Status = b.Status

                            };

                return await query.ToListAsync();
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<List<HotelRoomMapper>> GetAvailableHotels(DateTime checkIn, DateTime checkOut)
        {
            try
            {

                var query = from h in _context.Hotels
                            join r in _context.Rooms on h.Id equals r.HotelId
                            where !_context.Bookings.Any(b =>
                                b.RoomId == r.Id &&
                                (b.Status == "Confirmed" || b.Status == "Pending") &&
                                checkIn < b.CheckOutDate &&
                                checkOut > b.CheckInDate
                            )
                            select new HotelRoomMapper
                            {
                                HotelId = h.Id,
                                HotelName = h.Name,
                                Location = h.Location,
                                RoomId = r.Id,
                                RoomNumber = r.RoomNumber,
                                Type = r.Type,
                                PricePerNight = r.PricePerNight
                            };

                return await query.ToListAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<bool> AddRoom(Room room)
        {
            try
            {
                await _context.Rooms.AddAsync(room);
                await _context.SaveChangesAsync();
                Console.WriteLine("Room Added Successfully");

                return true;
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
                if (booking.Status != "Confirmed" && booking.Status != "Pending" && booking.Status != "Canceled")                
                    return "Invalid Status";
                

                bool isAvailable = await _context.Bookings.AnyAsync(b =>
                    b.RoomId == booking.RoomId &&
                    (b.Status == "Confirmed" || b.Status == "Pending") &&
                    booking.CheckInDate < b.CheckOutDate &&
                    booking.CheckOutDate > b.CheckInDate);

                if (isAvailable)
                {
                    return "Room Not Available";
                }

                await _context.Bookings.AddAsync(booking);
                await _context.SaveChangesAsync();

                return "Booking Added Successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> UpdateBookingStatus(int bookingId, string status)
        {
            try
            {
                if (status != "Confirmed" && status != "Pending" &&  status != "Canceled")
                    return "Invalid Status";               

                var booking = await _context.Bookings.FindAsync(bookingId);

                if (booking == null)
                    return "Booking Not Found";

                if (booking.Status == "Canceled" && status == "Confirmed")
                    return "Cannot confirm a canceled booking";


                if (status == "Canceled" && booking.CheckOutDate < DateTime.Now)
                    return "Cannot cancel past booking";


                booking.Status = status;
                await _context.SaveChangesAsync();

                return "Booking Status Updated Successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}
