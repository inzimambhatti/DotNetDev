using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class EventCalendar
    {
        public int sessionEventID { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public int eventID { get; set; }
        public string title { get; set; }
        public int schoolSessionID { get; set; }
        public int isMarkable { get; set; }
    }
}