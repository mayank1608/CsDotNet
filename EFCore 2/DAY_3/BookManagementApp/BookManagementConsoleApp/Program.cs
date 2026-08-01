using BookManagementBL;
using BookManagementDTO.Models;
using static System.Reflection.Metadata.BlobBuilder;

Console.WriteLine("Hello, World!");
BooksBL objbooksBl = new BooksBL();
List<Books> books = new List<Books>();


Console.Write("How many books want to add? ");
int count = int.Parse(Console.ReadLine());

for (int i = 0; i < count; i++)
{
    Books book = new Books();

    Console.WriteLine($"\nEnter details for Book {i + 1}");

    Console.Write("Title: ");
    book.Title = Console.ReadLine();

    Console.Write("Author: ");
    book.AuthorID = Console.ReadLine();

    Console.Write("Edition: ");
    book.Book_Edition = Int32.Parse(Console.ReadLine());

    books.Add(book);
}


await objbooksBl.AddBookDetails(books);