using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class StudentPromotionDetails
    {
        public int status { get; set; }
        public int studentID { get; set; }
        public string studentName { get; set; }
        public string studentRegistrationCode { get; set; }
        public string className { get; set; }
        public string sectionName { get; set; }
        public string branch_name { get; set; }
        public string departmentTypeName { get; set; }
        public int classID { get; set; }
        public int sectionID { get; set; }
        public int branch_department_section_id { get; set; }
        public int departmentTypeID { get; set; }
        public int transferStatus { get; set; }
        public int leftStatus { get; set; }
        public int passStatus { get; set; }
        public int parentID { get; set; }
        public string parentName { get; set; }
        public string parentPassportOrCNIC { get; set; }
    }
}