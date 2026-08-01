using BookManagementDAL;
using BookManagementDTO.Models;

namespace BookManagementBL
{
    public class BooksBL
    {
        private readonly BookDAL objBooksDAL;
        
        public BooksBL()
        {
            objBooksDAL = new BookDAL();
        }

        public async Task<bool> AddBookDetails(List<Books> books)
        {
            try
            {
                return await objBooksDAL.AddBookDetails(books);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return false;
            }
             
        }
    }
}
