using RestWithAspNet5Udemy.BLL.Interfaces;
using RestWithAspNet5Udemy.Data.DTO;
using RestWithAspNet5Udemy.Data.Mapper;
using RestWithAspNet5Udemy.Hypermedia.Utils;
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

        public PagedSearchDto<BookDto> FindWithPagedSearch(string title, string sortDirection, int pageSize, int page)
        {
            var sort = (!string.IsNullOrWhiteSpace(sortDirection)) && !sortDirection.Equals("desc") ? "asc" : "desc";
            var size = (pageSize < 1) ? 10 : pageSize;
            var offset = page > 0 ? (page - 1) * size : 0;

            string query = @"SELECT * FROM books p WHERE 1 = 1 ";
            string countQuery = @"SELECT COUNT(*) FROM books p WHERE 1 = 1 ";

            if (!string.IsNullOrWhiteSpace(title))
            {
                query += $" AND p.title like '%{title}%' ";
                countQuery += $" AND p.title like '%{title}%' ";
            }

            query += $" ORDER BY p.title {sort} limit {size} offset {offset}";

            var books = _repository.FindWithPagedSearch(query);
            int totalResults = _repository.GetCount(countQuery);

            return new PagedSearchDto<BookDto>
            {
                CurrentPage = page,
                List = _mapper.Parse(books),
                PageSize = size,
                SortDirections = sort,
                TotalResults = totalResults
            };
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
