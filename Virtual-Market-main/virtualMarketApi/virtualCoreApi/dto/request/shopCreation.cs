using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace virtualCoreApi.Entities
{
    public class ShopCreation
    {
        public int shopID { get; set; }
        public string shopName { get; set; }
        public string shopOwnerName { get; set; }
        public int shopCategoryID { get; set; }
        public int shopTypeID { get; set; }
        public int cityID { get; set; }
        public int currencyID { get; set; }
        public string shopAddress { get; set; }
        public string shopMobile { get; set; }
        public string shopOpenTime { get; set; }
        public string shopClosingTime { get; set; }
        public string shopDescription { get; set; }
        public string applicationEDocPath { get; set; }
        public string applicationEdocExtenstion { get; set; }
        public string applicationEDoc { get; set; }
        public int userID { get; set; }

    }
}