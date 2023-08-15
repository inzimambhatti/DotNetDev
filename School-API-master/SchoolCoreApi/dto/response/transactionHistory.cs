using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class TransactionHistory
    {
        public int feesTransactionID {get; set;}
        public string feesTransactionDate { get; set; }
        public string feesTransactionFromDate { get; set; }
        public string feesTransactionToDate { get; set; }
        public int feesTransactionValue {get; set;}
        public string feesElementTitle { get; set; }
        public int studentID { get; set; }
        public int feesElementID { get; set; }
        public int sectionID { get; set; }
        public int classID { get; set; }
        public int branch_department_section_id { get; set; }
    }
}