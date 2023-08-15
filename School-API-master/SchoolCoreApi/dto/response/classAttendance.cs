using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class ClassAttendance
    {
        public int teacherClassSectionID { get; set; }
        public int teacherID { get; set; }
        public string teacherFirstName { get; set; }
        public string teacherLastName { get; set; }
        public string sectionName { get; set; }
        public int sectionID { get; set; }
        public int classID { get; set; }
        public string className { get; set; }
        public int departmentTypeID { get; set; }
        public string departmentTypeName { get; set; }
        public int branch_department_section_id { get; set; }
        public int present { get; set; }
        public int totalStudent { get; set; }

    }
}