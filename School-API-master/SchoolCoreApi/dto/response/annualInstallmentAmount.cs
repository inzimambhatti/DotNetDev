using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class AnnualInstallmentAmount
    {
        public int annualFundID { get; set; }
        public int noOfInstallments { get; set; }
        public float remainingAmount { get; set; }
        public float generatedRemainingAmount { get; set; }
    }
}