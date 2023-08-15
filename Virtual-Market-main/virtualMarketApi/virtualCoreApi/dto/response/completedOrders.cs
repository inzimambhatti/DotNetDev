using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace virtualCoreApi.Entities
{
    public class CompletedOrders
    {
        public int TotalOrders { get; set; }
        public int completedOrders { get; set; }
        public int userID { get; set; }
    }
}