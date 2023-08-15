using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class StudentFunds
    {
        public int annualFundID { get; set; }
        public string annualFundTitle { get; set; }
        public string studentMonthlyFeesDate { get; set; }
        public int month { get; set; }
        public string monthName { get; set; }
    }
}