using BusinessLogicLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HRMS.Controllers
{
    [ApiController]
    [Route("api/leave")]
    //[Authorize]
    
    public class LeaveController : Controller
    {
        private readonly LeaveLogic _context;

        public LeaveController(LeaveLogic context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetLeaves()
        {
            var result = await _context.GetAllLeaves();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> RequestLeave([FromBody] LeaveViewModel model)
        {
            bool result = await _context.RequestLeave(model);

            if (result)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> ApproveOrRejectLeave(int id, [FromQuery] string status)
        {
            if (Enum.TryParse<LeaveLogic.LeaveStatus>(status, out var leaveStatus))
            {
                bool result = await _context.ApproveOrRejectLeave(id, leaveStatus);
                return Ok(result);
            }
            else
            {
                return BadRequest("Invalid status");
            }
        }

        [HttpDelete]
        public async Task<bool> DeleteRequest(int leaveId)
        {
            return await _context.DeleteLeaves(leaveId);

        }
    }
}
