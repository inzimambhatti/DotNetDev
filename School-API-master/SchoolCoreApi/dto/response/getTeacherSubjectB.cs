using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class getTeacherSubjectB
    {
        public int branch_department_section_id { get; set; }
        public int subjectID { get; set; }
        public string subjectTitle { get; set; }
        // public int teacherID { get; set; }
    }
}