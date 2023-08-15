using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class StudentSubjectCreation
    {
        public int studentClassSectionSubjectID { get; set; }
        public int branch_department_section_id { get; set; }
        public int studentClassID { get; set; }
        public int departmentTypeID { get; set; }
        public int sectionID { get; set; }
        public int subjectID { get; set; }
        public int classID { get; set; }
        public int userID { get; set; }
        public string json { get; set; }
        public string spType { get; set; }
    }
}