using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class StudentFeeChallan 
    {
        public int studentMonthlyFeesID { get; set; }
        public string feeChallanDate { get; set; }
        public int feeChallanMonth { get; set; }
        public string feeChallanYear { get; set; }
        public int studentID { get; set; }
        public string studentName { get; set; }
        public int studentRegistrationCode { get; set; }
        public string className { get; set; }
        public string fatherName { get; set; }
        public string sessionYear { get; set; }
        public string feesDueDate { get; set; }
        public string feesElementTitle { get; set; }
        public int studentMonthlyFeesAmount { get; set; }
        public int siblingValue { get; set; }
        public int volunteerAmount { get; set; }
        public int feesElementID { get; set; }
        // public string fromMonth { get; set; }
        // public string toMonth { get; set; }
        public string studentMonthlyFeesDate { get; set; }
        // public string instrumentDate { get; set; }
        // public int collectionAmount { get; set; }
        public string historyJson { get; set; }
        public string elementJson { get; set; }
        public string arrearJson { get; set; }
    }
}