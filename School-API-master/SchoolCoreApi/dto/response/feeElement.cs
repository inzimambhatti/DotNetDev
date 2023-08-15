using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class FeeElement
    {
        public int feesElementTypeID { get; set; }
        public string feesElementTypeTitle { get; set; } 
        public int feesElementID { get; set; }
        public string feesElementTitle {get;set;}
    }
}