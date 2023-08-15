using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UMISModuleAPI.Entities
{
    public class UserBranchCreation
    {
        public int newUserBranchID { get; set; }
        public int userID { get; set; }
        public string json { get; set; }
        public string spType { get; set; }
    }
}