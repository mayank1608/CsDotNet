using BookManagementDTO.Models;

namespace BookManagementDAL
{
    public class BookDAL
    {
        private readonly BookMngmtDBContext _dbContext;

        public BookDAL()
        {
            _dbContext = new BookMngmtDBContext();
        }

        public async Task<bool> AddBookDetails(List<Books> books)
        {
            try
            {
                await _dbContext.Books.AddRangeAsync(books);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

    }
}
