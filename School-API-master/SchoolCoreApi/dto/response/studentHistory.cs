using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class StudentHistory
    {
        public int studentID { get; set; }
        public string studentName { get; set; } 
        public string studentEdoc { get; set; } 
        public int studentRegistrationCode { get; set; } 
        public string parentName { get; set; } 
        public string parentPassportOrCNIC { get; set; } 
        public string className { get; set; } 
        public int studentMonthlyFeesAmount { get; set; } 
        public string studentMonthlyFeesDate { get; set; } 
        public int closingFlag { get; set; }
        public int amount { get; set; } 

    }
}