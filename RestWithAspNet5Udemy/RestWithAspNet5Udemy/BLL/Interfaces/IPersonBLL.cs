using RestWithAspNet5Udemy.Data.DTO;
using RestWithAspNet5Udemy.Hypermedia.Utils;
using System.Collections.Generic;

namespace RestWithAspNet5Udemy.BLL.Interfaces
{
    public interface IPersonBLL
    {
        /// <summary>
        /// Method responsible for returning all people
        /// </summary>
        /// <returns></returns>
        List<PersonDto> FindAll();

        /// <summary>
        /// Method responsible for returning one person by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        PersonDto FindById(long id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        List<PersonDto> FindByName(string firstName, string lastName);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        PagedSearchDto<PersonDto> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page);

        /// <summary>
        /// Method responsible to crete one new person
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        PersonDto Create(PersonDto person);

        /// <summary>
        /// Method responsible for updating one person
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        PersonDto Update(PersonDto person);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        PersonDto Disable(long id);

        /// <summary>
        /// Method responsible for deleting a person from an ID
        /// </summary>
        /// <param name="id"></param>
        void Delete(long id);
    }
}
