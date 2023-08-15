using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace virtualCoreApi.Entities
{
    public class ShopRatingCreation
    {
        public int shopRatingID { get; set; }
        public int shopRating { get; set; }
        public string comment { get; set; }
        public int shopID { get; set; }
        public int userID { get; set; }
    }
}