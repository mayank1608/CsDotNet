using EventManagementApp.DALRepo;
using EventManagementApp.Models;

namespace EventManagementApp.BLRepo
{
    public class EventBL : IEventBL
    {
        private readonly IEventDAL _eventDAL;

        public EventBL(IEventDAL eventDAL)
        {
            _eventDAL = eventDAL;
        }

        public async Task<List<EventByCityResponse>> GetEventsByCityAsync(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                throw new Exception("City is required");
            }

            var events = await _eventDAL.GetEventsByCityAsync(city);

            if (!events.Any())
            {
                throw new Exception($"No events found in {city}");
            }

            return events;
        }
    }
}