using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class FeeDefaulter
    {
        public int studentID { get; set; }
        public string studentName { get; set; }
        public string studentRegistrationCode { get; set; }
        public int totalAmount { get; set; }
        public int classID { get; set; }
        public string className { get; set; }
        public int sectionID { get; set; }
        public string sectionName { get; set; }
        public int branch_department_section_id { get; set; }
        public string status { get; set; }
    }
}