using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class FeeConfigurationCreation
    {
        public int newfeesPlanID { get; set; }
        public string feesPlanTitle { get; set; }
        public string feesPlanDescription { get; set; }
        public string feesPlanDate { get; set; }
        public int isActive { get; set; }
        public int userID { get; set; }
        public string json { get; set; }
        public string spType { get; set; }
    }
}