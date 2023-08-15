using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class SessionConfig  
    {
        public int schoolSessionID { get; set; }
        public string schoolSessionTitle { get; set; }
        public string schoolSessionStartDate { get; set; }
        public string schoolSessionEndDate { get; set; }
        public string noOfDays { get; set; }
        public string workingDays { get; set; }
    }
}