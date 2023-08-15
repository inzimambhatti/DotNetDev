using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class TimeTableClassSectionSubject
    {
        public int calenderDay { get; set; }
        public string calendarDayName { get; set; }
        public string sectionJson { get; set; }
    }
}