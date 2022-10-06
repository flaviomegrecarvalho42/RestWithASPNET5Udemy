using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RestWithAspNet5Udemy.BLL.Interfaces;
using RestWithAspNet5Udemy.Data.DTO;

namespace RestWithAspNet5Udemy.Controllers
{
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class LoginController : Controller
    {
        private ILoginBLL _loginBLL;

        public LoginController(ILoginBLL loginBLL)
        {
            _loginBLL = loginBLL;
        }

        [AllowAnonymous]
        [HttpPost]
        public object Post([FromBody] UserDto userDto)
        {
            if (userDto == null)
                return BadRequest();

            return _loginBLL.FindByLogin(userDto);
        }
    }
}
