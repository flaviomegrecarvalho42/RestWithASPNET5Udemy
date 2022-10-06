using System.Collections.Generic;
using RestWithAspNet5Udemy.Models;
using System.Linq;
using RestWithAspNet5Udemy.Data.DTO;
using RestWithAspNet5Udemy.Data.Mapper.Interfaces;

namespace RestWithAspNet5Udemy.Data.Mapper
{
    public class BookMapper : IParser<BookDto, Book>, IParser<Book, BookDto>
    {
        public Book Parse(BookDto bookDto)
        {
            if (bookDto == null)
                return new Book();

            return new Book
            {
                Id = bookDto.Id,
                Title = bookDto.Title,
                Author = bookDto.Author,
                LaunchDate = bookDto.LaunchDate,
                Price = bookDto.Price
            };
        }

        public BookDto Parse(Book book)
        {
            if (book == null)
                return new BookDto();

            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                LaunchDate = book.LaunchDate,
                Price = book.Price
            };
        }

        public List<Book> ParseList(List<BookDto> booksDto)
        {
            if (booksDto == null)
                return new List<Book>();

            return booksDto.Select(item => Parse(item)).ToList();
        }

        public List<BookDto> ParseList(List<Book> books)
        {
            if (books == null)
                return new List<BookDto>();

            return books.Select(item => Parse(item)).ToList();
        }
    }
}
