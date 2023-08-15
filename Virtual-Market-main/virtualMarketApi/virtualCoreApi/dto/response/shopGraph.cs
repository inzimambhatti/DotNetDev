using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace virtualCoreApi.Entities
{
    public class shopGraph
    {
        public int ordersCount { get; set; }
        public int orderAmount { get; set; }
        public int userID { get; set; }
        public int shopID { get; set; }
    }
}