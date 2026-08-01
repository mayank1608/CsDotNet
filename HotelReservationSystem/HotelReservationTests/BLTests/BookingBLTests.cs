using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelReservationWebApp.BLRepository;
using HotelReservationWebApp.DALRepository;
using HotelReservationWebApp.Models;

namespace HotelReservationTests.BLTests
{
    public class BookingBLTests
    {
        private readonly Mock<IBookingDRepository> _mockDAL;
        private readonly BookingBRepository _bl;

        public BookingBLTests()
        {
            _mockDAL = new Mock<IBookingDRepository>();
            _bl = new BookingBRepository(_mockDAL.Object);
        }

        // GetAllBookings - Success
        [Fact]
        public async Task GetAllBookings_ShouldReturnData()
        {
            var data = new List<BookingDetailsMapper>
            {
                new BookingDetailsMapper { BookingId = 1 }
            };

            _mockDAL.Setup(x => x.GetAllBookings()).ReturnsAsync(data);

            var result = await _bl.GetAllBookings();

            Assert.NotNull(result);
            Assert.Single(result);
        }

        // GetAllBookings - Empty
        [Fact]
        public async Task GetAllBookings_ShouldReturnEmpty()
        {
            _mockDAL.Setup(x => x.GetAllBookings())
                    .ReturnsAsync(new List<BookingDetailsMapper>());

            var result = await _bl.GetAllBookings();

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        

        // AddBooking - Success
        [Fact]
        public async Task AddBooking_ShouldReturnSuccess()
        {
            _mockDAL.Setup(x => x.AddBooking(It.IsAny<Booking>()))
                    .ReturnsAsync("Booking Added Successfully");

            var result = await _bl.AddBooking(new Booking());

            Assert.Equal("Booking Added Successfully", result);
        }

        // AddBooking - Invalid Status
        [Fact]
        public async Task AddBooking_ShouldReturnInvalidStatus()
        {
            _mockDAL.Setup(x => x.AddBooking(It.IsAny<Booking>()))
                    .ReturnsAsync("Invalid Status");

            var result = await _bl.AddBooking(new Booking());

            Assert.Equal("Invalid Status", result);
        }

        // AddBooking - Room Not Available
        [Fact]
        public async Task AddBooking_ShouldReturnRoomNotAvailable()
        {
            _mockDAL.Setup(x => x.AddBooking(It.IsAny<Booking>()))
                    .ReturnsAsync("Room Not Available");

            var result = await _bl.AddBooking(new Booking());

            Assert.Equal("Room Not Available", result);
        }

        

        // UpdateBookingStatus - Success
        [Fact]
        public async Task UpdateBookingStatus_ShouldReturnSuccess()
        {
            _mockDAL.Setup(x => x.UpdateBookingStatus(1, "Canceled"))
                    .ReturnsAsync("Booking Status Updated Successfully");

            var result = await _bl.UpdateBookingStatus(1, "Canceled");

            Assert.Equal("Booking Status Updated Successfully", result);
        }

        // UpdateBookingStatus - Not Found
        [Fact]
        public async Task UpdateBookingStatus_ShouldReturnNotFound()
        {
            _mockDAL.Setup(x => x.UpdateBookingStatus(1, "Canceled"))
                    .ReturnsAsync("Booking Not Found");

            var result = await _bl.UpdateBookingStatus(1, "Canceled");

            Assert.Equal("Booking Not Found", result);
        }

        // UpdateBookingStatus - Invalid Status
        [Fact]
        public async Task UpdateBookingStatus_ShouldReturnInvalidStatus()
        {
            _mockDAL.Setup(x => x.UpdateBookingStatus(1, "Wrong"))
                    .ReturnsAsync("Invalid Status");

            var result = await _bl.UpdateBookingStatus(1, "Wrong");

            Assert.Equal("Invalid Status", result);
        }

        // UpdateBookingStatus - Rule Fail
        [Fact]
        public async Task UpdateBookingStatus_ShouldFail_WhenCanceledToConfirmed()
        {
            _mockDAL.Setup(x => x.UpdateBookingStatus(1, "Confirmed"))
                    .ReturnsAsync("Cannot confirm a canceled booking");

            var result = await _bl.UpdateBookingStatus(1, "Confirmed");

            Assert.Equal("Cannot confirm a canceled booking", result);
        }

        // UpdateBookingStatus - Past Booking
        [Fact]
        public async Task UpdateBookingStatus_ShouldFail_WhenPastBookingCancel()
        {
            _mockDAL.Setup(x => x.UpdateBookingStatus(1, "Canceled"))
                    .ReturnsAsync("Cannot cancel past booking");

            var result = await _bl.UpdateBookingStatus(1, "Canceled");

            Assert.Equal("Cannot cancel past booking", result);
        }

        

        // GetAvailableHotels - Success
        [Fact]
        public async Task GetAvailableHotels_ShouldReturnData()
        {
            var data = new List<HotelRoomMapper>
            {
                new HotelRoomMapper { HotelId = 1 }
            };

            _mockDAL.Setup(x => x.GetAvailableHotels(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                    .ReturnsAsync(data);

            var result = await _bl.GetAvailableHotels(DateTime.Now, DateTime.Now.AddDays(1));

            Assert.NotNull(result);
        }

        // GetAvailableHotels - Empty
        [Fact]
        public async Task GetAvailableHotels_ShouldReturnEmpty()
        {
            _mockDAL.Setup(x => x.GetAvailableHotels(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                    .ReturnsAsync(new List<HotelRoomMapper>());

            var result = await _bl.GetAvailableHotels(DateTime.Now, DateTime.Now);

            Assert.Empty(result);
        }

        

        // AddRoom - Success
        [Fact]
        public async Task AddRoom_ShouldReturnTrue()
        {
            _mockDAL.Setup(x => x.AddRoom(It.IsAny<Room>()))
                    .ReturnsAsync(true);

            var result = await _bl.AddRoom(new Room());

            Assert.True(result);
        }

        //AddRoom - Failure
        [Fact]
        public async Task AddRoom_ShouldReturnFalse()
        {
            _mockDAL.Setup(x => x.AddRoom(It.IsAny<Room>()))
                    .ReturnsAsync(false);

            var result = await _bl.AddRoom(new Room());

            Assert.False(result);
        }

        

        //  General Null Handling
        [Fact]
        public async Task AddBooking_ShouldHandleNullInput()
        {
            _mockDAL.Setup(x => x.AddBooking(null))
                    .ReturnsAsync("Invalid");

            var result = await _bl.AddBooking(null);

            Assert.Equal("Invalid", result);
        }
    }
}