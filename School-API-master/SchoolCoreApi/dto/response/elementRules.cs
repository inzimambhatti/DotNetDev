using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class ElementRules
    {
        public int feesElementID { get; set; }
        public string feesElementTitle { get; set; } 
        public int feesElementTypeID { get; set; }
        public string feesElementTypeTitle { get; set; }
        public int feesElementRulelID { get; set; } 
        public string feesElementRuleDate { get; set; }
        public int installements { get; set; }
        public int siblings { get; set; } 
        public string feeselementRuleType { get; set; }
        public int feesElementRuleValue {get;set;}
        public string feesElementRuleNature { get; set; }
        public int feesElementRuleOverride {get;set;}
    }
}