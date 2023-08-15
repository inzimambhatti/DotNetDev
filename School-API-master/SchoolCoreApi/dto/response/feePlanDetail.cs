using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class FeePlanDetail
    {
        public int feePlanDetailID { get; set; }
        public float feePlanDetailAmount { get; set; }
        public string feePlanDetailDescription { get; set; }
        public int feeElementID { get; set; }
        public int feePlanID { get; set; }
    }
}