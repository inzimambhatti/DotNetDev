using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class FixturesTimeTableCreation
    {
        public int branch_department_section_id { get; set; }
        public int departmentTypeID { get; set; }
        public int classID { get; set; }
        public int subjectID { get; set; }
        public int sectionID { get; set; }
        public int timeSlotDetailID { get; set; }
        public int oldTeacherID { get; set; }
        public int newTeacherID { get; set; }
        public string timeTableDate { get; set; }
        public int userID { get; set; }
        public string spType { get; set; }
        public int calenderDay { get; set; }
        public int calendarDaySlotNo { get; set; }
        public int pinCode { get; set; }
    }
}