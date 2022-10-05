using RestWithAspNet5Udemy.Models;
using System.Collections.Generic;

namespace RestWithAspNet5Udemy.Repositories.Interfaces
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
        Person Disable (long id);
        List<Person> FindByName(string firstName, string lastName);
    }
}
