using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class MarkableEvent
    {
        public int markableEventDetailID { get; set; }
        public int eventID { get; set; }
        public string eventTitle { get; set; }
        public int userID { get; set; }
        public string userName { get; set; }
        public int eventTypeID { get; set; }
        public string eventTypeTitle { get; set; }
        public int subjectID { get; set; }
        public string subjectTitle { get; set; }
        public int classID { get; set; }
        public string className { get; set; }
        public int sectionID { get; set; }
        public string sectionName { get; set; }
        public int departmentTypeID { get; set; }
        public string departmentTypeName { get; set; }
        public int branch_department_section_id { get; set; }
        public string branch_name { get; set; }
        public string markableEventDetailFromDate { get; set; }
        public string markableEventDetailToDate { get; set; }
        public string markableEventDetailFromTime { get; set; }
        public string markableEventDetailToTime { get; set; }
    }
}