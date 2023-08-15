using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class ElementRulesCreation
    {
        public int feesElementRulelID { get; set; }
        public int feesElementID { get; set; } 
        public string feesElementRuleDate { get; set; }
        public int installements { get; set; }
        public int siblings { get; set; } 
        public string feeselementRuleType { get; set; }
        public int feesElementRuleValue { get; set; }
        public string feesElementRuleNature { get; set; } 
        public int feesElementRuleOverride { get; set; }
        public int isActive {get;set;}
         public int userID {get;set;}
        public string spType {get;set;}
    }
}