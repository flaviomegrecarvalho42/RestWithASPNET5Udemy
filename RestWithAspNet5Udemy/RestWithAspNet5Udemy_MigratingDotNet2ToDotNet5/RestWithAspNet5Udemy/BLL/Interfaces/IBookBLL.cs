using RestWithAspNet5Udemy.Data.DTO;
using System.Collections.Generic;

namespace RestWithAspNet5Udemy.BLL.Interfaces
{
    public interface IBookBLL
    {
        BookDto Create(BookDto book);
        BookDto FindById(long id);
        List<BookDto> FindAll();
        BookDto Update(BookDto book);
        void Delete(long id);
    }
}
