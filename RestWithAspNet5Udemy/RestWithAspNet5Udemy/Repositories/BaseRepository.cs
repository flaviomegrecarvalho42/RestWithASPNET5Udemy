using Microsoft.EntityFrameworkCore;
using RestWithAspNet5Udemy.Models.Base;
using RestWithAspNet5Udemy.Models.Context;
using RestWithAspNet5Udemy.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestWithAspNet5Udemy.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly MySQLContext _context;
        private DbSet<T> _dataSet;

        public BaseRepository(MySQLContext context)
        {
            _context = context;
            _dataSet = _context.Set<T>();
        }

        public List<T> FindAll()
        {
            return _dataSet.ToList();
        }

        public T FindById(long id)
        {
            return _dataSet.SingleOrDefault(p => p.Id.Equals(id));
        }

        public T Create(T item)
        {
            try
            {
                _dataSet.Add(item);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

            return item;
        }

        public T Update(T item)
        {
            // We check if the person exists in the database
            // If it doesn't exist we return an empty person instance
            if (!Exists(item.Id))
                return null;

            try
            {
                // Get the current status of the record in the database
                var result = _dataSet.SingleOrDefault(p => p.Id.Equals(item.Id));

                if (result == null)
                    return item;

                // set changes and save
                _context.Entry(result).CurrentValues.SetValues(item);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

            return item;
        }

        public void Delete(long id)
        {
            try
            {
                var result = _dataSet.SingleOrDefault(p => p.Id.Equals(id));

                if (result != null)
                {
                    _dataSet.Remove(result);
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
            return _dataSet.Any(p => p.Id.Equals(id));
        }
    }
}
