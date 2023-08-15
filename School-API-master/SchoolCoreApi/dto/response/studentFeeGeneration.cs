using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class StudentFeeGeneration
    {
        public int studentID { get; set; }
        public string studentName { get; set; }
        public string studentRegistrationCode { get; set; }
        public string fatherName { get; set; }
        public string studentMonthlyFeesDate { get; set; }
        public string feesDueDate { get; set; }
        public int studentMonthlyFeesID { get; set; }
        public int siblingValue { get; set; }
        public string className { get; set; }
        public string sessionYear { get; set; }
        public string elementJson { get; set; }
        public string historyJson { get; set; }
        public string feeChallanDate { get; set; }
        public int feeChallanYear { get; set; }
        public int classID { get; set; }
        public int sectionID { get; set; }
        public int branch_department_section_id { get; set; }
    }
}