using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace virtualCoreApi.Entities
{
    public class UserCartCreation
    {
        public int userCartID { get; set; }
        public int userID { get; set; }
        public int shopProductID { get; set; }
        public int productID { get; set; }
        public int shopID { get; set; }
    }
}