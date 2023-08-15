using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class SessionReportCard
    {
        public int studentID { get; set; }
        public string studentRegistrationCode { get; set; }
        public string studentName { get; set; }
        public int branch_department_section_id { get; set; }
        public string branch_name { get; set; }
        public int classID { get; set; }
        public string className { get; set; }
        public int sectionID { get; set; }
        public string sectionName { get; set; }
        public int parentID { get; set; }
        public string parentName { get; set; }
        public string parentPassportOrCNIC { get; set; }
        public int totalWorkingDays { get; set; }
        public int presentDays { get; set; }
        public string sessionTitle { get; set; }
        public string homeWorkJson { get; set; }
        public string examJson { get; set; }
    }
}