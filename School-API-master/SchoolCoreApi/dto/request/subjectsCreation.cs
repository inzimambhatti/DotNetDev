using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class SubjectsCreation
    {
        public int subjectID { get; set; }
        public string subjectTitle { get; set; }
        public string subjectDescription { get; set; }
        public bool isActive { get; set; }
        public int userID { get; set; }
        public string spType { get; set; }
    }
}