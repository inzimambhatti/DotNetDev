using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class TeacherDetail
    {
        public int teacherID { get; set; }
        public string teacherFirstName { get; set; }
        public string teacherLastName { get; set; }
        public string teacherCNIC { get; set; }
        public string teacherGender { get; set; }
        public string teacherReligion { get; set; }
        public string teacherSubject { get; set; }
        public string teacherEmail { get; set; }
        public string teacherDOB { get; set; }
        public string teacherAddress { get; set; }
        public string teacherPhone { get; set; }
        public string teacherEmergencyNo { get; set; }
        public string teacherQualification { get; set; }
        public int branch_department_section_id { get; set; }
        public string teacherEdoc { get; set; }
        public int nationalityID { get; set; }
        public string nationalityName { get; set; }
        public int sectionID { get; set; }
        public string sectionName { get; set; }
        public int classID { get; set; }
        public string className { get; set; }
        public int departmentTypeID { get; set; }
        public string departmentTypeName { get; set; }
        public int branch_id { get; set; }
        public string branch_name { get; set; }
        public int subjectID { get; set; }
        public string subjectTitle { get; set; }
        public bool isClassIncharge { get; set; }
        public bool isAttendance { get; set; }
    }
}