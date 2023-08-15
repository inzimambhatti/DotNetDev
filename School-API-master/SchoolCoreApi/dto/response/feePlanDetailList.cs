using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class FeePlanDetailList
    {
        public int feesPlanID { get; set; }
        public string feesPlanTitle { get; set; }
        public string feesPlanDate { get; set; }
        public string feesPlanDescription { get; set; }
        public int feesPlanElementAmount { get; set; }
    }
}