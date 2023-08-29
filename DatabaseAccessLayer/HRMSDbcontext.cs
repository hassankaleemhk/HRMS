using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLayer
{
    public class HRMSDbcontext : Microsoft.EntityFrameworkCore.DbContext
    {
        public HRMSDbcontext(DbContextOptions<HRMSDbcontext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Attendance> Attendance { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<Leave> Leaves { get; set; }
    }
}
