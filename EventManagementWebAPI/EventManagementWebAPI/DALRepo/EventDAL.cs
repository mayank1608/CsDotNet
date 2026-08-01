using EventManagementApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EventManagementApp.DALRepo
{
    public class EventDAL : IEventDAL
    {
        private readonly AppDbContext _context;

        public EventDAL(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<EventByCityResponse>> GetEventsByCityAsync(string city)
        {
            // var result = await
            //     (
            //         from oe in _context.OrganizeEvents
            //         join e in _context.Events
            //             on oe.EventID equals e.EventID

            //         join o in _context.Organizers
            //             on oe.OrganizerID equals o.OrganizerID

            //         where oe.City.ToLower() == city.ToLower()

            //         select new EventByCityResponse
            //         {
            //             EventType = e.EventType,
            //             OrganizerName = o.UserName,
            //             EventLocation = oe.EventLocation,
            //             StartDate = oe.StartDate.ToString("yyyy-MM-dd"),
            //             EndDate = oe.EndDate.ToString("yyyy-MM-dd")
            //         }

            //     ).ToListAsync();

            var result = await _context.OrganizeEvents
                            .Include(x => x.Event)
                            .Include(x => x.Organizer)
                            .Where(x => x.City == city)
                            .Select(x => new EventByCityResponse
                            {
                                EventType = x.Event!.EventType,
                                OrganizerName = x.Organizer!.UserName,
                                EventLocation = x.EventLocation,
                                StartDate = x.StartDate.ToString("yyyy-MM-dd"),
                                EndDate = x.EndDate.ToString("yyyy-MM-dd"),
                            })
                            .ToListAsync();

            return result;
        }
    }
}