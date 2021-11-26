using BooksAPI.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Globalization;
using System;

namespace BooksAPI.Services
{
    public class BookProvider : IBookProvider
    {
        private readonly string _jsonData;

        public BookProvider()
        {
            _jsonData = GetJsonDataFromEmbededResourceFile();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchField"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public IEnumerable<Book> FindBooksByField(string searchField, string searchValue)
        {
            // Deserialize Json data with case insensitive option
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var books = JsonSerializer.Deserialize<IEnumerable<Book>>(_jsonData, options);

            IEnumerable<Book> orderedBooks = new List<Book>();

            switch (searchField.ToUpper())
            {
                case BookConstants.Id:
                    orderedBooks = books.Where(b => b.Id.ToUpper().Contains(searchValue.ToUpper()));
                    orderedBooks = orderedBooks.OrderBy(b => b.Id);
                    break;
                case BookConstants.Author:
                    orderedBooks = books.Where(b => b.Author.ToUpper().Contains(searchValue.ToUpper()));
                    orderedBooks = orderedBooks.OrderBy(b => b.Author);
                    break;
                case BookConstants.Title:
                    orderedBooks = books.Where(b => b.Title.ToUpper().Contains(searchValue.ToUpper()));
                    orderedBooks = orderedBooks.OrderBy(b => b.Title);
                    break;
                case BookConstants.Genre:
                    orderedBooks = books.Where(b => b.Genre.ToUpper().Contains(searchValue.ToUpper()));
                    orderedBooks = orderedBooks.OrderBy(b => b.Genre);
                    break;
                case BookConstants.Price:
                    // Maybe the code below is nicer to have in an own method. 
                    string[] prices = searchValue.Split('&');
                    if (prices.Length > 1)
                    {
                        if ((double.TryParse(prices[0], NumberStyles.Any, CultureInfo.InvariantCulture, out double fromPrice)) &&
                            (double.TryParse(prices[1], NumberStyles.Any, CultureInfo.InvariantCulture, out double tomPrice)))
                        {
                            orderedBooks = books.Where(b => b.Price >= fromPrice && b.Price <= tomPrice);
                            orderedBooks = orderedBooks.OrderBy(b => b.Price);
                        }
                    }
                    else
                    {
                        if (double.TryParse(searchValue, NumberStyles.Any, CultureInfo.InvariantCulture, out double price))
                        {
                            orderedBooks = books.Where(b => b.Price == price);
                            orderedBooks = orderedBooks.OrderBy(b => b.Price);
                        }
                    }
                    break;
                case BookConstants.Description:
                    orderedBooks = books.Where(b => b.Description.ToUpper().Contains(searchValue.ToUpper()));
                    orderedBooks = orderedBooks.OrderBy(b => b.Description);
                    break;
                default:
                    break;
            }

            return orderedBooks;
        }

        /// <summary>
        /// Gets all unsorted books 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Book> GetAllBooks()
        {
            // Deserialize Json data with case insensitive option
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<IEnumerable<Book>>(_jsonData, options);
        }

        /// <summary>
        /// Sorts books by field
        /// </summary>
        /// <param name="searchField"></param>
        /// <returns></returns>
        public IEnumerable<Book> OrderBooksByField(string searchField)
        {
            // Deserialize Json data with case insensitive option
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var books = JsonSerializer.Deserialize<IEnumerable<Book>>(_jsonData, options);

            IEnumerable<Book> orderedBooks = new List<Book>();

            switch (searchField.ToUpper())
            {
                case BookConstants.Id:
                    orderedBooks = books.OrderBy(b => b.Id);
                    break;
                case BookConstants.Author:
                    orderedBooks = books.OrderBy(b => b.Author);
                    break;
                case BookConstants.Title:
                    orderedBooks = books.OrderBy(b => b.Title);
                    break;
                case BookConstants.Genre:
                    orderedBooks = books.OrderBy(b => b.Genre);
                    break;
                case BookConstants.Price:
                    orderedBooks = books.OrderBy(b => b.Price);
                    break;
                case BookConstants.Publish_Date:
                    orderedBooks = books.OrderBy(b => b.PublishDate);
                    break;
                case BookConstants.Description:
                    orderedBooks = books.OrderBy(b => b.Description);
                    break;
                default:
                    break;
            }
            return orderedBooks;
        }

        /// <summary>
        /// Gets books from given date and sorts by date.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public IEnumerable<Book> OrderBooksByPublished(string date)
        {
            IEnumerable<Book> orderedBooks = new List<Book>();

            // Ensure that we have correct date. 
            if (DateTime.TryParse(date, out DateTime fromDate))
            {
                // Deserialize Json data with case insensitive option
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var books = JsonSerializer.Deserialize<IEnumerable<Book>>(_jsonData, options);

                orderedBooks = books.Where(b => b.PublishDate >= fromDate);
                orderedBooks = orderedBooks.OrderBy(b => b.PublishDate);
            }

            return orderedBooks;      
        }

        #region Private methods
        /// <summary>
        /// Gets Json data from embeded resource file - books.json
        /// </summary>
        /// <returns></returns>
        private string GetJsonDataFromEmbededResourceFile()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "BooksAPI.Data.books.json";

            string jsonData = null;
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                jsonData = reader.ReadToEnd();
            }
     
            return jsonData;
        }
        #endregion
    }
}
