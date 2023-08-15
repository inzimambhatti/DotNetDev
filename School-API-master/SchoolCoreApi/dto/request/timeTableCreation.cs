using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class TimeTableCreation
    {
        public int branch_department_section_id { get; set; }
        public int departmentTypeID { get; set; }
        public int classID { get; set; }
        public int subjectID { get; set; }
        public int sectionID { get; set; }
        public int timeSlotDetailID { get; set; }
        public int teacherID { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public int userID { get; set; }
        public string spType { get; set; }
        public string slotFromTime { get; set; }
        public string slotToTime { get; set; }
        public int calendarDay { get; set; }
    }
}