using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMISModuleApi.Entities
{
    public class DepartmentContact
    {
        public int contact_info_id { get; set; }
        public string contact_info_title { get; set; }
        public int contact_type_id { get; set; }
        public string contact_type_title { get; set; }
    }
}