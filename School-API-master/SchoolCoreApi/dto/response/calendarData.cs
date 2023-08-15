using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class CalendarData
    {
        public int schoolSessionID { get; set; }
        public int calendarDay { get; set; }
        public string calendarDayName { get; set; }
        public string calenderDayStartTime { get; set; }
        public string calenderDayEndTime { get; set; }
        public int isWeakend { get; set; }
        public int isHalfDay { get; set; }
    }
}