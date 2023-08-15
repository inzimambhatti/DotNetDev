using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace virtualCoreApi.Entities
{
    public class OrderCreation
    {
        public int orderID { get; set; }
        public string orderDate { get; set; }
        public string customerName { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string address { get; set; }
        public string status { get; set; }
        public int userID { get; set; }
        public string json { get; set; }
    }
}