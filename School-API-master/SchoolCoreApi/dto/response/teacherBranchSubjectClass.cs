using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class TeacherBranchSubjectClass
    {
        public int branch_department_section_id { get; set; }
        public int subjectID { get; set; }
        public int classID { get; set; }
        public string className { get; set; }
        public int departmentTypeID { get; set; }
        // public int teacherID { get; set; }
    }
}