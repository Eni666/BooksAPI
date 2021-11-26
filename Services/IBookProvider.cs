using BooksAPI.Models;
using System.Collections.Generic;

namespace BooksAPI.Services
{
    public interface IBookProvider
    {
        IEnumerable<Book> GetAllBooks();

        IEnumerable<Book> OrderBooksByField(string searchField);

        IEnumerable<Book> FindBooksByField(string searchField, string searchValue);

        IEnumerable<Book> OrderBooksByPublished(string date);
    }
}
