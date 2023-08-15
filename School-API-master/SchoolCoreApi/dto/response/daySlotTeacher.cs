using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class DaySlotTeacher
    {
        public int timeSlotDetailID { get; set; }
        public int teacherID { get; set; }
        public string teacherName { get; set; }
    }
}