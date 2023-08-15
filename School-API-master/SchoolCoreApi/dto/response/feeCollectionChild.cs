using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class FeeCollectionChild
    {
        public int studentMonthlyFeesID { get; set; }
        public string studentMonthlyFeesDate { get; set; }
        public int closingFlag { get; set; }
        public int feesDueDate { get; set; }
        public int feesDueDateValue { get; set; }
        public int classID { get; set; }
        public int sectionID { get; set; }
        public int branch_department_section_id { get; set; }    
    }
}