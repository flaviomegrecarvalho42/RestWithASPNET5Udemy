using RestWithAspNet5Udemy.BLL.Interfaces;
using RestWithAspNet5Udemy.Models;
using RestWithAspNet5Udemy.Repositories.Interfaces;
using System.Collections.Generic;

namespace RestWithAspNet5Udemy.BLL
{
    public class PersonBLL : IPersonBLL
    {
        private readonly IPersonRepository _repository;

        public PersonBLL(IPersonRepository repository)
        {
            _repository = repository;
        }

        public List<Person> FindAll()
        {
            return _repository.FindAll();
        }

        public Person FindById(long id)
        {
            return _repository.FindById(id);
        }

        public Person Create(Person person)
        {
            return _repository.Create(person);
        }

        public Person Update(Person person)
        {
            return _repository.Update(person);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}
