using GoodReadsWebApp.Models;

namespace GoodReadsWebApp.BLRepo
{
    public interface IBookBL
    {
        Task<ApiResponse<List<BookReviewsResponse>>> GetReviewsByBookTitleAsync(string title);

        Task<ApiResponse<List<AuthorBookReviewResponse>>> GetHighRatedReviewsByAuthorNameAsync(string authorName);

        Task<ApiResponse<ReviewRatingResponse>> AddReviewAsync(AddReviewRequest request);

        Task<ApiResponse<ReviewRatingResponse>> UpdateReviewAsync(int reviewId, UpdateReviewRequest request);

        Task<ApiResponse<ReviewRatingResponse>> GetReviewByIdAsync(int reviewId);
    }
}