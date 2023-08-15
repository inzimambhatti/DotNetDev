using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class TeacherTimeTable
    {
        public int calenderDay { get; set; }
        public string calendarDayName { get; set; }
        public string timeSlotFromTime { get; set; }
        public string timeSlotToTime { get; set; }
        public int calendarDaySlotno { get; set; }
        public string subJson { get; set; }
        public int timeSlotTypeID { get; set; }
    }
}