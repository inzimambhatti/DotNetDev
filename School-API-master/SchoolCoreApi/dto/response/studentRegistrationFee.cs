using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class StudentRegistrationFee
    {
        public int feeMasterPlanID { get; set; }
        public string feeMasterPlanTitle { get; set; }
        public int studentID { get; set; }
        public float feeGenerateAmount { get; set; }
        public string feeGenerateDate { get; set; }
    }
}