using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMISModuleApi.Entities
{
    public class department
    {
        public int department_section_id { get; set; }
        public string dept_sec_name { get; set; }
        public string dept_sec_desc { get; set; }
        public int parent_id { get; set; }
    }
}