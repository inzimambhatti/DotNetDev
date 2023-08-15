using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class CurDateAttendance
    {
        public int studentID { get; set; }
        public int attendanceID { get; set; }
        public string attendanceDate { get; set; }
        public string attendanceFlag { get; set; }
    }
}