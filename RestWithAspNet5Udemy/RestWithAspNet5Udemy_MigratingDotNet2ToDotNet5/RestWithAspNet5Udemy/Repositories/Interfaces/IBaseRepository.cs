using RestWithAspNet5Udemy.Models.Base;
using System.Collections.Generic;

namespace RestWithAspNet5Udemy.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        T Create(T item);
        T FindById(long id);
        List<T> FindAll();
        T Update(T item);
        void Delete(long id);
        bool Exists(long? id);
        List<T> FindWithPagedSearch(string query);
        int GetCount(string query);
    }
}
