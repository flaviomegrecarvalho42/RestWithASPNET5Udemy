using RestWithAspNet5Udemy.BLL.Interfaces;
using RestWithAspNet5Udemy.Data.DTO;
using RestWithAspNet5Udemy.Data.Mapper;
using RestWithAspNet5Udemy.Hypermedia.Utils;
using RestWithAspNet5Udemy.Repositories.Interfaces;
using System.Collections.Generic;

namespace RestWithAspNet5Udemy.BLL
{
    public class PersonBLL : IPersonBLL
    {
        private readonly IPersonRepository _repository;
        private readonly PersonMapper _mapper;

        public PersonBLL(IPersonRepository repository)
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

        public List<PersonDto> FindByName(string firstName, string lastName)
        {
            return _mapper.Parse(_repository.FindByName(firstName, lastName));
        }

        public PagedSearchDto<PersonDto> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            var sort = (!string.IsNullOrWhiteSpace(sortDirection)) && !sortDirection.Equals("desc") ? "asc" : "desc";
            var size = (pageSize < 1) ? 10 : pageSize;
            var offSet = page > 0 ? (page - 1) * size : 0;

            string query = @"SELECT * FROM person p WHERE 1 = 1";
            string countQuery = @"SELECT COUNT(*) FROM person p WHERE 1 = 1";

            if (!string.IsNullOrWhiteSpace(name))
            { 
                query += $" AND p.first_Name like '%{name}%'";
                countQuery += $" AND p.first_Name like '%{name}%'";
            }

            query += $" ORDER BY p.first_Name {sort} LIMIT {size} OFFSET {offSet}";

            var persons = _repository.FindWithPagedSearch(query);
            int totalRecords = _repository.GetCount(countQuery);

            return new PagedSearchDto<PersonDto> {
                CurrentPage = page,
                List = _mapper.Parse(persons),
                PageSize = size,
                SortDirections = sort,
                TotalResults = totalRecords
            };
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

        public PersonDto Disable(long id)
        {
            var personEntity = _repository.Disable(id);

            return _mapper.Parse(personEntity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}
