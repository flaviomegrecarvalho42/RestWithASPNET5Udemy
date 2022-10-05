using RestWithAspNet5Udemy.Data.DTO;

namespace RestWithAspNet5Udemy.BLL.Interfaces
{
    public interface ILoginBLL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        TokenDto ValidateCredentials(UserDto userDto);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tokenDto"></param>
        /// <returns></returns>
        TokenDto RefreshCredentials(TokenDto tokenDto);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        bool RevokeToken(string userName);
    }
}
