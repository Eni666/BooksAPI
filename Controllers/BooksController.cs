using BooksAPI.Models;
using BooksAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BooksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        private readonly IBookProvider _bookProvider;

        public BooksController(IBookProvider bookProvider)
        {
            _bookProvider = bookProvider;
        }

        public IEnumerable<Book> Index()
        {
            return _bookProvider.GetAllBooks();
        }

        [HttpGet("{field}/{value}")]
        public IEnumerable<Book> FindBooksByField(string field, string value)
        {
            return _bookProvider.FindBooksByField(field, value);
        }

        [HttpGet("{field}")]
        public IEnumerable<Book> OrderBooksByField(string field)
        {
            return _bookProvider.OrderBooksByField(field);
        }

        [HttpGet("published/{year}")]
        public IEnumerable<Book> OrderBooksByPublished(string year)
        {
            return _bookProvider.OrderBooksByPublished(year + "-01-01");
        }

        [HttpGet("published/{year}/{month}")]
        public IEnumerable<Book> OrderBooksByPublished(string year, string month)
        {
            return _bookProvider.OrderBooksByPublished(year + "-" + month + "-01");
        }

        [HttpGet("published/{year}/{month}/{day}")]
        public IEnumerable<Book> OrderBooksByPublished(string year, string month, string day)
        {
            return _bookProvider.OrderBooksByPublished(year + "-" + month + "-" + day);
        }
    }
}
