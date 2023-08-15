using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UMISModuleApi.dto.request
{
    public class changePassword
    {
        public int userID { get; set; }
        public string newPassword { get; set; }
    }
}