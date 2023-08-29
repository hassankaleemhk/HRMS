using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer
{
    public class SalaryViewModel
    {
        
        public int SalaryId { get; set; }

        public int EmployeeId { get; set; }
        //public EmployeeViewModel Employee { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public DateTime EffectiveDate { get; set; }

    }
}