using GoodReadsWebApp.Models;

namespace GoodReadsWebApp.DALRepo
{
    public interface IBookDAL
    {
        Task<List<BookReviewsResponse>> GetReviewsByBookTitleAsync(string title);

        Task<List<AuthorBookReviewResponse>> GetHighRatedReviewsByAuthorNameAsync(string authorName);

        Task<bool> BookExistsAsync(int bookIsbn);

        Task<Review> AddReviewAsync(Review review);

        Task<Review?> GetReviewByIdAsync(int reviewId);

        Task<Review> UpdateReviewAsync(Review review);
    }
}