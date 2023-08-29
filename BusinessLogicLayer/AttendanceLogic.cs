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
    public class AttendanceLogic
    {
        private readonly HRMSDbcontext _context;
        public AttendanceLogic(HRMSDbcontext context)
        {
            _context = context;
        }
        public async Task<List<Attendance>> GetAllAttendance()
        {
            var users = await _context.Attendance.ToListAsync();
            return users;
        }
        public async Task<Employee> GetoneAttendance(int id)
        {
            var user = await _context.Employees.FindAsync(id);
            return user;
        }
        public async Task<bool> InsertAttendance(AttendanceViewModel attendanceViewModel)
        {
            var attendance = new Attendance()
            {
                AttendanceId = attendanceViewModel.AttendanceId,
                EmployeeId = attendanceViewModel.EmployeeId,
                Date = attendanceViewModel.Date,
                TimeIn = attendanceViewModel.TimeIn,
                TimeOut = attendanceViewModel.TimeOut,
            };
            await _context.AddAsync(attendance);
            int rowsAffected = await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateAttendance(AttendanceViewModel model)
        {
            var employee = await _context.Attendance.FindAsync(model.EmployeeId);

            if (employee != null)
            {
                employee.TimeOut = model.TimeOut;
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAttendance(int roleId)
        {
            var att = await _context.Attendance.FindAsync(roleId);
            if (att == null)
            {
                return false;
            }

            _context.Attendance.Remove(att);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
