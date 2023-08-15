using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class StudentFeePlan
    {
        public int studentFeesPlanDetailID { get; set; }
        public string studentFeesPlanID { get; set; }
        public int feesElementID { get; set; }
        public string feesElementTypeTitle { get; set; }
        public string studentFeesFromDate { get; set; }
        public string studentFeesToDate { get; set; }
        public int installments { get; set; }
        public int studenFeesPlanID { get; set; }
        public int feesPlanID { get; set; }
        public int studentID { get; set; }
        public string studentFeesPlanDate { get; set; }
        public int feesPlanElementAmount { get; set; }
    }
}