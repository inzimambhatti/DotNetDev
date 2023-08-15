using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMISModuleApi.dto.request
{
    public class departmentCreation
    {
        public int newDeptSecID { get; set; }
        public string dept_sec_name { get; set; }
        public string dept_sec_desc { get; set; }
        public int parent_id { get; set; }
        public string dept_sec_type { get; set; }
        public int userID { get; set; }
        public string spType { get; set; }
    }
}