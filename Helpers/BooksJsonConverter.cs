using BooksAPI.Models;
using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BooksAPI.Helpers
{
    /// <summary>
    /// Custom Json Converter for Book model. 
    /// </summary>
    public class BooksJsonConverter : JsonConverter<Book>
    {
        public override Book Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var book = new Book();

            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("JSON invalid");
            }

            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    string propertyName = reader.GetString();
                    reader.Read(); // Jump to value of property

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        continue; // No point in doing anything if null
                    }

                    switch (propertyName)
                    {
                        case "id":
                            if (reader.TokenType == JsonTokenType.String)
                                book.Id = reader.GetString();
                            else
                                throw new JsonException($"'{propertyName}' must be of type string.");
                            break;

                        case "author":
                            if (reader.TokenType == JsonTokenType.String)
                                book.Author = reader.GetString();
                            else
                                throw new JsonException($"'{propertyName}' must be of type string.");
                            break;

                        case "title":
                            if (reader.TokenType == JsonTokenType.String)
                                book.Title = reader.GetString();
                            else
                                throw new JsonException($"'{propertyName}' must be of type string.");
                            break;

                        case "genre":
                            if (reader.TokenType == JsonTokenType.String)
                                book.Genre = reader.GetString();
                            else
                                throw new JsonException($"'{propertyName}' must be of type string.");
                            break;

                        case "description":
                            if (reader.TokenType == JsonTokenType.String)
                                book.Description = reader.GetString();
                            else
                                throw new JsonException($"'{propertyName}' must be of type string.");
                            break;

                        case "price":
                            if (reader.TokenType == JsonTokenType.String)
                            {
                                if (double.TryParse(reader.GetString(), NumberStyles.Any, CultureInfo.InvariantCulture, out double price))
                                    book.Price = price;
                                else
                                    throw new JsonException($"'{propertyName}' can't be parsed as double.");
                            }
                            else
                                throw new JsonException($"'{propertyName}' must be of type string.");
                            break;

                        case "publish_date":
                            if (reader.TokenType == JsonTokenType.String)
                            {
                                if (DateTime.TryParse(reader.GetString(), out DateTime publishDate))
                                    book.PublishDate = publishDate;
                                else
                                    throw new JsonException($"'{propertyName}' can't be parsed as DateTime.");
                            }
                            else
                                throw new JsonException($"'{propertyName}' must be of type string.");
                            break;

                        default: throw new JsonException($"'{propertyName}' syntax error in Book Json");
                    }
                }
            }

            return book;
        }

        public override void Write(Utf8JsonWriter writer, Book value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("id", value.Id);
            writer.WriteString("author", value.Author);
            writer.WriteString("title", value.Title);
            writer.WriteString("genre", value.Genre);
            writer.WriteString("price", value.Price.ToString(CultureInfo.InvariantCulture));
            writer.WriteString("publish_date", value.PublishDate.ToString(CultureInfo.InvariantCulture));
            writer.WriteString("description", value.Description);
            writer.WriteEndObject();
        }
    }
}
