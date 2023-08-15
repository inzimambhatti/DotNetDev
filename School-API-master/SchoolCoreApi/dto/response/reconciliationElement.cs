using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class ReconciliationElement
    {
        public int feesElementID { get; set; }
        public string feesElementTitle { get; set; }
        public int amount { get; set; }
        public string nature { get; set; }
    }
}