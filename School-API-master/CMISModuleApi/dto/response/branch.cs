using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMISModuleApi.Entities
{
    public class Branch
    {
        public int branch_id { get; set; }
        public int branch_type_id { get; set; }
        public int countory_id { get; set; }
        public int city_id { get; set; }
        public string branch_name { get; set; }
        public string contact_info_title { get; set; }
        public string city_name { get; set; }
        public string address { get; set; }
    }
}