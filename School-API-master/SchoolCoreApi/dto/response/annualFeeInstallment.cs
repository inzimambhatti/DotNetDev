using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class AnnualFeeInstallment
    {
        public int feeMasterPlanID { get; set; }
        public string feeMasterPlanTitle { get; set; }
        public float feeElementsAmount { get; set; }
        public int rowNo { get; set; }
    }
}