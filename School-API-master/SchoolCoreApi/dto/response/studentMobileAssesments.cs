using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class StudentMobileAssesments
    {
        public int sessionEventID { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public int eventID { get; set; }
        public string title { get; set; }
        public int schoolSessionID { get; set; }
        public int isMarkable { get; set; }
        public int sectionID { get; set; }
        public string sectionName { get; set; }
        public int classID { get; set; }
        public string className { get; set; }
        public int subjectID { get; set; }
        public string subjectTitle { get; set; }
        public int departmentTypeID { get; set; }
        public string status { get; set; }
        public int studentID { get; set; }
    }
}