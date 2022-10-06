using RestWithAspNet5Udemy.Models;

namespace RestWithAspNet5Udemy.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User FindByLogin(string login);
    }
}
