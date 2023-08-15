using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UMISModuleApi.dto.response
{
    public class UserBranches
    {
        public int branchID { get; set; }
        public string branch_name { get; set; }
        public int branch_department_section_id { get; set; }
    }
}