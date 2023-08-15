using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class ClassSubjectExamMarks
    {
        public int classID { get; set; }
        public string className { get; set; }
        public int branch_department_section_id { get; set; }
        public int subjectID { get; set; }
        public string subjectTitle { get; set; }
        public int eventID { get; set; }
        public string eventTitle { get; set; }
        public int teacherID { get; set; }
        public string teacherName { get; set; }
        public string status { get; set; }
    }
}