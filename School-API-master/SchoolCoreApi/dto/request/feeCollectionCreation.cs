using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class FeeCollectionCreation
    {
        public int studentMonthlyFeesID { get; set; }
        public int collectionAmount { get; set; }
        public string instrumentDate { get; set; }
        public int finAmount { get; set; }
        public int userID { get; set;  }
        public string spType { get; set; }
        public int month { get; set; }
        public int year { get; set; }
        public int studentID { get; set; }
    }
}