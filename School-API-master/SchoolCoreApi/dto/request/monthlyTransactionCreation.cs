using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class MonthlyTransactionCreation 
    {
        public int feesTransactionID { get; set; }
        public int userID { get; set; }
        public string json { get; set; }
        public string spType { get; set; }
        public int branch_department_section_id { get; set; }
        public int classID { get; set; }
        public int sectionID { get; set; }
    }
}