using RestWithAspNet5Udemy.Models;

namespace RestWithAspNet5Udemy.Repositories.Interfaces
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
        Person Disable (long id);
    }
}
