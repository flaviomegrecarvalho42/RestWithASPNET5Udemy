using System.Collections.Generic;
using RestWithAspNet5Udemy.Models;
using RestWithAspNet5Udemy.BLL.Interfaces;
using RestWithAspNet5Udemy.Repositories.Interfaces;
using RestWithAspNet5Udemy.Data.Mapper;
using RestWithAspNet5Udemy.Data.DTO;
using RestWithAspNet5Udemy.Hypermedia.Utils;

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

        public PagedSearchDto<BookDto> FindWithPagedSearch(string title, string sortDirection, int pageSize, int page)
        {
            var sort = (!string.IsNullOrWhiteSpace(sortDirection)) && !sortDirection.Equals("desc") ? "asc" : "desc";
            var size = (pageSize < 1) ? 10 : pageSize;
            var offset = page > 0 ? (page - 1) * size : 0;

            string query = @"SELECT * FROM books b WHERE 1 = 1 ";
            string countQuery = @"SELECT count(*) FROM books b WHERE 1 = 1 ";

            if (!string.IsNullOrWhiteSpace(title))
            {
                query += $" AND b.title like '%{title}%' ";
                countQuery += countQuery + $" AND b.title like '%{title}%' ";
            }

            query += $" ORDER BY b.title {sort} LIMIT {size} OFFSET {offset}";

            var books = _repository.FindWithPagedSearch(query);
            int totalRecords = _repository.GetCount(countQuery);

            return new PagedSearchDto<BookDto>
            {
                CurrentPage = page,
                List = _mapper.ParseList(books),
                PageSize = size,
                SortDirections = sort,
                TotalResults = totalRecords
            };
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