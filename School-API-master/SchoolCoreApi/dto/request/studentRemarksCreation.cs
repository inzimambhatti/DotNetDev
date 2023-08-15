using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class StudentRemarksCreation
    {
        public int studentID { get; set; }
        public int classID { get; set; }
        public string remarks { get; set; }
        // public int userID { get; set; }
        public string spType { get; set; }
    }
}