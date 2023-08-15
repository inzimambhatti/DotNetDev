using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class SessionEventSubjectClass
    {
        public int eventID { get; set; }
        public string eventTitle { get; set; }
        public int subjectID { get; set; }
        public string subjectTitle { get; set; }
        public int classID { get; set; }
        public string className { get; set; }
    }
}