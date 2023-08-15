using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class MarkableEventResultCreation
    {
        public int markableEventResultID { get; set; }
        public int subjectID { get; set; }
        public int teacherID { get; set; }
        public int sectionID { get; set; }
        public int classID { get; set; }
        public int markableEventDetailID { get; set; }
        public int userID { get; set; }
        public string json { get; set; }
        public string spType { get; set; }
        public int departmentTypeID { get; set; }
    }
}