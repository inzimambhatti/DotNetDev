using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class AttendanceOfMonth
    {
        public int daysPresent { get; set; }
        public string mName { get; set; }
        public int totalWorkingDays { get; set; }
        public int officialLeaves { get; set; }
        public int month { get; set; }
    }
}