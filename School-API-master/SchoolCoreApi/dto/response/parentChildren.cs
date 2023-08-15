using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class ParentChildren
    {
        public int studentID { get; set; }
        public string studentName { get; set; }
        public string studentRegistrationCode { get; set; }
        public int parentID { get; set; }
        public string parentName { get; set; }
        public string parentPassportOrCNIC { get; set; }
        public int classID { get; set; }
        public string className { get; set; }
    }
}