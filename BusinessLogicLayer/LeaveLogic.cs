using DatabaseAccessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class LeaveLogic
    {
        private readonly HRMSDbcontext _context;
        public LeaveLogic(HRMSDbcontext context)
        {
            _context = context;
        }
        public enum LeaveStatus
        {
            Pending,
            Approved,
            Rejected
        }
        public async Task<List<Leave>> GetAllLeaves()
        {
            var users = await _context.Leaves.ToListAsync();
            return users;
        }
        public async Task<bool> RequestLeave(LeaveViewModel model)
        {
            var leave = new Leave
            {
                EmployeeId = model.EmployeeId,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Reason = model.Reason,
                Status = "Pending"
            };

            _context.Leaves.Add(leave);
            await _context.SaveChangesAsync();
            return true;
        }

        //public async Task<bool> ApproveOrRejectLeave(int leaveId, string status)
        //{
        //    var leave = await _context.Leaves.FindAsync(leaveId);

        //    if (leave != null)
        //    {
        //        leave.Status = status;
        //        await _context.SaveChangesAsync();
        //        return true;
        //    }

        //    return false;
        //}

        public async Task<bool> ApproveOrRejectLeave(int leaveId, LeaveStatus status)
        {
            var leave = await _context.Leaves.FindAsync(leaveId);

            if (leave != null)
            {
                leave.Status = status.ToString();
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteLeaves(int leaveId)
        {
            var leave = await _context.Leaves.FindAsync(leaveId);
            if (leave == null)
            {
                return false; // Leave not found
            }

            _context.Leaves.Remove(leave);
            await _context.SaveChangesAsync();
            return true; // Role deleted successfully
        }

    }
}
