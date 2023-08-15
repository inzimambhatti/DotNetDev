using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class MarkableEventCreation
    {
        public int markableEventDetailID { get; set; }
        public string markableEventDetailFromDate { get; set; }
        public string markableEventDetailToDate { get; set; }
        public int totalMarks { get; set; }
        public string markableEventDetailFromTime { get; set; }
        public string markableEventDetailToTime { get; set; }
        public string markableEventDetailDescription { get; set; }
        public string markableEventDetailEDoc { get; set; }
        public int sessionEventID { get; set; }
        public int classSectionSubjectID { get; set; }
        public int classSectionID { get; set; }
        public int branch_department_section_id { get; set; }
        public int departmentTypeID { get; set; }
        public int classID { get; set; }
        public int sectionID { get; set; }
        public int eventID { get; set; }
        public int eventTypeID { get; set; }
        public int subjectID { get; set; }
        public int userID { get; set; }
        public string spType { get; set; }
    }
}