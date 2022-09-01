using RestWithAspNet5Udemy.Models;
using RestWithAspNet5Udemy.Models.Context;
using RestWithAspNet5Udemy.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestWithAspNet5Udemy.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly MySQLContext _context;

        public PersonRepository(MySQLContext context)
        {
            _context = context;
        }

        public List<Person> FindAll()
        {
            return _context.Persons.ToList();
        }

        public Person FindById(long id)
        {
            return _context.Persons.SingleOrDefault(p => p.Id.Equals(id));
        }

        public Person Create(Person person)
        {
            try
            {
                _context.Add(person);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

            return person;
        }

        public Person Update(Person person)
        {
            // We check if the person exists in the database
            // If it doesn't exist we return an empty person instance
            if (!Exists(person.Id))
                return null;

            try
            {
                // Get the current status of the record in the database
                var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(person.Id));

                if (result == null)
                    return person;

                // set changes and save
                _context.Entry(result).CurrentValues.SetValues(person);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

            return person;
        }

        public void Delete(long id)
        {
            try
            {
                var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(id));

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
            return _context.Persons.Any(p => p.Id.Equals(id));
        }
    }
}
