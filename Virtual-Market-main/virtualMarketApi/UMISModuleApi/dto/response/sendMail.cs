using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UMISModuleApi.Entities
{
    public class SendMail
    {
        public string userName { get; set; }
        public string userEmail { get; set; }
        public string ourEmail { get; set; }
        public string subject { get; set; }
        public string message { get; set; }
    }
}