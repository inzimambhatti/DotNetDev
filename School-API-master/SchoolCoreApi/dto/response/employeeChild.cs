using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class EmployeeChild
    {
        public int feesElementID { get; set; }
        public int feesElementRuleValue { get; set; }
        public string feeselementRuleType { get; set; }
        public int feesElementTypeID { get; set; }
    }
}