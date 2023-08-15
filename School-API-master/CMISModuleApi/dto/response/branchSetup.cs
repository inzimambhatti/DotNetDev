using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMISModuleApi.Entities
{
    public class BranchSetup
    {
        public int branch_id { get; set; }
        public string branch_name { get; set; }
        public int company_id { get; set; }
        public string company_name { get; set; }
        public int company_branch_id { get; set; }
        public int department_section_id { get; set; }
        public string dept_sec_type { get; set; }
    }
}