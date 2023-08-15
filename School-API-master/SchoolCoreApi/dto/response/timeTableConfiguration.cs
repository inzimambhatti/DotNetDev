using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class TimeTableConfiguration
    {
        public int timeSlotID { get; set; }
        public string timeSlotTitle { get; set; }
        public int schoolSessionID { get; set; }
        public string schoolSessionTitle { get; set; }
        public string schoolSessionYear { get; set; }
    }
}