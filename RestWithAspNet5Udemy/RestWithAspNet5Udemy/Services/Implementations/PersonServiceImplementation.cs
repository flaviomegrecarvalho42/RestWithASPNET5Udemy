using RestWithAspNet5Udemy.Models;
using RestWithAspNet5Udemy.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestWithAspNet5Udemy.Services.Implementations
{
    public class PersonServiceImplementation : IPersonService
    {
        private readonly MySQLContext _context;

        public PersonServiceImplementation(MySQLContext context)
        {
            _context = context;
        }

        public List<Person> FindAll()
        {
            return _context.Persons.ToList();
        }

        public Person FindById(long id)
        {
            return _context.Persons.SingleOrDefault(p => p.Id == id);
        }

        public Person Create(Person person)
        {
            try
            {
                _context.Add(person);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return person;
        }

        public Person Update(Person person)
        {
            if (!Exists(person.Id))
                return new Person();

            try
            {
                var result = _context.Persons.SingleOrDefault(p => p.Id == person.Id);

                if (result == null)
                    return person;

                _context.Entry(result).CurrentValues.SetValues(person);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return person;
        }

        public void Delete(long id)
        {
            try
            {
                var result = _context.Persons.SingleOrDefault(p => p.Id == id);
                if (result != null)
                {
                    _context.Remove(result);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool Exists(long id)
        {
            return _context.Persons.Any(p => p.Id.Equals(id));
        }
    }
}
