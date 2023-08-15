using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UMISModuleApi.Entities
{
    public class MobileUserCreation
    {
        public int newUserID { get; set; }
        public string firstName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string userCNIC { get; set; }
        public int userID { get; set; }
        public string spType { get; set; }
    }
}