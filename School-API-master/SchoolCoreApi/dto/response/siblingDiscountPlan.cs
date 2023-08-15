using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class SiblingDiscountPlan
    {
        public int feeMasterPlanID { get; set; }
        public string feeMasterPlanTitle { get; set; }
        public float feeDiscountAmount { get; set; }
    }
}