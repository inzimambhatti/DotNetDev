using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMISModuleApi.dto.request
{
    public class BranchCreation
    {
        public int new_branch_id { get; set; }
        public string branch_name { get; set; }
        public string branch_latitude { get; set; }
        public string branch_longitude { get; set; }
        public string branch_address { get; set; }
        public int branch_type_id { get; set; }
        public int city_id { get; set; }
        public string json { get; set; }
        public int userID { get; set; }
        public string spType { get; set; }
    }
}