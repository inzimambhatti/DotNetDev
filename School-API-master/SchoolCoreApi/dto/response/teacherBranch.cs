using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class TeacherBranch
    {
        public int branch_department_section_id { get; set; }
        public string branchName { get; set; }
        public int teacherID { get; set; }
    }
}