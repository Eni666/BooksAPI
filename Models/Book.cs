using BooksAPI.Helpers;
using System;
using System.Text.Json.Serialization;

namespace BooksAPI.Models
{
    [JsonConverter(typeof(BooksJsonConverter))]
    public class Book
    {
        public string Id { get; set; }

        public string Author { get; set; }

        public string Title { get; set; }

        public string Genre { get; set; }

        public double Price { get; set; }

        public DateTime PublishDate { get; set; }

        public string Description { get; set; }
    }
}
