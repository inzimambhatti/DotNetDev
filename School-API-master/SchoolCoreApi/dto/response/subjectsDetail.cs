using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class SubjectsDetail
    {
        public int subjectID { get; set; }
        public string subjectTitle { get; set; }
        public string subjectDescription { get; set; }
        public int isActive { get; set; }
    }
}