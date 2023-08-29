using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLayer
{
    public class Salary
    {
        [Key]
        public int SalaryId { get; set; }
        [ForeignKey("EmployeeId")]

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public decimal Amount { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}
