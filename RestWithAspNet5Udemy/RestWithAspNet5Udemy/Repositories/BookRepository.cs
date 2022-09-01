using RestWithAspNet5Udemy.Models;
using RestWithAspNet5Udemy.Models.Context;
using RestWithAspNet5Udemy.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestWithAspNet5Udemy.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly MySQLContext _context;

        public BookRepository(MySQLContext context)
        {
            _context = context;
        }

        public List<Book> FindAll()
        {
            return _context.Books.ToList();
        }

        public Book FindById(long id)
        {
            return _context.Books.SingleOrDefault(b => b.Id.Equals(id));
        }

        public Book Create(Book book)
        {
            try
            {
                _context.Add(book);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

            return book;
        }

        public Book Update(Book book)
        {
            // We check if the book exists in the database
            // If it doesn't exist we return an empty book instance
            if (!Exists(book.Id))
                return null;

            try
            {
                // Get the current status of the record in the database
                var result = _context.Books.SingleOrDefault(b => b.Id.Equals(book.Id));

                if (result == null)
                    return book;

                // set changes and save
                _context.Entry(result).CurrentValues.SetValues(book);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

            return book;
        }

        public void Delete(long id)
        {
            try
            {
                var result = _context.Books.SingleOrDefault(b => b.Id.Equals(id));

                if (result != null)
                {
                    _context.Remove(result);
                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Exists(long id)
        {
            return _context.Books.Any(b => b.Id.Equals(id));
        }
    }
}
