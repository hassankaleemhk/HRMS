using BusinessLogicLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Controllers
{
    [ApiController]
    [Route("api/attendance")]
    public class AttendanceController : Controller
    {
        private readonly AttendanceLogic _context;
        public AttendanceController(AttendanceLogic context)
        {
            this._context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAttendance()
        {
            var result = await _context.GetAllAttendance();
            return Ok(result);
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetOneAttendance(int id)
        {
            var result = await _context.GetoneAttendance(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Attendance(AttendanceViewModel attendanceViewModel)
        {
            var result = await _context.InsertAttendance(attendanceViewModel);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAttendance(int id, [FromBody] AttendanceViewModel model)
        {
            if (id != model.EmployeeId)
            {
                return BadRequest();
            }

            bool result = await _context.UpdateAttendance(model);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete]
        public async Task<bool> DeleteAttendance(int roleId)
        {
            return await _context.DeleteAttendance(roleId);

        }
    }
}
