using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class BranchFee
    {
        public int feeBranchID { get; set; }
        public int schoolSessionID { get; set; }
        public string schoolSessionTitle { get; set; }
        public int branch_department_section_id { get; set; }
        public string branch_Name { get; set; }
        public string amount { get; set; }
    }
}