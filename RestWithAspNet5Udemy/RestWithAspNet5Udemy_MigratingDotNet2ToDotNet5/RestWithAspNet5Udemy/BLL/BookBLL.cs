using System.Collections.Generic;
using RestWithAspNet5Udemy.Models;
using RestWithAspNet5Udemy.BLL.Interfaces;
using RestWithAspNet5Udemy.Repositories.Interfaces;
using RestWithAspNet5Udemy.Data.Mapper;
using RestWithAspNet5Udemy.Data.DTO;

namespace RestWithAspNet5Udemy.BLL
{
    public class BookBLL : IBookBLL
    {
        private IBaseRepository<Book> _repository;
        private readonly BookMapper _mapper;

        public BookBLL(IBaseRepository<Book> repository)
        {
            _repository = repository;
            _mapper = new BookMapper();
        }

        public BookDto Create(BookDto book)
        {
            var bookEntity = _mapper.Parse(book);
            bookEntity = _repository.Create(bookEntity);
            return _mapper.Parse(bookEntity);
        }

        public BookDto FindById(long id)
        {
            return _mapper.Parse(_repository.FindById(id));
        }

        public List<BookDto> FindAll()
        {
            return _mapper.ParseList(_repository.FindAll());
        }

        public BookDto Update(BookDto book)
        {
            var bookEntity = _mapper.Parse(book);
            bookEntity = _repository.Update(bookEntity);
            return _mapper.Parse(bookEntity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public bool Exists(long id)
        {
            return _repository.Exists(id);
        }
    }
}