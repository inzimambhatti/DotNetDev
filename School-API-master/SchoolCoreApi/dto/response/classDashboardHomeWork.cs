using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class classDashboardHomeWork
    {
        public int teacherID { get; set; }
        public string teacherFirstName { get; set; }
        public int classID { get; set; }
        public string className { get; set; }
        public int branch_department_section_id { get; set; }
        public int subjectID { get; set; }
        public string subjectTitle { get; set; }
        public int sectionID { get; set; }
        public string sectionName { get; set; }
        public int effectiveSubject { get; set; }
    }
}