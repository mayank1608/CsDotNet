namespace GoodReadsWebApp.Models
{
    public class AuthorBookReviewResponse
    {
        public string? BookTitle { get; set; }
        public int BookEdition { get; set; }
        public string AuthorName { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string Review { get; set; } = string.Empty;
    }
}