using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class TeacherBranchSubjectClassSection
    {
        public int branch_department_section_id { get; set; }
        public int subjectID { get; set; }
        public int classID { get; set; }
        public int sectionID { get; set; }
        public string sectionName { get; set; }
        public int teacherID { get; set; }
        public int departmentTypeID { get; set; }
    }
}