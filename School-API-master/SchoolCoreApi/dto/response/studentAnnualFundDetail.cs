using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class StudentAnnualFundDetail
    {
        public int studentID { get; set; }
        public string studentName { get; set; }
        public string studentRegistrationCode { get; set; }
        public int classID { get; set; }
        public string className { get; set; }
        public int sectionID { get; set; }
        public string sectionName { get; set; }
        public int status { get; set; }
        public int fundAvail { get; set; }
    }
}