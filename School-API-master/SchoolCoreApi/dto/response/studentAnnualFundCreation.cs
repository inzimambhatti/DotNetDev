using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class StudentAnnualFundCreation
    {
        public int annualFundID { get; set; }
        public int noOfInstallments { get; set; }
        public int studentID { get; set; }
        public string effectDate { get; set; }
        public string json { get; set; }
        public int userID { get; set; }
        public string spType { get; set; }
        public float amount { get; set; }
    }
}