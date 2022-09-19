using RestWithAspNet5Udemy.Data.DTO;

namespace RestWithAspNet5Udemy.BLL.Interfaces
{
    public interface ILoginBLL
    {
        TokenDto ValidateCredentials(UserDto userDto);
        TokenDto RefreshCredentials(TokenDto tokenDto);
        bool RevokeToken(string userName);
    }
}
