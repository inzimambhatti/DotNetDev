using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class SubjectLoadCreation
    {
        public int branch_department_section_id { get; set; }
        public int departmentTypeID { get; set; }
        public int schoolSessionID { get; set; }
        public int userID { get; set; }
        public string json { get; set; }
        public string spType { get; set; }
    }
}