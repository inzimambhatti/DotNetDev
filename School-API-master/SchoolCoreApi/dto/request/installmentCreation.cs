using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class InstallmentCreation
    {
        public int newFeeInstallmentFeeID { get; set; }
        public string feeInstallmentMonth { get; set; }
        public string feeInstallmentName { get; set; }
        public int userID { get; set; }
        public string spType { get; set; }
    }
}