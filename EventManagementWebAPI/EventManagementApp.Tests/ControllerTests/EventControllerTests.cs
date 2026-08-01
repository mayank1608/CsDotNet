using EventManagementApp.BLRepo;
using EventManagementApp.Controllers;
using EventManagementApp.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EventManagementApp.Tests.ControllerTests
{
    public class EventControllerTests
    {
        private readonly Mock<IEventBL> _eventBLMock;
        private readonly EventController _controller;

        public EventControllerTests()
        {
            _eventBLMock = new Mock<IEventBL>();

            _controller =
                new EventController(_eventBLMock.Object);
        }

        [Fact]
        public async Task GetEventsByCity_ReturnsOkResult()
        {
            // Arrange

            var data =
                new List<EventByCityResponse>
                {
                    new EventByCityResponse
                    {
                        EventType = "Conference",
                        OrganizerName = "Organizer1",
                        EventLocation = "Radison Blu Hotel"
                    }
                };

            _eventBLMock
                .Setup(x => x.GetEventsByCityAsync("Mysore"))
                .ReturnsAsync(data);

            // Act

            var result =
                await _controller.GetEventsByCity("Mysore");

            // Assert

            var okResult =
                Assert.IsType<OkObjectResult>(result);

            var returnedData =
                Assert.IsAssignableFrom<List<EventByCityResponse>>
                (
                    okResult.Value
                );

            Assert.Single(returnedData);
        }

        [Fact]
        public async Task GetEventsByCity_ReturnsNotFound()
        {
            // Arrange

            _eventBLMock
                .Setup(x => x.GetEventsByCityAsync("XYZ"))
                .ThrowsAsync(new Exception("No events found"));

            // Act

            var result =
                await _controller.GetEventsByCity("XYZ");

            // Assert

            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}