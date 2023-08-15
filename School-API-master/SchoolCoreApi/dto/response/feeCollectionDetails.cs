using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class FeeCollectionDetails
    {
        public string studentName { get; set; }
        public string studentRegistrationCode { get; set; }
        public int studentMonthlyFeesAmount { get; set; }
        public string studentMonthlyFeesDate { get; set; }
        public int sectionID { get; set; }
        public int classID { get; set; }
        public int branch_department_section_id { get; set; }
         public int studentID { get; set; }
    }
}