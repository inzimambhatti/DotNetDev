using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class SessionEvent  
    {
        public int eventID { get; set; }
        public string eventTitle { get; set; }
        public int schoolSessionID { get; set; }
        public string sessionEventStartDate { get; set; }
        public string sessionDateEndDate { get; set; }
    }
}