using DatabaseAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class SalaryLogic
    {
        private readonly HRMSDbcontext _context;
        public SalaryLogic(HRMSDbcontext context)
        {
            _context = context;
        }
        public async Task<List<Salary>> GetAllSalaries()
        {
            var users = await _context.Salaries.ToListAsync();
            return users;
        }
        public async Task<bool> InsertSalary(SalaryViewModel salaryViewModel)
        {
            var salary = new Salary()
            {
                EmployeeId = salaryViewModel.EmployeeId,
                Amount = salaryViewModel.Amount,
                EffectiveDate = salaryViewModel.EffectiveDate,
            };
            await _context.AddAsync(salary);
            int rowsAffected = await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> EditData(int Id, int amount)
        {
            var salary = await _context.Salaries.FindAsync(Id);

            if (salary != null)
            {
                salary.Amount = amount;
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
        public async Task<bool> DeleteSalary(int leaveId)
        {
            var leave = await _context.Salaries.FindAsync(leaveId);
            if (leave == null)
            {
                return false; // Leave not found
            }

            _context.Salaries.Remove(leave);
            await _context.SaveChangesAsync();
            return true; // Role deleted successfully
        }

    }
}
