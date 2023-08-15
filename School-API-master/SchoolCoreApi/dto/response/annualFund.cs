using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class AnnualFund
    {
        public int annualFundID { get; set; }
        public string annualFundTitle { get; set; }
        public int noOfInstallments { get; set; }
        public int feesElementID { get; set; }
        public string feesElementTitle { get; set; }
        public int schoolSessionID { get; set; }
        public string schoolSessionTitle { get; set; }
        public int branch_department_section_id { get; set; }
        public string branch_name { get; set; }
        public int month { get; set; }
    }
}