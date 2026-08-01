using GoodReadsWebApp.BLRepo;
using GoodReadsWebApp.DALRepo;
using GoodReadsWebApp.Models;
using Moq;
using Xunit;

namespace GoodReadsWebApp.BLTests
{
    public class BookBLTests
    {
        private readonly Mock<IBookDAL> _bookDALMock;
        private readonly BookBL _bookBL;

        public BookBLTests()
        {
            _bookDALMock = new Mock<IBookDAL>();
            _bookBL = new BookBL(_bookDALMock.Object);
        }

        [Fact]
        public async Task AddReview_Returns_BadRequest_When_Rating_Is_Less_Than_1()
        {
            var request = new AddReviewRequest
            {
                Book_isbn = 1001,
                ReviewText = "Good Book",
                Rating = 0
            };

            var result = await _bookBL.AddReviewAsync(request);

            Assert.False(result.Success);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Rating must be between 1 and 5.", result.Message);
        }

        [Fact]
        public async Task AddReview_Returns_BadRequest_When_Rating_Greater_Than_5()
        {
            var request = new AddReviewRequest
            {
                Book_isbn = 1001,
                ReviewText = "Good Book",
                Rating = 6
            };

            var result = await _bookBL.AddReviewAsync(request);

            Assert.False(result.Success);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task AddReview_Returns_BadRequest_When_Review_Is_Empty()
        {
            var request = new AddReviewRequest
            {
                Book_isbn = 1001,
                ReviewText = "",
                Rating = 5
            };

            var result = await _bookBL.AddReviewAsync(request);

            Assert.False(result.Success);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Review is required.", result.Message);
        }

        [Fact]
        public async Task AddReview_Returns_BadRequest_When_Book_Not_Exists()
        {
            var request = new AddReviewRequest
            {
                Book_isbn = 9999,
                ReviewText = "Nice",
                Rating = 4
            };

            _bookDALMock
                .Setup(x => x.BookExistsAsync(9999))
                .ReturnsAsync(false);

            var result = await _bookBL.AddReviewAsync(request);

            Assert.False(result.Success);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Book ISBN does not exist.", result.Message);

            _bookDALMock.Verify(
                x => x.BookExistsAsync(9999),
                Times.Once);
        }

        [Fact]
        public async Task AddReview_Returns_Created_When_Request_Is_Valid()
        {
            var request = new AddReviewRequest
            {
                Book_isbn = 1001,
                ReviewText = "Excellent Book",
                Rating = 5
            };

            _bookDALMock
                .Setup(x => x.BookExistsAsync(1001))
                .ReturnsAsync(true);

            _bookDALMock
                .Setup(x => x.AddReviewAsync(It.IsAny<Review>()))
                .ReturnsAsync(new Review
                {
                    Review_id = 1,
                    Book_isbn = 1001,
                    ReviewText = "Excellent Book",
                    Rating = 5
                });

            var result = await _bookBL.AddReviewAsync(request);

            Assert.True(result.Success);
            Assert.Equal(201, result.StatusCode);
            Assert.NotNull(result.Data);
            Assert.Equal(1, result.Data.ReviewId);

            _bookDALMock.Verify(
                x => x.AddReviewAsync(It.IsAny<Review>()),
                Times.Once);
        }

        [Fact]
        public async Task GetReviewsByBookTitle_Returns_NotFound_When_No_Data()
        {
            _bookDALMock
                .Setup(x => x.GetReviewsByBookTitleAsync("xyz"))
                .ReturnsAsync(new List<BookReviewsResponse>());

            var result = await _bookBL.GetReviewsByBookTitleAsync("xyz");

            Assert.False(result.Success);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task GetReviewsByBookTitle_Returns_Data_When_Found()
        {
            var books = new List<BookReviewsResponse>
            {
                new BookReviewsResponse
                {
                    BookIsbn = 1001,
                    Title = "Clean Code",
                    AuthorName = "Robert Martin"
                }
            };

            _bookDALMock
                .Setup(x => x.GetReviewsByBookTitleAsync("clean"))
                .ReturnsAsync(books);

            var result = await _bookBL.GetReviewsByBookTitleAsync("clean");

            Assert.True(result.Success);
            Assert.Equal(200, result.StatusCode);
            Assert.Single(result?.Data);
        }

        [Fact]
        public async Task GetHighRatedReviews_Returns_NotFound()
        {
            _bookDALMock
                .Setup(x =>
                    x.GetHighRatedReviewsByAuthorNameAsync("Robert"))
                .ReturnsAsync(
                    new List<AuthorBookReviewResponse>());

            var result =
                await _bookBL.GetHighRatedReviewsByAuthorNameAsync("Robert");

            Assert.False(result.Success);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task GetHighRatedReviews_Returns_Data()
        {
            var data = new List<AuthorBookReviewResponse>
            {
                new AuthorBookReviewResponse
                {
                    AuthorName = "Robert Martin",
                    BookTitle = "Clean Code",
                    Rating = 5,
                    Review = "Excellent"
                }
            };

            _bookDALMock
                .Setup(x =>
                    x.GetHighRatedReviewsByAuthorNameAsync("Robert"))
                .ReturnsAsync(data);

            var result =
                await _bookBL.GetHighRatedReviewsByAuthorNameAsync("Robert");

            Assert.True(result.Success);
            Assert.Equal(200, result.StatusCode);
            Assert.Single(result?.Data);
        }

        [Fact]
        public async Task UpdateReview_Returns_NotFound_When_Id_Not_Exists()
        {
            var request = new UpdateReviewRequest
            {
                Rating = 5,
                Review = "Updated Review"
            };

            _bookDALMock
                .Setup(x => x.GetReviewByIdAsync(100))
                .ReturnsAsync((Review?)null);

            var result =
                await _bookBL.UpdateReviewAsync(100, request);

            Assert.False(result.Success);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task UpdateReview_Returns_Ok_When_Review_Updated()
        {
            var existingReview = new Review
            {
                Review_id = 1,
                Book_isbn = 1001,
                ReviewText = "Old Review",
                Rating = 3
            };

            var request = new UpdateReviewRequest
            {
                Rating = 5,
                Review = "Updated Review"
            };

            _bookDALMock
                .Setup(x => x.GetReviewByIdAsync(1))
                .ReturnsAsync(existingReview);

            _bookDALMock
                .Setup(x => x.UpdateReviewAsync(It.IsAny<Review>()))
                .ReturnsAsync(existingReview);

            var result =
                await _bookBL.UpdateReviewAsync(1, request);

            Assert.True(result.Success);
            Assert.Equal(200, result.StatusCode);

            _bookDALMock.Verify(
                x => x.UpdateReviewAsync(It.IsAny<Review>()),
                Times.Once);
        }

        [Fact]
        public async Task GetReviewById_Returns_NotFound()
        {
            _bookDALMock
                .Setup(x => x.GetReviewByIdAsync(50))
                .ReturnsAsync((Review?)null);

            var result = await _bookBL.GetReviewByIdAsync(50);

            Assert.False(result.Success);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task GetReviewById_Returns_Data()
        {
            var review = new Review
            {
                Review_id = 1,
                Book_isbn = 1001,
                ReviewText = "Excellent",
                Rating = 5
            };

            _bookDALMock
                .Setup(x => x.GetReviewByIdAsync(1))
                .ReturnsAsync(review);

            var result = await _bookBL.GetReviewByIdAsync(1);

            Assert.True(result.Success);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(5, result?.Data?.Rating);
        }
    }
}