using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class DrilldownAttendance
    {
        public int totalClassStudent { get; set; }
        public int presentClassStudents { get; set; }
        public int absentClassStudents { get; set; }
        public int onLeaveClassStudents { get; set; }
        public int classID { get; set; }
        public string className { get; set; }
        public string sectionJson { get; set; }
    }
}