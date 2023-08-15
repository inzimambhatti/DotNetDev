using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class UncertainEvent
    {
        public int schoolSessionID { get; set; }
        public string uncertainEventDescription { get; set; }
        public string schoolSessionTitle { get; set; }
        public string calendarDateView { get; set; }
        public int noOfDays { get; set; }
    }
}