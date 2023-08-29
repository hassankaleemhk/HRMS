using BusinessLogicLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Controllers
{
    [ApiController]
    [Route("api/salary")]
    //[Authorize]
    public class SalaryController : Controller
    {
        private readonly SalaryLogic _salaryLogic;

        public SalaryController(SalaryLogic salaryLogic)
        {
            _salaryLogic = salaryLogic;
        }

        [HttpGet]
        public async Task<IActionResult> GetLeaves()
        {
            var result = await _salaryLogic.GetAllSalaries();
            return Ok(result);
        }
        [HttpPost]
        
        public async Task<IActionResult> Index(SalaryViewModel salaryViewModel)
        {
            bool result =await _salaryLogic.InsertSalary(salaryViewModel);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> ApproveOrRejectLeave(int id, [FromQuery] int amount)
        {
            bool result = await _salaryLogic.EditData(id, amount);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<bool> DeleteRequest(int leaveId)
        {
            return await _salaryLogic.DeleteSalary(leaveId);

        }
    }
}
