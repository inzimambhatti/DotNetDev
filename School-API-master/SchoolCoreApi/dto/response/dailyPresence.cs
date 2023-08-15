using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class DailyPresence
    {
        public int totalPresentBoys { get; set; }
        public int totalAbsentBoys { get; set; }
        public int totalLeaveBoys { get; set; }
        public int totalPresentGirls { get; set; }
        public int totalAbsentGirls { get; set; }
        public int totalLeaveGirls { get; set; }
    }
}