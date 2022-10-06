using RestWithAspNet5Udemy.Data.DTO;
using RestWithAspNet5Udemy.Hypermedia.Utils;
using System.Collections.Generic;

namespace RestWithAspNet5Udemy.BLL.Interfaces
{
    public interface IPersonBLL
    {
        PersonDto Create(PersonDto person);
        PersonDto FindById(long id);
        List<PersonDto> FindAll();
        List<PersonDto> FindByName(string fristName, string lastName);
        PersonDto Update(PersonDto person);
        void Delete(long id);
        PagedSearchDto<PersonDto> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page);
    }
}
