using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class EventType 
    {
        public int eventTypeID { get; set; }
        public string eventTypeTitle { get; set; }
        public bool isMarkable { get; set; }
        public string eventTypeDescription { get; set; }
    }
}