using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class SubjectTeacher
    {
        public int teacherID { get; set; }
        public string teacherFirstName { get; set; }
        public string teacherLastName { get; set; }
        public int subjectID { get; set; }
        public int classCount { get; set; }
        public int branch_department_section_id { get; set; }
    }
}