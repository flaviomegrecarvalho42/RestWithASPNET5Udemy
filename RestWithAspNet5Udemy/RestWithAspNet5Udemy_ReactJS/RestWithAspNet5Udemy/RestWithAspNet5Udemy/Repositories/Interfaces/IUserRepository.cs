using RestWithAspNet5Udemy.Data.DTO;
using RestWithAspNet5Udemy.Models;

namespace RestWithAspNet5Udemy.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User ValidateCredentials(UserDto userDto);
        User RefreshCredentials(string userName);
        User RefreshUserInfo(User user);
        bool RevokeToken(string userName);
    }
}
