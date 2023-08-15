using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class OneWeekAttendance
    {
        public int presentStudents { get; set; }
        public int absentStudents { get; set; }
        public int onLeaveStudents { get; set; }
        public string dayName { get; set; }
    }
}