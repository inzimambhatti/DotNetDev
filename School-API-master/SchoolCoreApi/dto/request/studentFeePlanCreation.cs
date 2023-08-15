using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    
    public class StudentFeePlanCreation 
    {
        public int studentFeesPlanDetailID { get; set; }
        public int studentFeesPlanID { get; set; }
        public string studentFeesPlanDate { get; set; }
        public int feesPlanID { get; set; }
        public int sibling { get; set; }
        public int siblingAmount { get; set; }
        public int employeeDiscount { get; set; }
        public string registrationDate { get; set; }
        public int studentID { get; set; }
        public int userID { get; set; }
        public string json { get; set; }
        public string spType { get; set; }
    }
}