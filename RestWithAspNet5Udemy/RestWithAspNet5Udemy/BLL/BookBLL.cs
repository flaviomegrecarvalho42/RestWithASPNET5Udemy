using RestWithAspNet5Udemy.BLL.Interfaces;
using RestWithAspNet5Udemy.Models;
using RestWithAspNet5Udemy.Repositories.Interfaces;
using System.Collections.Generic;

namespace RestWithAspNet5Udemy.BLL
{
    public class BookBLL : IBookBLL
    {
        private readonly IBookRepository _repository;

        public BookBLL(IBookRepository repository)
        {
            _repository = repository;
        }

        public List<Book> FindAll()
        {
            return _repository.FindAll();
        }

        public Book FindById(long id)
        {
            return _repository.FindById(id);
        }

        public Book Create(Book book)
        {
            return _repository.Create(book);
        }

        public Book Update(Book book)
        {
            return _repository.Update(book);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}
