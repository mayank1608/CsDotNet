using GoodReadsWebApp.BLRepo;
using GoodReadsWebApp.Controllers;
using GoodReadsWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace GoodReadsWebApp.ControllerTests
{
    public class BookControllerTests
    {
        private readonly Mock<IBookBL> _mockBookBL;
        private readonly BookController _controller;

        public BookControllerTests()
        {
            _mockBookBL = new Mock<IBookBL>();
            _controller = new BookController(_mockBookBL.Object);
        }

        [Fact]
        public async Task GetReviewsByBookTitle_ShouldReturnOk_WhenBooksFound()
        {
            // Arrange
            var title = "clean";

            var responseData = new List<BookReviewsResponse>
            {
                new BookReviewsResponse
                {
                    BookIsbn = 1001,
                    Title = "Clean Code",
                    BookEdition = 1,
                    AuthorName = "Robert Martin",
                    Reviews = new List<ReviewRatingResponse>
                    {
                        new ReviewRatingResponse
                        {
                            ReviewId = 1,
                            Rating = 5,
                            Review = "Excellent book"
                        }
                    }
                }
            };

            var apiResponse = ApiResponse<List<BookReviewsResponse>>
                .Ok(responseData, "Book reviews fetched successfully.");

            _mockBookBL
                .Setup(x => x.GetReviewsByBookTitleAsync(title))
                .ReturnsAsync(apiResponse);

            // Act
            var result = await _controller.GetReviewsByBookTitle(title);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualResponse = Assert.IsType<ApiResponse<List<BookReviewsResponse>>>(okResult.Value);

            Assert.True(actualResponse.Success);
            Assert.Equal(200, actualResponse.StatusCode);
            Assert.Single(actualResponse.Data!);
            Assert.Equal("Clean Code", actualResponse.Data![0].Title);

            _mockBookBL.Verify(
                x => x.GetReviewsByBookTitleAsync(title),
                Times.Once);
        }

        [Fact]
        public async Task GetReviewsByBookTitle_ShouldReturnNotFound_WhenNoBookFound()
        {
            // Arrange
            var title = "unknown";

            var apiResponse = ApiResponse<List<BookReviewsResponse>>
                .NotFound("No book found matching the given title.");

            _mockBookBL
                .Setup(x => x.GetReviewsByBookTitleAsync(title))
                .ReturnsAsync(apiResponse);

            // Act
            var result = await _controller.GetReviewsByBookTitle(title);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var actualResponse = Assert.IsType<ApiResponse<List<BookReviewsResponse>>>(notFoundResult.Value);

            Assert.False(actualResponse.Success);
            Assert.Equal(404, actualResponse.StatusCode);
            Assert.Equal("No book found matching the given title.", actualResponse.Message);
        }

        [Fact]
        public async Task GetHighRatedReviewsByAuthorName_ShouldReturnOk_WhenRecordsFound()
        {
            // Arrange
            var authorName = "Robert";

            var responseData = new List<AuthorBookReviewResponse>
            {
                new AuthorBookReviewResponse
                {
                    BookTitle = "Clean Code",
                    BookEdition = 1,
                    AuthorName = "Robert Martin",
                    Rating = 5,
                    Review = "Excellent book"
                },
                new AuthorBookReviewResponse
                {
                    BookTitle = "Clean Architecture",
                    BookEdition = 1,
                    AuthorName = "Robert Martin",
                    Rating = 4,
                    Review = "Very useful"
                }
            };

            var apiResponse = ApiResponse<List<AuthorBookReviewResponse>>
                .Ok(responseData, "High rated reviews fetched successfully.");

            _mockBookBL
                .Setup(x => x.GetHighRatedReviewsByAuthorNameAsync(authorName))
                .ReturnsAsync(apiResponse);

            // Act
            var result = await _controller.GetHighRatedReviewsByAuthorName(authorName);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualResponse = Assert.IsType<ApiResponse<List<AuthorBookReviewResponse>>>(okResult.Value);

            Assert.True(actualResponse.Success);
            Assert.Equal(200, actualResponse.StatusCode);
            Assert.Equal(2, actualResponse.Data!.Count);
            Assert.All(actualResponse.Data, item => Assert.True(item.Rating >= 4));
        }

        [Fact]
        public async Task GetHighRatedReviewsByAuthorName_ShouldReturnNotFound_WhenNoRecordsFound()
        {
            // Arrange
            var authorName = "Unknown";

            var apiResponse = ApiResponse<List<AuthorBookReviewResponse>>
                .NotFound("No records found with rating greater than or equal to 4 for the given author.");

            _mockBookBL
                .Setup(x => x.GetHighRatedReviewsByAuthorNameAsync(authorName))
                .ReturnsAsync(apiResponse);

            // Act
            var result = await _controller.GetHighRatedReviewsByAuthorName(authorName);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var actualResponse = Assert.IsType<ApiResponse<List<AuthorBookReviewResponse>>>(notFoundResult.Value);

            Assert.False(actualResponse.Success);
            Assert.Equal(404, actualResponse.StatusCode);
        }

        [Fact]
        public async Task AddReview_ShouldReturnCreatedAtAction_WhenReviewCreated()
        {
            // Arrange
            var request = new AddReviewRequest
            {
                Book_isbn = 1001,
                ReviewText = "Very helpful book",
                Rating = 5
            };

            var responseData = new ReviewRatingResponse
            {
                ReviewId = 10,
                Rating = 5,
                Review = "Very helpful book"
            };

            var apiResponse = ApiResponse<ReviewRatingResponse>
                .Created(responseData, "Review added successfully.");

            _mockBookBL
                .Setup(x => x.AddReviewAsync(request))
                .ReturnsAsync(apiResponse);

            // Act
            var result = await _controller.AddReview(request);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var actualResponse = Assert.IsType<ApiResponse<ReviewRatingResponse>>(createdResult.Value);

            Assert.Equal(nameof(BookController.GetReviewById), createdResult.ActionName);
            Assert.True(actualResponse.Success);
            Assert.Equal(201, actualResponse.StatusCode);
            Assert.Equal(10, actualResponse.Data!.ReviewId);
        }

        [Fact]
        public async Task AddReview_ShouldReturnBadRequest_WhenValidationFails()
        {
            // Arrange
            var request = new AddReviewRequest
            {
                Book_isbn = 1001,
                ReviewText = "",
                Rating = 5
            };

            var apiResponse = ApiResponse<ReviewRatingResponse>
                .BadRequest("Review is required.");

            _mockBookBL
                .Setup(x => x.AddReviewAsync(request))
                .ReturnsAsync(apiResponse);

            // Act
            var result = await _controller.AddReview(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var actualResponse = Assert.IsType<ApiResponse<ReviewRatingResponse>>(badRequestResult.Value);

            Assert.False(actualResponse.Success);
            Assert.Equal(400, actualResponse.StatusCode);
            Assert.Equal("Review is required.", actualResponse.Message);
        }

        [Fact]
        public async Task UpdateReview_ShouldReturnOk_WhenReviewUpdated()
        {
            // Arrange
            var reviewId = 1;

            var request = new UpdateReviewRequest
            {
                Review = "Updated review text",
                Rating = 4
            };

            var responseData = new ReviewRatingResponse
            {
                ReviewId = reviewId,
                Rating = 4,
                Review = "Updated review text"
            };

            var apiResponse = ApiResponse<ReviewRatingResponse>
                .Ok(responseData, "Review updated successfully.");

            _mockBookBL
                .Setup(x => x.UpdateReviewAsync(reviewId, request))
                .ReturnsAsync(apiResponse);

            // Act
            var result = await _controller.UpdateReview(reviewId, request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualResponse = Assert.IsType<ApiResponse<ReviewRatingResponse>>(okResult.Value);

            Assert.True(actualResponse.Success);
            Assert.Equal(200, actualResponse.StatusCode);
            Assert.Equal(4, actualResponse.Data!.Rating);
            Assert.Equal("Updated review text", actualResponse.Data.Review);
        }

        [Fact]
        public async Task UpdateReview_ShouldReturnNotFound_WhenReviewIdDoesNotExist()
        {
            // Arrange
            var reviewId = 99;

            var request = new UpdateReviewRequest
            {
                Review = "Updated review text",
                Rating = 4
            };

            var apiResponse = ApiResponse<ReviewRatingResponse>
                .NotFound("Review ID does not exist.");

            _mockBookBL
                .Setup(x => x.UpdateReviewAsync(reviewId, request))
                .ReturnsAsync(apiResponse);

            // Act
            var result = await _controller.UpdateReview(reviewId, request);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var actualResponse = Assert.IsType<ApiResponse<ReviewRatingResponse>>(notFoundResult.Value);

            Assert.False(actualResponse.Success);
            Assert.Equal(404, actualResponse.StatusCode);
            Assert.Equal("Review ID does not exist.", actualResponse.Message);
        }

        [Fact]
        public async Task PatchReview_ShouldReturnOk_WhenReviewUpdated()
        {
            // Arrange
            var reviewId = 1;

            var request = new UpdateReviewRequest
            {
                Review = "Patched review text",
                Rating = 5
            };

            var responseData = new ReviewRatingResponse
            {
                ReviewId = reviewId,
                Rating = 5,
                Review = "Patched review text"
            };

            var apiResponse = ApiResponse<ReviewRatingResponse>
                .Ok(responseData, "Review updated successfully.");

            _mockBookBL
                .Setup(x => x.UpdateReviewAsync(reviewId, request))
                .ReturnsAsync(apiResponse);

            // Act
            var result = await _controller.PatchReview(reviewId, request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualResponse = Assert.IsType<ApiResponse<ReviewRatingResponse>>(okResult.Value);

            Assert.True(actualResponse.Success);
            Assert.Equal(200, actualResponse.StatusCode);
            Assert.Equal("Patched review text", actualResponse.Data!.Review);
        }
    }
}