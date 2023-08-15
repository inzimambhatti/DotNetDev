using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class FeeGenerationDetails
    {
        public int studenFeesPlanID { get; set; }
        public int studentID { get; set; }
        public int feeGenerated { get; set; }
        public int isPublished { get; set; }
        public string studentName { get; set; }
        public string studentDOB { get; set; }
        public string studentRegistrationCode { get; set; }
        public float studentFeesElementAmount { get; set; }
        public string className { get; set; }
        public int classID { get; set; }
        public int sectionID { get; set; }
        public string studentMonthlyFeesDate { get; set; }
        public int branch_department_section_id { get; set; }
        public string sectionName { get; set; }
        public string branch_name { get; set; }
    }
}