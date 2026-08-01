using EventManagementApp.BLRepo;
using EventManagementApp.DALRepo;
using EventManagementApp.Models;
using Moq;
using Xunit;

namespace EventManagementApp.Tests.BLTests
{
    public class EventBLTests
    {
        private readonly Mock<IEventDAL> _eventDALMock;
        private readonly EventBL _eventBL;

        public EventBLTests()
        {
            _eventDALMock = new Mock<IEventDAL>();
            _eventBL = new EventBL(_eventDALMock.Object);
        }

        [Fact]
        public async Task GetEventsByCity_ShouldReturnEvents()
        {
            // Arrange

            string city = "Mysore";

            var expectedData =
                new List<EventByCityResponse>
                {
                    new EventByCityResponse
                    {
                        EventType = "Conference",
                        OrganizerName = "Organizer1",
                        EventLocation = "Radison Blu Hotel",
                        StartDate = DateTime.Parse("2022-05-29").ToString(),
                        EndDate = DateTime.Parse("2022-05-31").ToString()
                    }
                };

            _eventDALMock
                .Setup(x => x.GetEventsByCityAsync(city))
                .ReturnsAsync(expectedData);

            // Act

            var result = await _eventBL.GetEventsByCityAsync(city);

            // Assert

            Assert.NotNull(result);

            Assert.Single(result);

            Assert.Equal
            (
                "Conference",
                result[0].EventType
            );
        }

        [Fact]
        public async Task GetEventsByCity_ShouldThrowException_WhenNoDataFound()
        {
            // Arrange

            string city = "Vadodara";

            _eventDALMock
                .Setup(x => x.GetEventsByCityAsync(city))
                .ReturnsAsync(new List<EventByCityResponse>());

            // Act & Assert

            var exception =
                await Assert.ThrowsAsync<Exception>
                (
                    () => _eventBL.GetEventsByCityAsync(city)
                );

            Assert.Equal
            (
                "No events found in Vadodara",
                exception.Message
            );
        }

        [Fact]
        public async Task GetEventsByCity_ShouldThrowException_WhenCityEmpty()
        {
            var exception =
                await Assert.ThrowsAsync<Exception>
                (
                    () => _eventBL.GetEventsByCityAsync("")
                );

            Assert.Equal
            (
                "City is required",
                exception.Message
            );
        }
    }
}