using Microsoft.EntityFrameworkCore;
using GoodReadsWebApp.Models;

namespace GoodReadsWebApp.DALRepo
{
    public class BookDAL : IBookDAL
    {
        private readonly AppDbContext _context;

        public BookDAL(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<BookReviewsResponse>> GetReviewsByBookTitleAsync(string title)
        {
            try
            {
                return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Reviews)
                .Where(b => b.Title != null &&
                            EF.Functions.Like(b.Title, $"%{title}%"))
                .Select(b => new BookReviewsResponse
                {
                    BookIsbn = b.Book_isbn,
                    Title = b.Title,
                    BookEdition = b.Book_edition,
                    AuthorName = b.Author.Author_name,
                    Reviews = b.Reviews.Select(r => new ReviewRatingResponse
                    {
                        ReviewId = r.Review_id,
                        Rating = r.Rating,
                        Review = r.ReviewText
                    }).ToList()
                })
                .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<AuthorBookReviewResponse>> GetHighRatedReviewsByAuthorNameAsync(string authorName)
        {
            try
            {
                return await _context.Reviews
                    .Include(r => r.Book)
                    .ThenInclude(b => b.Author)
                    .Where(r => EF.Functions.Like(r.Book.Author.Author_name, $"%{authorName}%")
                                && r.Rating >= 4)
                    .OrderByDescending(r => r.Rating)
                    .Select(r => new AuthorBookReviewResponse
                    {
                        BookTitle = r.Book.Title,
                        BookEdition = r.Book.Book_edition,
                        AuthorName = r.Book.Author.Author_name,
                        Rating = r.Rating,
                        Review = r.ReviewText
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> BookExistsAsync(int bookIsbn)
        {
            try
            {
                return await _context.Books
                    .AnyAsync(b => b.Book_isbn == bookIsbn);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<Review> AddReviewAsync(Review review)
        {
            try
            {
                await _context.Reviews.AddAsync(review);
                await _context.SaveChangesAsync();

                return review;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<Review?> GetReviewByIdAsync(int reviewId)
        {
            try
            {
                return await _context.Reviews
                .Include(r => r.Book)
                .FirstOrDefaultAsync(r => r.Review_id == reviewId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<Review> UpdateReviewAsync(Review review)
        {
            try
            {
                _context.Reviews.Update(review);
            await _context.SaveChangesAsync();

            return review;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }            
        }
    }
}