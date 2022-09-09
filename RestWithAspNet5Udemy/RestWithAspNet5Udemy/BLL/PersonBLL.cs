using RestWithAspNet5Udemy.BLL.Interfaces;
using RestWithAspNet5Udemy.Data.DTO;
using RestWithAspNet5Udemy.Data.Mapper;
using RestWithAspNet5Udemy.Models;
using RestWithAspNet5Udemy.Repositories.Interfaces;
using System.Collections.Generic;

namespace RestWithAspNet5Udemy.BLL
{
    public class PersonBLL : IPersonBLL
    {
        private readonly IBaseRepository<Person> _repository;
        private readonly PersonMapper _mapper;

        public PersonBLL(IBaseRepository<Person> repository)
        {
            _repository = repository;
            _mapper = new PersonMapper();
        }

        public List<PersonDto> FindAll()
        {
            return _mapper.Parse(_repository.FindAll());
        }

        public PersonDto FindById(long id)
        {
            return _mapper.Parse(_repository.FindById(id));
        }

        public PersonDto Create(PersonDto personDto)
        {
            var personEntity = _mapper.Parse(personDto);
            personEntity = _repository.Create(personEntity);
            
            return _mapper.Parse(personEntity);
        }

        public PersonDto Update(PersonDto personDto)
        {
            var personEntity = _mapper.Parse(personDto);
            personEntity = _repository.Update(personEntity);

            return _mapper.Parse(personEntity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}
