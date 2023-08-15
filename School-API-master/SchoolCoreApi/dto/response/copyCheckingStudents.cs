using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class CopyCheckingStudents
    {
        public int studentID { get; set; }
        public string studentName { get; set; }
        public string studentRegistrationCode { get; set; }
        public string remarks { get; set; }
    }
}