using RestWithAspNet5Udemy.Models;
using System.Collections.Generic;

namespace RestWithAspNet5Udemy.BLL.Interfaces
{
    public interface IPersonBLL
    {
        /// <summary>
        /// Method responsible for returning all people
        /// </summary>
        /// <returns></returns>
        List<Person> FindAll();

        /// <summary>
        /// Method responsible for returning one person by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Person FindById(long id);

        /// <summary>
        /// Method responsible to crete one new person
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        Person Create(Person person);

        /// <summary>
        /// Method responsible for updating one person
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        Person Update(Person person);

        /// <summary>
        /// Method responsible for deleting a person from an ID
        /// </summary>
        /// <param name="id"></param>
        void Delete(long id);
    }
}
