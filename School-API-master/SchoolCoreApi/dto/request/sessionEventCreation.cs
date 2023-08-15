using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class SessionEventCreation 
    {
        public int sessionEventID { get; set; }
        public int schoolSessionID { get; set; }
        public int eventID { get; set; }
        public string sessionEventStartDate { get; set; }
        public string sessionEventStartDateTime { get; set; }
        public string sessionEventEndDate { get; set; }
        public string sessionEventEndDateTime { get; set; }
        public string sessionEventRemarks { get; set; }
        public int userID { get; set; }
        public string spType { get; set; }
    }
}