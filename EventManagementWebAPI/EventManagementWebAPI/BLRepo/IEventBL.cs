using EventManagementApp.Models;

namespace EventManagementApp.BLRepo
{
    public interface IEventBL
    {
        Task<List<EventByCityResponse>> GetEventsByCityAsync(string city);
    }
}