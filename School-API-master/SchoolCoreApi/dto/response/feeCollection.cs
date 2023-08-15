using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class feeCollection
    {
        public int studentMonthlyFeesID { get; set; }
        public int studentID { get; set; }
        public string studentName { get; set; }
        public string studentRegistrationCode { get; set; }
        public string feesDueDate { get; set; }
        public string instrumentDate { get; set; }
        public int finAmount { get; set; }
        public int feesDueDateValue { get; set; }
        public string studentMonthlyFeesDate { get; set; }
        public int collectionAmount { get; set; }
        public int studentMonthlyFeesAmount { get; set; }
        public int closingFlag { get; set; }
        public int classID { get; set; }
        public int sectionID { get; set; }
        public int branch_department_section_id { get; set; }    
    }
}