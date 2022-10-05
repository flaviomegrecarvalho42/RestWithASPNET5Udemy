using RestWithAspNet5Udemy.BLL.Interfaces;
using RestWithAspNet5Udemy.Data.DTO;
using RestWithAspNet5Udemy.Data.Mapper;
using RestWithAspNet5Udemy.Models;
using RestWithAspNet5Udemy.Repositories.Interfaces;
using System.Collections.Generic;

namespace RestWithAspNet5Udemy.BLL
{
    public class BookBLL : IBookBLL
    {
        private readonly IBaseRepository<Book> _repository;
        private readonly BookMapper _mapper;

        public BookBLL(IBaseRepository<Book> repository)
        {
            _repository = repository;
            _mapper = new BookMapper();
        }

        public List<BookDto> FindAll()
        {
            return _mapper.Parse(_repository.FindAll());
        }

        public BookDto FindById(long id)
        {
            return _mapper.Parse(_repository.FindById(id));
        }

        public BookDto Create(BookDto bookDto)
        {
            var bookEntity = _mapper.Parse(bookDto);
            bookEntity = _repository.Create(bookEntity);

            return _mapper.Parse(bookEntity);
        }

        public BookDto Update(BookDto bookDto)
        {
            var bookEntity = _mapper.Parse(bookDto);
            bookEntity = _repository.Update(bookEntity);

            return _mapper.Parse(bookEntity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}
