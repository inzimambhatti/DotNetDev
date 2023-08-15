using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMISModuleApi.dto.request
{
    public class DepartmentContactCreation
    {
        public int branch_id { get; set; }
        public int company_id { get; set; }
        public int department_section_id { get; set; }
        public string json { get; set; }
    }
}