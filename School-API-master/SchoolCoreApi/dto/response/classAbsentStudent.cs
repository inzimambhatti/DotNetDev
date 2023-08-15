using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class ClassAbsentStudent
    {
        public int classID { get; set; }
        public string className { get; set; }
        public int totalAbsentStudent { get; set; }
    }
}