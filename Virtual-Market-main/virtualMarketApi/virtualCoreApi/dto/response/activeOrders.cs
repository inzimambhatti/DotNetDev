using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace virtualCoreApi.Entities
{
    public class ActiveOrders
    {
        public int orderCount { get; set; }
        public float balance { get; set; }
        public int userID { get; set; }
    }
}