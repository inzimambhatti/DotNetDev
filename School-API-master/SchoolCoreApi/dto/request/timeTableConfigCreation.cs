using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class TimeTableConfigCreation
    {
        public int newTimeSlotID { get; set; }
        public string timeSlotTitle { get; set; }
        public string timeSlotFromDate { get; set; }
        public string timeSlotToDate { get; set; }
        public int schoolSessionID { get; set; }
        public int branch_department_section_id { get; set; }
        public int departmentTypeID { get; set; }
        public int NoOFPeriods { get; set; }
        public int breakDuration { get; set; }
        public int breakAfterClass { get; set; }
        public int userID { get; set; }
        public string json { get; set; }
        public string dayJson { get; set; }
        public string spType { get; set; }
    }
}