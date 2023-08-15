using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class EventCreation
    {
        public int eventID { get; set; }
        public string eventTitle { get; set; } 
        public int eventParentID { get; set; } 
        public string eventDescription { get; set; }
        public int eventTypeID { get; set; }
        public bool importantEvent { get; set; }
        public int userID {get;set;}
        public string spType {get;set;}
        public int eventNatureID { get; set; }
    }
}