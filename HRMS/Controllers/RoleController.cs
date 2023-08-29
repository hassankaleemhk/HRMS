using BusinessLogicLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Controllers
{
    [ApiController]
    [Route("api/role")]
    public class RoleController : Controller
    {
        private readonly RoleLogic _roleLogic;

        public RoleController(RoleLogic roleLogic)
        {
            _roleLogic = roleLogic;
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateRole([FromBody] RoleViewModel roleViewModel, [FromHeader(Name = "Authorization")] string authorizationHeader)
        {
            try
            {
                string token = authorizationHeader.Replace("Bearer ", "");

                bool result = await _roleLogic.UpdateRole(roleViewModel, token);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
