using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class SessionDate
    {
        public int schoolSessionID { get; set; }
        public string schoolSessionTitle { get; set; }
        public string schoolSessionStartDate { get; set; }
        public string schoolSessionEndDate { get; set; }
    }
}