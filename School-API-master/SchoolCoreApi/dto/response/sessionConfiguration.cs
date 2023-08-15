using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class SessionConfiguration
    {
        public string sessionYear { get; set; }
        public string sessionStartDate { get; set; }
        public string sessionEndDate { get; set; }
        public int totalDays { get; set; }
    }
}