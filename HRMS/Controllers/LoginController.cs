using BusinessLogicLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : Controller
    {
        private readonly LoginLogic _loginLogic;

        public LoginController(LoginLogic loginLogic)
        {
            _loginLogic = loginLogic;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] EmployeeViewModel model, [FromServices] IConfiguration configuration)
        {
            try
            {
                string token = await _loginLogic.Login(model, configuration);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                // This will return a 500 status code with the exception message,
                // which might help with debugging
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

    }
}
