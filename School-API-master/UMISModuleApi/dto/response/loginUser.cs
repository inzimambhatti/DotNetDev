using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UMISModuleApi.Entities
{
    public class LoginUser
    {
        public int userLoginId { get; set; }
        public string loginName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string userCNIC { get; set; }
        public int teacherID { get; set; }
        public bool isTeacher { get; set; }
        public bool isParent { get; set; }
        public bool isEmployee { get; set; }
    }
}