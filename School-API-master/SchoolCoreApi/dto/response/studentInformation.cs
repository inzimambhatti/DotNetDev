using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class StudentInformation
    {
        public int studentID { get; set; }
        public string studentName { get; set; }
        public string studentPlaceOfBirth { get; set; }
        public string studentGender { get; set; }
        public string studentReligon { get; set; }
        public string studentEdoc { get; set; }
    }
}