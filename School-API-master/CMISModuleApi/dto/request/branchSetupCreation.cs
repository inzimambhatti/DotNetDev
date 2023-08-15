using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMISModuleApi.Entities
{
    public class BranchSetupCreation
    {
        public int new_company_branch_id { get; set; }
        public int company_id { get; set; }
       // public int branch_id { get; set; }
        public string json { get; set; }
        public int userID { get; set; }
        public string spType { get; set; }
    }
}