using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class BranchFeeCreation
    {
        public int feeBranchID { get; set; }
        public int schoolSessionID  { get; set; }
        public int amount { get; set; }
        public int branch_department_section_id { get; set; }
        public int userID { get; set; }
        public string spType { get; set; }
    }
}