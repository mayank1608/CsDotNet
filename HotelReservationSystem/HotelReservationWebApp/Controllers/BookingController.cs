using HotelReservationWebApp.BLRepository;
using HotelReservationWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : Controller
    {
        private readonly IBookingBRepository _bookingBRepository;

        public BookingController(IBookingBRepository bookingBRepository)
        {
            _bookingBRepository = bookingBRepository;
        }


        [HttpGet("GetAllBookings")]
        public async Task<ActionResult<List<BookingDetailsMapper>>> GetAllBookings()
        {
            try
            {
                var result = await _bookingBRepository.GetAllBookings();
                if (result == null || result.Count == 0)
                    return NotFound("No Booking Found");
                return Ok(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Internal Server Error!");
            }
        }


        [HttpGet("AvailableHotels")]
        public async Task<ActionResult<List<HotelRoomMapper>>> GetAvailableHotels(DateTime checkIn, DateTime checkOut)
        {
            try
            {
                var result = await _bookingBRepository.GetAvailableHotels(checkIn, checkOut);

                if (result == null || result.Count == 0)
                    return NotFound("No hotels available");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Internal Server Error!");
            }
        }

        [HttpPost("AddRoom")]
        public async Task<ActionResult<bool>> AddRoom(Room room)
        {
            try
            {
                var result = await _bookingBRepository.AddRoom(room);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Internal Server Error!");
            }
        }


        [HttpPost("AddBooking")]
        public async Task<ActionResult<string>> AddBooking(Booking booking)
        {
            try
            {
                var result = await _bookingBRepository.AddBooking(booking);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPatch("UpdateBookingStatus/{bookingId}")]
        public async Task<ActionResult<string>> UpdateBookingStatus(int bookingId, string status)
        {
            try
            {
                var result = await _bookingBRepository.UpdateBookingStatus(bookingId, status);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
