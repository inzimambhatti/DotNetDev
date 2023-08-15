using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UMISModuleApi.dto.response
{
    public class userDetail
    {
        public int userID { get; set; }     
        public int roleId { get; set; }     
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string roleTitle { get; set; }
        public bool active { get; set; }
        public string password { get; set; }
        public string userCNIC { get; set; }
        public int teacherID { get; set; }
        public string userType { get; set; }
    }
}