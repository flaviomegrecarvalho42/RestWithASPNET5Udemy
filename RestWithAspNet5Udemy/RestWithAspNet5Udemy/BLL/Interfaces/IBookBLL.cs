using RestWithAspNet5Udemy.Data.DTO;
using RestWithAspNet5Udemy.Models;
using System.Collections.Generic;

namespace RestWithAspNet5Udemy.BLL.Interfaces
{
    public interface IBookBLL
    {
        /// <summary>
        /// Method responsible for returning all books
        /// </summary>
        /// <returns></returns>
        List<BookDto> FindAll();

        /// <summary>
        /// Method responsible for returning one book by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BookDto FindById(long id);

        /// <summary>
        /// Method responsible to crete one new book
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        BookDto Create(BookDto book);

        /// <summary>
        /// Method responsible for updating one book
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        BookDto Update(BookDto book);

        /// <summary>
        /// Method responsible for deleting a book from an ID
        /// </summary>
        /// <param name="id"></param>
        void Delete(long id);
    }
}
