using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class FeeElementCreation
    {
        public int feesElementID { get; set; }
        public string feesElementTitle { get; set; }
        public int feesElementTypeID { get; set; }
        public int isActive {get;set;}
        public int userID {get;set;}
        public string spType { get; set; }
    }
}