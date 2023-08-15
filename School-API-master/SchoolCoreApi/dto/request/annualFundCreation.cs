using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class AnnualFundCreation
    {
        public int newAnnualFundID { get; set; }
        public int noOfInstallments { get; set; }
        public string annualFundTitle { get; set; }
        // public float amount { get; set; }
        public int feesElementID { get; set; }
        public int branch_department_section_id { get; set; }
        public int schoolSessionID { get; set; }
        public string json { get; set; }
        public int userID { get; set; }
        public string spType { get; set; }
    }
}