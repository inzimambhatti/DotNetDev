using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class FeeElementsCollection
    {
        public int feesElementID { get; set; }
        public string feesElementTitle { get; set; }
        public int totalAmount { get; set; }
        public int paidFee { get; set; }
    }
}