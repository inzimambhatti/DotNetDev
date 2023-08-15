using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class FrequentAbsent
    {
        public int studentID { get; set; }
        public int absentCount { get; set; }
        public string studentName { get; set; }
        public string studentEdoc { get; set; }
        public int classID { get; set; }
        public string className { get; set; }
    }
}