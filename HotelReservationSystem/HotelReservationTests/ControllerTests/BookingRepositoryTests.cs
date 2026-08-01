using HotelReservationWebApp.BLRepository;
using HotelReservationWebApp.Controllers;
using HotelReservationWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace HotelReservationTests.ControllerTests
{
    public class BookingRepositoryTests
    {
        private readonly BookingController _controller;
        private readonly Mock<IBookingBRepository> _mockBL;

        public BookingRepositoryTests()
        {
            _mockBL = new Mock<IBookingBRepository>();
            _controller = new BookingController(_mockBL.Object);
        }

        // ✅ GROUP 1: GetAllBookings (4 tests)

        [Fact]
        public async Task GetAllBookings_ShouldReturnOk()
        {
            var data = new List<BookingDetailsMapper> { new BookingDetailsMapper() };

            _mockBL.Setup(x => x.GetAllBookings()).ReturnsAsync(data);

            var result = await _controller.GetAllBookings();

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetAllBookings_ShouldReturnNotFound()
        {
            _mockBL.Setup(x => x.GetAllBookings())
                   .ReturnsAsync(new List<BookingDetailsMapper>());

            var result = await _controller.GetAllBookings();

            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetAllBookings_ShouldReturnMultipleRecords()
        {
            var data = new List<BookingDetailsMapper>
            {
                new BookingDetailsMapper(),
                new BookingDetailsMapper()
            };

            _mockBL.Setup(x => x.GetAllBookings()).ReturnsAsync(data);

            var result = await _controller.GetAllBookings();
            var ok = result.Result as OkObjectResult;

            Assert.Equal(2, ((List<BookingDetailsMapper>)ok.Value).Count);
        }

        

        // ✅ GROUP 2: AddBooking (5 tests)

        [Fact]
        public async Task AddBooking_ShouldReturnSuccess()
        {
            _mockBL.Setup(x => x.AddBooking(It.IsAny<Booking>()))
                   .ReturnsAsync("Booking Added Successfully");

            var result = await _controller.AddBooking(new Booking());

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task AddBooking_ShouldReturnInvalidStatus()
        {
            _mockBL.Setup(x => x.AddBooking(It.IsAny<Booking>()))
                   .ReturnsAsync("Invalid Status");

            var result = await _controller.AddBooking(new Booking());

            var ok = result.Result as OkObjectResult;
            Assert.Equal("Invalid Status", ok.Value);
        }

        [Fact]
        public async Task AddBooking_ShouldReturnRoomNotAvailable()
        {
            _mockBL.Setup(x => x.AddBooking(It.IsAny<Booking>()))
                   .ReturnsAsync("Room Not Available");

            var result = await _controller.AddBooking(new Booking());

            var ok = result.Result as OkObjectResult;
            Assert.Equal("Room Not Available", ok.Value);
        }

        [Fact]
        public async Task AddBooking_ShouldHandleNullInput()
        {
            _mockBL.Setup(x => x.AddBooking(null))
                   .ReturnsAsync("Invalid");

            var result = await _controller.AddBooking(null);

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task AddBooking_ShouldHandleException()
        {
            _mockBL.Setup(x => x.AddBooking(It.IsAny<Booking>()))
                   .ThrowsAsync(new Exception());

            var result = await _controller.AddBooking(new Booking());

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }


        // ✅ GROUP 3: UpdateBookingStatus (5 tests)

        [Fact]
        public async Task UpdateBookingStatus_ShouldReturnSuccess()
        {
            _mockBL.Setup(x => x.UpdateBookingStatus(1, "Canceled"))
                   .ReturnsAsync("Booking Status Updated Successfully");

            var result = await _controller.UpdateBookingStatus(1, "Canceled");

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task UpdateBookingStatus_ShouldReturnNotFound()
        {
            _mockBL.Setup(x => x.UpdateBookingStatus(1, "Canceled"))
                   .ReturnsAsync("Booking Not Found");

            var result = await _controller.UpdateBookingStatus(1, "Canceled");

            var ok = result.Result as OkObjectResult;
            Assert.Equal("Booking Not Found", ok.Value);
        }

        [Fact]
        public async Task UpdateBookingStatus_ShouldReturnInvalidStatus()
        {
            _mockBL.Setup(x => x.UpdateBookingStatus(1, "Wrong"))
                   .ReturnsAsync("Invalid Status");

            var result = await _controller.UpdateBookingStatus(1, "Wrong");

            var ok = result.Result as OkObjectResult;
            Assert.Equal("Invalid Status", ok.Value);
        }

        [Fact]
        public async Task UpdateBookingStatus_ShouldFail_WhenCanceledToConfirmed()
        {
            _mockBL.Setup(x => x.UpdateBookingStatus(1, "Confirmed"))
                   .ReturnsAsync("Cannot confirm a canceled booking");

            var result = await _controller.UpdateBookingStatus(1, "Confirmed");

            var ok = result.Result as OkObjectResult;
            Assert.Equal("Cannot confirm a canceled booking", ok.Value);
        }

        [Fact]
        public async Task UpdateBookingStatus_ShouldFail_WhenPastBookingCancel()
        {
            _mockBL.Setup(x => x.UpdateBookingStatus(1, "Canceled"))
                   .ReturnsAsync("Cannot cancel past booking");

            var result = await _controller.UpdateBookingStatus(1, "Canceled");

            var ok = result.Result as OkObjectResult;
            Assert.Equal("Cannot cancel past booking", ok.Value);
        }


        // ✅ GROUP 4: GetAvailableHotels (3 tests)

        [Fact]
        public async Task GetAvailableHotels_ShouldReturnOk()
        {
            _mockBL.Setup(x => x.GetAvailableHotels(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                   .ReturnsAsync(new List<HotelRoomMapper> { new HotelRoomMapper() });

            var result = await _controller.GetAvailableHotels(DateTime.Now, DateTime.Now.AddDays(1));

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetAvailableHotels_ShouldReturnNotFound()
        {
            _mockBL.Setup(x => x.GetAvailableHotels(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                   .ReturnsAsync(new List<HotelRoomMapper>());

            var result = await _controller.GetAvailableHotels(DateTime.Now, DateTime.Now);

            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        

        // ✅ GROUP 5: AddRoom (3 tests)

        [Fact]
        public async Task AddRoom_ShouldReturnTrue()
        {
            _mockBL.Setup(x => x.AddRoom(It.IsAny<Room>()))
                   .ReturnsAsync(true);

            var result = await _controller.AddRoom(new Room());

            var ok = result.Result as OkObjectResult;
            Assert.True((bool)ok.Value);
        }

        [Fact]
        public async Task AddRoom_ShouldReturnFalse()
        {
            _mockBL.Setup(x => x.AddRoom(It.IsAny<Room>()))
                   .ReturnsAsync(false);

            var result = await _controller.AddRoom(new Room());

            var ok = result.Result as OkObjectResult;
            Assert.False((bool)ok.Value);
        }
    }
}