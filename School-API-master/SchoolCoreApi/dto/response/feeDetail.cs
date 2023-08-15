using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class FeeDetail
    {
        public int feesPlanID { get; set; }
        public int feesElementID { get; set; }
        public string feesElementTitle { get; set; }
        public int feesElementTypeID { get; set; }
        public string feesPlanElementAmount { get; set; }
        public int noOfInstallements { get; set; }
    }
}