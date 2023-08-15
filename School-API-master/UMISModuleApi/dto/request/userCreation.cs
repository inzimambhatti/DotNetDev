using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UMISModuleApi.dto.request
{
    public class userCreation
    {
        public int userID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int isPinCode { get; set; }
        public int pincode { get; set; }
        public int active { get; set; }
        public string spType { get; set; }
        public int branch_id { get; set; }
        public int roleID { get; set; }
        public int teacherID { get; set; }
        public string userType { get; set; }
        public string userCNIC { get; set; }
    }
}