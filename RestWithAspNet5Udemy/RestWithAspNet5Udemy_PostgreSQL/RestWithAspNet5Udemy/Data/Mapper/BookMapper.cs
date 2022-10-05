using RestWithAspNet5Udemy.Data.DTO;
using RestWithAspNet5Udemy.Data.Mapper.Interfaces;
using RestWithAspNet5Udemy.Models;
using System.Collections.Generic;
using System.Linq;

namespace RestWithAspNet5Udemy.Data.Mapper
{
    public class BookMapper : IParser<BookDto, Book>, IParser<Book, BookDto>
    {
        public Book Parse(BookDto originDto)
        {
            if (originDto == null)
                return null;

            return new Book
            {
                Author = originDto.Author,
                Id = originDto.Id,
                LaunchDate = originDto.LaunchDate,
                Price = originDto.Price,
                Title = originDto.Title
            };
        }

        public BookDto Parse(Book origin)
        {
            if (origin == null)
                return null;

            return new BookDto
            {
                Author = origin.Author,
                Id = origin.Id,
                LaunchDate = origin.LaunchDate,
                Price = origin.Price,
                Title = origin.Title
            };
        }

        public List<Book> Parse(List<BookDto> originsDto)
        {
            if (originsDto == null)
                return null;

            return originsDto.Select(item => Parse(item)).ToList();
        }

        public List<BookDto> Parse(List<Book> origins)
        {
            if (origins == null)
                return null;

            return origins.Select(item => Parse(item)).ToList();
        }
    }
}
