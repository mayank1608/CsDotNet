using GoodReadsWebApp.BLRepo;
using GoodReadsWebApp.DALRepo;
using GoodReadsWebApp.Models;

namespace GoodReadsWebApp.BLRepo
{
    public class BookBL : IBookBL
    {
        private readonly IBookDAL _bookDAL;

        public BookBL(IBookDAL bookDAL)
        {
            _bookDAL = bookDAL;
        }

        public async Task<ApiResponse<List<BookReviewsResponse>>> GetReviewsByBookTitleAsync(string title)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(title))
                {
                    return ApiResponse<List<BookReviewsResponse>>
                        .BadRequest("Book title is required.");
                }

                var books = await _bookDAL.GetReviewsByBookTitleAsync(title.Trim());

                if (books == null || books.Count == 0)
                {
                    return ApiResponse<List<BookReviewsResponse>>
                        .NotFound("No book found matching the given title.");
                }

                return ApiResponse<List<BookReviewsResponse>>
                    .Ok(books, "Book reviews fetched successfully.");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponse<List<AuthorBookReviewResponse>>> GetHighRatedReviewsByAuthorNameAsync(string authorName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(authorName))
                {
                    return ApiResponse<List<AuthorBookReviewResponse>>
                        .BadRequest("Author name is required.");
                }

                var result = await _bookDAL
                    .GetHighRatedReviewsByAuthorNameAsync(authorName.Trim());

                if (result == null || result.Count == 0)
                {
                    return ApiResponse<List<AuthorBookReviewResponse>>
                        .NotFound("No records found with rating greater than or equal to 4 for the given author.");
                }

                return ApiResponse<List<AuthorBookReviewResponse>>
                    .Ok(result, "High rated reviews fetched successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }

        }

        public async Task<ApiResponse<ReviewRatingResponse>> AddReviewAsync(AddReviewRequest request)
        {
            try
            {
                if (request == null)
                {
                    return ApiResponse<ReviewRatingResponse>
                        .BadRequest("Request body cannot be null.");
                }

                if (request.Book_isbn <= 0)
                {
                    return ApiResponse<ReviewRatingResponse>
                        .BadRequest("Valid Book ISBN is required.");
                }

                if (string.IsNullOrWhiteSpace(request.ReviewText))
                {
                    return ApiResponse<ReviewRatingResponse>
                        .BadRequest("Review is required.");
                }

                if (request.ReviewText.Length > 200)
                {
                    return ApiResponse<ReviewRatingResponse>
                        .BadRequest("Review cannot be more than 200 characters.");
                }

                if (request.Rating < 1 || request.Rating > 5)
                {
                    return ApiResponse<ReviewRatingResponse>
                        .BadRequest("Rating must be between 1 and 5.");
                }

                var bookExists = await _bookDAL.BookExistsAsync(request.Book_isbn);

                if (!bookExists)
                {
                    return ApiResponse<ReviewRatingResponse>
                        .BadRequest("Book ISBN does not exist.");
                }

                var review = new Review
                {
                    Book_isbn = request.Book_isbn,
                    ReviewText = request.ReviewText.Trim(),
                    Rating = request.Rating
                };

                var createdReview = await _bookDAL.AddReviewAsync(review);

                var response = new ReviewRatingResponse
                {
                    ReviewId = createdReview.Review_id,
                    Rating = createdReview.Rating,
                    Review = createdReview.ReviewText
                };

                return ApiResponse<ReviewRatingResponse>
                    .Created(response, "Review added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }

        }

        public async Task<ApiResponse<ReviewRatingResponse>> UpdateReviewAsync(int reviewId, UpdateReviewRequest request)
        {
            try
            {
                if (reviewId <= 0)
                {
                    return ApiResponse<ReviewRatingResponse>
                        .BadRequest("Valid Review ID is required.");
                }

                if (request == null)
                {
                    return ApiResponse<ReviewRatingResponse>
                        .BadRequest("Request body cannot be null.");
                }

                if (string.IsNullOrWhiteSpace(request.Review))
                {
                    return ApiResponse<ReviewRatingResponse>
                        .BadRequest("Review is required.");
                }

                if (request.Review.Length > 200)
                {
                    return ApiResponse<ReviewRatingResponse>
                        .BadRequest("Review cannot be more than 200 characters.");
                }

                if (request.Rating < 1 || request.Rating > 5)
                {
                    return ApiResponse<ReviewRatingResponse>
                        .BadRequest("Rating must be between 1 and 5.");
                }

                var existingReview = await _bookDAL.GetReviewByIdAsync(reviewId);

                if (existingReview == null)
                {
                    return ApiResponse<ReviewRatingResponse>
                        .NotFound("Review ID does not exist.");
                }

                existingReview.Rating = request.Rating;
                existingReview.ReviewText = request.Review.Trim();

                var updatedReview = await _bookDAL.UpdateReviewAsync(existingReview);

                var response = new ReviewRatingResponse
                {
                    ReviewId = updatedReview.Review_id,
                    Rating = updatedReview.Rating,
                    Review = updatedReview.ReviewText
                };

                return ApiResponse<ReviewRatingResponse>
                    .Ok(response, "Review updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponse<ReviewRatingResponse>> GetReviewByIdAsync(int reviewId)
        {
            try
            {
                if (reviewId <= 0)
                {
                    return ApiResponse<ReviewRatingResponse>
                        .BadRequest("Valid Review ID is required.");
                }

                var review = await _bookDAL.GetReviewByIdAsync(reviewId);

                if (review == null)
                {
                    return ApiResponse<ReviewRatingResponse>
                        .NotFound("Review ID does not exist.");
                }

                var response = new ReviewRatingResponse
                {
                    ReviewId = review.Review_id,
                    Rating = review.Rating,
                    Review = review.ReviewText
                };

                return ApiResponse<ReviewRatingResponse>
                    .Ok(response, "Review fetched successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}