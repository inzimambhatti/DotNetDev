using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class BranchSubjectLoad
    {
        public int subjectID { get; set; }
        public string subjectTitle { get; set; }
        public int loadSum { get; set; }
    }
}