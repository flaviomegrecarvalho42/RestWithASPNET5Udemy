using System.Collections.Generic;
using RestWithAspNet5Udemy.Repositories.Interfaces;
using RestWithAspNet5Udemy.Data.Mapper;
using RestWithAspNet5Udemy.Data.DTO;
using RestWithAspNet5Udemy.BLL.Interfaces;
using RestWithAspNet5Udemy.Hypermedia.Utils;

namespace RestWithAspNet5Udemy.BLL
{
    public class PersonBLL : IPersonBLL
    {
        private IPersonRepository _repository;
        private readonly PersonMapper _mapper;

        public PersonBLL(IPersonRepository repository)
        {
            _repository = repository;
            _mapper = new PersonMapper();
        }

        public PersonDto Create(PersonDto person)
        {
            var personEntity = _mapper.Parse(person);
            personEntity = _repository.Create(personEntity);
            return _mapper.Parse(personEntity);
        }

        public PersonDto FindById(long id)
        {
            return _mapper.Parse(_repository.FindById(id));
        }

        public List<PersonDto> FindAll()
        {
            return _mapper.ParseList(_repository.FindAll());
        }

        public List<PersonDto> FindByName(string firstName, string lastName)
        {
            return _mapper.ParseList(_repository.FindByName(firstName, lastName));
        }

        public PersonDto Update(PersonDto person)
        {
            var personEntity = _mapper.Parse(person);
            personEntity = _repository.Update(personEntity);
            return _mapper.Parse(personEntity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public PagedSearchDto<PersonDto> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            page = page > 0 ? page - 1 : 0;
            string query = @"select * from Persons p where 1 = 1 ";
            if (!string.IsNullOrEmpty(name)) query = query + $" and p.firstName like '%{name}%'";
            
            query = query + $" order by p.firstName {sortDirection} limit {pageSize} offset {page}";

            string countQuery = @"select count(*) from Persons p where 1 = 1 ";
            if (!string.IsNullOrEmpty(name)) countQuery = countQuery + $" and p.firstName like '%{name}%'";

            var persons = _repository.FindWithPagedSearch(query);

            int totalResults = _repository.GetCount(countQuery);

            return new PagedSearchDto<PersonDto>
            {
                CurrentPage = page + 1,
                List = _mapper.ParseList(persons),
                PageSize = pageSize,
                SortDirections = sortDirection,
                TotalResults = totalResults
            };
        }
        
        public bool Exists(long id)
        {
            return _repository.Exists(id);
        }
    }
}
