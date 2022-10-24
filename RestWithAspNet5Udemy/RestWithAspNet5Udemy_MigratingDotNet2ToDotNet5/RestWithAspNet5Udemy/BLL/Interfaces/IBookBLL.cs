using RestWithAspNet5Udemy.Data.DTO;
using RestWithAspNet5Udemy.Hypermedia.Utils;
using System.Collections.Generic;

namespace RestWithAspNet5Udemy.BLL.Interfaces
{
    public interface IBookBLL
    {
        BookDto Create(BookDto book);
        BookDto FindById(long id);
        List<BookDto> FindAll();
        PagedSearchDto<BookDto> FindWithPagedSearch(string title, string sortDirection, int pageSize, int page);
        BookDto Update(BookDto book);
        void Delete(long id);
    }
}
