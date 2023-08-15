using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class Event
    {
        public int eventID { get; set; }
        public int eventTypeID { get; set; }
        public string eventTypeTitle { get; set; }
        public int eventParentID { get; set; }
        public string eventParentTitle { get; set; }
        public string eventTitle { get; set; }
        public string eventDescription { get; set; }
        public int eventNatureID { get; set; }
        public string eventNatureTitle { get; set; }
    }
}
