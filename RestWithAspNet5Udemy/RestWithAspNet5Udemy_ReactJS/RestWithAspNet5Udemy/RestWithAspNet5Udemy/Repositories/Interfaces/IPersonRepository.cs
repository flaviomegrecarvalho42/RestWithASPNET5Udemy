using RestWithAspNet5Udemy.Models;
using System.Collections.Generic;

namespace RestWithAspNet5Udemy.Repositories.Interfaces
{ 
    public interface IPersonRepository : IBaseRepository<Person>
    {
        List<Person> FindByName(string fristName, string lastName);
        Person Disable(long id);
    }
}
