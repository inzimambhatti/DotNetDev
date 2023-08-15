using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class SchoolCalenderDays
    {
        public string calendarDate { get; set; }
        public int calenderDay { get; set; }
        public string calendarDayName { get; set; }
        public string calenderDayStartTime { get; set; }
        public string calenderDayEndTime { get; set; }
        public bool isHalfDay { get; set; }
    }
}