using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMISModuleApi.dto.request
{
    public class BranchDetail
    {
        public int branch_id { get; set; }
        public string branch_name { get; set; }
        public string branch_type_title { get; set; }
        public string address { get; set; }
        public int city_id { get; set; }
        public string city_name { get; set; }
        public int countory_id { get; set; }
        public string country_name { get; set; }
        public int contact_info_id { get; set; }
        public int contact_type_id { get; set; }
        public string contact_info_title { get; set; }
        public string contact_type_title { get; set; }
    }
}