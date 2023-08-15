using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class StudentSubjectDetail
    {
        public int studentClassSectionSubjectID { get; set; }
        public int studentID { get; set; }
        public string studentName { get; set; }
        public int sectionID { get; set; }
        public string sectionName { get; set; }
        public string studentRegistrationCode { get; set; }
        public string subjectTitle { get; set; }
        public int isClassActive { get; set; }
        public int isCompleted { get; set; }
    }
}