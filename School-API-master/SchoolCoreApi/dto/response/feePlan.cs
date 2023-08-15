using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class FeePlan
    {
        public int feesPlanID { get; set; }
        public string fessPlanTitle { get; set; }
        public string feesPlanDate { get; set; }
        public string feesPlanDescription { get; set; }
    }
}