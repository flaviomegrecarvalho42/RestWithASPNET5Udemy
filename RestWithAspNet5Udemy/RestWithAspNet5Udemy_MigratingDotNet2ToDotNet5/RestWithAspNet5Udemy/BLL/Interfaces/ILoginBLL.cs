using RestWithAspNet5Udemy.Data.DTO;

namespace RestWithAspNet5Udemy.BLL.Interfaces
{
    public interface ILoginBLL
    {
         object FindByLogin(UserDto user);
    }
}
