using BusinessLogicLayer;
using DatabaseAccessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Controllers
{
    [ApiController]
    [Route("api/employee")]
    [Authorize]

    public class EmployeeController : Controller
    {

        private readonly EmployeeLogic _context;
        public EmployeeController(EmployeeLogic context)
        {
            this._context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var result = await _context.GetAllEmployees();
            return Ok(result);
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetOneEmployee(int id)
        {
            var result = await _context.GetoneEmployee(id);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeViewModel model)
        {
            if (id != model.EmployeeId)
            {
                return BadRequest();
            }

            bool result = await _context.UpdateEmployee(model);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }
        [HttpDelete]
        public async Task<bool> DeleteRole(int roleId)
        {
            return await _context.DeleteRole(roleId);            

        }
        

    }
}
