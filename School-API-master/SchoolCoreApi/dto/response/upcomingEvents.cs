using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class UpcomingEvents
    {
        public int sessionEventID { get; set; }
        public string sessionEventStartDateTime { get; set; }
        public string sessionEventEndDateTime { get; set; }
        public string sessionEventStartDate { get; set; }
        public int eventID { get; set; }
        public string eventTitle { get; set; }
        public string month { get; set; }
    }
}