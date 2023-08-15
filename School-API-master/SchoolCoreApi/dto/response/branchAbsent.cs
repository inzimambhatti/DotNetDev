using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class BranchAbsent
    {
        public int branch_department_section_id { get; set; }
        public int totalAbsentStudent { get; set; }
    }
}