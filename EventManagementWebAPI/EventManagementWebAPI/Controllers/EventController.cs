using EventManagementApp.BLRepo;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventBL _eventBL;

        public EventController(IEventBL eventBL)
        {
            _eventBL = eventBL;
        }

        [HttpGet("city/{city}")]
        public async Task<IActionResult> GetEventsByCity(string city)
        {
            try
            {
                var result = await _eventBL.GetEventsByCityAsync(city);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new
                {
                    Message = ex.Message
                });
            }
        }
    }
}