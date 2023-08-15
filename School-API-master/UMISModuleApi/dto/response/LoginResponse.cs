using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UMISModuleApi.Entities
{
    public class LoginResponse
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
        public string token { get; set; }

        public LoginResponse(List<LoginUser> user, string userToken)
        {
            userLoginId = user[0].userLoginId;
            FullName = user[0].FullName;
            loginName = user[0].loginName;
            Password = user[0].Password;
            userCNIC = user[0].userCNIC;
            teacherID = user[0].teacherID;
            isTeacher = user[0].isTeacher;
            isParent = user[0].isParent;
            isEmployee = user[0].isEmployee;
            token = userToken;
        }
    }
}