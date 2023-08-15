using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UMISModuleApi.dto.request
{
    public class roleCreation
    {
        public int new_role_id { get; set; }
        // public int newRoleDetailID { get; set; }
        public string roleTitle { get; set; }
        public string roleDescription { get; set; }
        // public int active { get; set; }
        public int userID { get; set; }
        public string json { get; set; }
        public string spType { get; set; }
    }
}