using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class TimeTableConfigDetail
    {
        public int schoolSessionID { get; set; }
        public string schoolSessionTitle { get; set; }
        public int timeSlotID { get; set; }
        public string timeSlotTitle { get; set; }
        public int branch_department_section_id { get; set; }
        public int departmentTypeID { get; set; }
        public string classes { get; set; }
        public string timeSlotFromDate { get; set; }
        public string timeSlotToDate { get; set; }
        public int noOfPeriods { get; set; }
        public int breakDuration { get; set; }
        public int breakAfterClass { get; set; }
        public string sessionCalenderDays { get; set; }
    }
}