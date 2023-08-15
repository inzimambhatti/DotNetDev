using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class AttendanceMonthDetail
    {
        public string calendarDate { get; set; }
        public string attendanceFlag { get; set; }
        public string mName { get; set; }
        public int officialLeaves { get; set; }
    }
}