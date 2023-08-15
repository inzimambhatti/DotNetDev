using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class TotalStudentsFee
    {
        public int totalStudent { get; set; }
        public int totalAmount { get; set; }
        public int paidFee { get; set; }
        public int paidFeeStudent { get; set; }
        public int RemainingFee { get; set; }
        public int RemainingFeeStudent { get; set; }
        public int lateFine { get; set; }
        public int lateFineStudents { get; set; }
        public int branch_department_section_id { get; set; }
    }
}