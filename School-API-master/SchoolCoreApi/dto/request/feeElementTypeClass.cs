using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class FeeElementTypeCreation
    {
        public int feesElementTypeID { get; set; }
        public string feesElementTypeTitle { get; set; }
        public int userID { get; set; }
        public string spType { get; set; } 
    }
}