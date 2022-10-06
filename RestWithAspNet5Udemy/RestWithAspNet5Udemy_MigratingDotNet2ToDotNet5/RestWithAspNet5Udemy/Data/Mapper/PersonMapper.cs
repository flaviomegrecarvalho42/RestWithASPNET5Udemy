using System.Collections.Generic;
using RestWithAspNet5Udemy.Data.DTO;
using System.Linq;
using RestWithAspNet5Udemy.Data.Mapper.Interfaces;
using RestWithAspNet5Udemy.Models;

namespace RestWithAspNet5Udemy.Data.Mapper
{
    public class PersonMapper : IParser<PersonDto, Person>, IParser<Person, PersonDto>
    {
        public Person Parse(PersonDto origin)
        {
            if (origin == null)
                return new Person();

            return new Person
            {
                Id = origin.Id,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Address = origin.Address,
                Gender = origin.Gender
            };
        }

        public PersonDto Parse(Person origin)
        {
            if (origin == null)
                return new PersonDto();

            return new PersonDto
            {
                Id = origin.Id,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Address = origin.Address,
                Gender = origin.Gender
            };
        }

        public List<Person> ParseList(List<PersonDto> origin)
        {
            if (origin == null)
                return new List<Person>();

            return origin.Select(item => Parse(item)).ToList();
        }

        public List<PersonDto> ParseList(List<Person> origin)
        {
            if (origin == null)
                return new List<PersonDto>();

            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
