using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWithAspNet5Udemy.BLL.Interfaces;
using RestWithAspNet5Udemy.Data.DTO;

namespace RestWithAspNet5Udemy.Controllers
{
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private ILoginBLL _loginBLL;

        public AuthController(ILoginBLL loginBLL)
        {
            _loginBLL = loginBLL;
        }

        [HttpPost]
        [Route("signin")]
        public IActionResult Signin([FromBody] UserDto userDto)
        {
            if (userDto == null)
                return BadRequest("Invalid client request");

            var token = _loginBLL.ValidateCredentials(userDto);
            
            if (token == null)
                return Unauthorized();

            return Ok(token);
        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh([FromBody] TokenDto tokenDto)
        {
            if (tokenDto == null)
                return BadRequest("Invalid client request");

            var token = _loginBLL.RefreshCredentials(tokenDto);

            if (token == null)
                return BadRequest("Invalid client request");

            return Ok(token);
        }

        [HttpGet]
        [Route("revoke")]
        [Authorize("Bearer")]
        public IActionResult Revoke()
        {
            var userName = User.Identity.Name;
            var result = _loginBLL.RevokeToken(userName);

            if (!result)
                return BadRequest("Invalid client request");

            return NoContent();
        }

    }
}
