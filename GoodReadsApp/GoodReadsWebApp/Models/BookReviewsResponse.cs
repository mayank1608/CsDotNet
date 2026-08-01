namespace GoodReadsWebApp.Models
{
    public class BookReviewsResponse
    {
        public int BookIsbn { get; set; }
        public string? Title { get; set; }
        public int BookEdition { get; set; }
        public string AuthorName { get; set; } = string.Empty;

        public List<ReviewRatingResponse> Reviews { get; set; } = new List<ReviewRatingResponse>();
    }
}