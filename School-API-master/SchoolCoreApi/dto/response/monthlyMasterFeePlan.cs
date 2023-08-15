using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class MonthlyMasterFeePlan
    {
        public int feeMasterPlanID { get; set; }
        public string feeMasterPlanTitle { get; set; }
        public float feeElementsAmount { get; set; }

    }
}