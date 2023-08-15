using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class HomeWorkLast7Days
    {
        public int markableEventDetailID { get; set; }
        public string markableEventDetailDescription { get; set; }
        public string markableEventDetailFromDate { get; set; }
        public int teacherID { get; set; }
        public string teacherFirstName { get; set; }
        public string dayName { get; set; }
    }
}