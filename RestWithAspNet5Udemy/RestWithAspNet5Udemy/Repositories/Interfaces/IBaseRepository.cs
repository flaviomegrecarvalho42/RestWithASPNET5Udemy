using RestWithAspNet5Udemy.Models.Base;
using System.Collections.Generic;

namespace RestWithAspNet5Udemy.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Method responsible for returning all people
        /// </summary>
        /// <returns></returns>
        List<T> FindAll();

        /// <summary>
        /// Method responsible for returning one person by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T FindById(long id);

        /// <summary>
        /// Method responsible to crete one new person
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        T Create(T item);

        /// <summary>
        /// Method responsible for updating one person
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        T Update(T item);

        /// <summary>
        /// Method responsible for deleting a person from an ID
        /// </summary>
        /// <param name="id"></param>
        void Delete(long id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Exists(long id);
    }
}
