using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class AttendanceViewModel
    {
        public int AttendanceId { get; set; }
        public int EmployeeId { get; set; }
        //public EmployeeViewModel Employee { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan TimeIn { get; set; }
        public TimeSpan TimeOut { get; set; }
    }
}
