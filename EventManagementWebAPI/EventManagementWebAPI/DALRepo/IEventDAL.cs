using EventManagementApp.Models;

namespace EventManagementApp.DALRepo
{
    public interface IEventDAL
    {
        Task<List<EventByCityResponse>> GetEventsByCityAsync(string city);
    }
}