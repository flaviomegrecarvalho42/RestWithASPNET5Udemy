using RestWithAspNet5Udemy.Hypermedia.Interfaces;
using System;
using System.Collections.Generic;

namespace RestWithAspNet5Udemy.Hypermedia.Utils
{
    public class PagedSearchDto<T> where T : ISupportHyperMedia
    {
        public PagedSearchDto()
        {
        }

        public PagedSearchDto(int currentPage, int pageSize, string sortDirections)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            SortDirections = sortDirections;
        }

        public PagedSearchDto(int currentPage, string sortFields, string sortDirections) : this(currentPage, 10, sortDirections)
        {
        }

        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalResults { get; set; }
        public string SortDirections { get; set; }
        public List<T> List { get; set; }

        public int GetCurrentPage()
        {
            return CurrentPage == 0 ? 2 : CurrentPage;
        }

        public int GetPageSize()
        {
            return PageSize == 0 ? 10 : PageSize;
        }
    }
}
