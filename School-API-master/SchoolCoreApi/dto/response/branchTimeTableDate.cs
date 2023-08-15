using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class BranchTimeTableDate
    {
        public int timeSlotID { get; set; }
        public string date { get; set; }
        public string timeSlotFromDate { get; set; }
        public string timeSlotToDate { get; set; }
    }
}