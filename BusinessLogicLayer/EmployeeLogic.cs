using DatabaseAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class EmployeeLogic
    {
        private readonly HRMSDbcontext _context;
        private readonly IConfiguration _configuration;

        public EmployeeLogic(HRMSDbcontext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            var users = await _context.Employees.Select(e => new Employee
            {
                // Map the necessary properties of Employee entity
                EmployeeId = e.EmployeeId,
                Name = e.Name,
                RoleId = e.RoleId,
                Role = e.Role,
                DepartmentId = e.DepartmentId,
                Username = e.Username,
                Password = e.Password
                // Add more properties as needed

            }).ToListAsync();

            return users;
        }
        public async Task<Employee> GetoneEmployee(int id)
        {
            var user = await _context.Employees.FindAsync(id);
            return user;
        }

        public async Task<bool> InsertRole(RoleViewModel roleViewModel)
        {
            var role = new Role()
            {
                RoleName = roleViewModel.RoleName,
            };
            await _context.AddAsync(role);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteRole(int roleId)
        {
            var role = await _context.Roles.FindAsync(roleId);
            if (role == null)
            {
                return false; // Role not found
            }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return true; // Role deleted successfully
        }
        public async Task<bool> UpdateEmployee(EmployeeViewModel model)
        {
            var employee = await _context.Employees.FindAsync(model.EmployeeId);

            if (employee != null)
            {
                employee.Name = model.Name;
                employee.DepartmentId = model.DepartmentId;
                employee.RoleId = model.RoleId;
                employee.PhoneNumber = model.PhoneNumber;
                employee.Address = model.Address;

                await _context.SaveChangesAsync();
                return true;
            }

            return false; // Employee not found
        }
       
    }
}
