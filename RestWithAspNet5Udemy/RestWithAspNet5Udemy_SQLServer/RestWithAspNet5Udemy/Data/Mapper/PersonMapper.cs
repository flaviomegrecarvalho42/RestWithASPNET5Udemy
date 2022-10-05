using RestWithAspNet5Udemy.Data.DTO;
using RestWithAspNet5Udemy.Data.Mapper.Interfaces;
using RestWithAspNet5Udemy.Models;
using System.Collections.Generic;
using System.Linq;

namespace RestWithAspNet5Udemy.Data.Mapper
{
    public class PersonMapper : IParser<PersonDto, Person>, IParser<Person, PersonDto>
    {
        public Person Parse(PersonDto originDto)
        {
            if (originDto == null)
                return null;

            return new Person
            {
                Address = originDto.Address,
                FirstName = originDto.FirstName,
                Gender = originDto.Gender,
                Id = originDto.Id,
                LastName = originDto.LastName
            };
        }

        public PersonDto Parse(Person origin)
        {
            if (origin == null)
                return null;

            return new PersonDto
            {
                Address = origin.Address,
                FirstName = origin.FirstName,
                Gender = origin.Gender,
                Id = origin.Id,
                LastName = origin.LastName
            };
        }

        public List<Person> Parse(List<PersonDto> originsDto)
        {
            if (originsDto == null)
                return null;

            return originsDto.Select(item => Parse(item)).ToList();
        }

        public List<PersonDto> Parse(List<Person> origins)
        {
            if (origins == null)
                return null;

            return origins.Select(item => Parse(item)).ToList();
        }
    }
}
