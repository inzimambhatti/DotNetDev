using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class CreateEventCreation
    {
        public int userID { get; set; }
        public string branchJson { get; set; }
        public int subjectID { get; set; }
        public string departmentTypeJson { get; set; }
        public string classJson { get; set; }
        public string json { get; set; }
        public int eventTypeID { get; set; }
        public int eventID { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public string fromTime { get; set; }
        public string toTime { get; set; }
        public int sessionID { get; set; }
        public int totalMarks { get; set; }
        public string description { get; set; }
        public bool isMarkable { get; set; }
    }
}