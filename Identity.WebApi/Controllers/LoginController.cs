using Identity.WebApi.Interfaces;
using Identity.WebApi.Models;
using Identity.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.WebApi.Controllers
{
    [Route("user")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] Login loginParam)
        {
            var token = _loginService.Authenticate(loginParam.Username, loginParam.Password);

            if (token == null)
                return BadRequest(new { message = "Either username or password is incorrect. Please try again." });

            return Ok(token);
        }
    }
}
