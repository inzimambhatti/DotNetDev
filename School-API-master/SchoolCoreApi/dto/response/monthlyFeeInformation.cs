using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class MonthlyFeeInformation
    {
        public string feeMasterPlanTitle { get; set; }
        public float feeGenerateAmount { get; set; }
        public int feeGenerateID { get; set; }
        public int studentID { get; set; }
        
    }
}