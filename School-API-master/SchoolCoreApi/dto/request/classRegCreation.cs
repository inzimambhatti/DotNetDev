using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class ClassRegCreation
    {
        public int newClassDepartmentID { get; set; }
        public int classID { get; set; }
        public int branch_department_section_id { get; set; }
        public int departmentTypeID { get; set; }
        public string json { get; set; }
        public int userID { get; set; }
        public string spType { get; set; }
    }
}