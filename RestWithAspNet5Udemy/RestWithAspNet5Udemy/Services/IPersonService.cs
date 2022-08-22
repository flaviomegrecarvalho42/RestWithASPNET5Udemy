using RestWithAspNet5Udemy.Models;
using System.Collections.Generic;

namespace RestWithAspNet5Udemy.Services
{
    public interface IPersonService
    {
        List<Person> FindAll();
        Person FindById(long id);
        Person Create(Person person);
        Person Update(Person person);
        void Delete(long id);
    }
}
