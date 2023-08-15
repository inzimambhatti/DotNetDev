using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class MonthlyTransactionDetailCreation  
    {
        public int feesTransactionID { get; set; }
        public int feesTransactionValue { get; set; }
        public string feesTransactionDate { get; set; }
        public string feesTransactionNature { get; set; }
        public string feesTransactionFromDate { get; set; }
        public string feesTransactionToDate { get; set; }
        public int elementID { get; set; }
        public int userID { get; set; }
        public string spType { get; set; }
    }
}