using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class ClassBranchGen
    {
        public int branch_department_section_id { get; set; }
        public int classID { get; set; }
        public string className { get; set; }
    }
}