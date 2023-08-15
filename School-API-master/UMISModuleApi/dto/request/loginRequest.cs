using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UMISModuleApi.Entities
{
    public class LoginRequest
    {
        public string email { get; set; }
        public string hashpassword { get; set; }
    }
}