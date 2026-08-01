namespace EventManagementApp.Models
{
    public class EventByCityResponse
    {
        public string EventType { get; set; } = string.Empty;

        public string OrganizerName { get; set; } = string.Empty;

        public string EventLocation { get; set; } = string.Empty;

        public string StartDate { get; set; } = string.Empty;

        public string EndDate { get; set; } = string.Empty;
    }
}