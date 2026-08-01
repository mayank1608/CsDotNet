namespace GoodReadsWebApp.Models
{
    public class ReviewRatingResponse
    {
        public int ReviewId { get; set; }
        public int Rating { get; set; }
        public string Review { get; set; } = string.Empty;
    }
}