using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class StudentFeeInfo
    {
        public int studentID { get; set; }
        public int month { get; set; }
        public int year { get; set; }
        public int totalAmount { get; set; }
        public string status { get; set; }
    }
}