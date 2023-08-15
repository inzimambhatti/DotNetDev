using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace virtualCoreApi.Entities
{
    public class UserCart
    {
        public int shopID { get; set; }
        public string shopName { get; set; }
        public string shopLogoDoc { get; set; }
        public string shopOpenTime { get; set; }
        public string shopClosingTime { get; set; }
        public string shopAddress { get; set; }
        public string shopMobile { get; set; }
        public int userID { get; set; }
        public string shopEDocExtension { get; set; }
        public int productID { get; set; }
        public string productName { get; set; }
        public int productTypeID { get; set; }
        public int rateOfSale { get; set; }
        public string productDescription { get; set; }
        public string productEDoc { get; set; }
        public int rateOfPurchase { get; set; }
        public string productEDocExtension { get; set; }
        public int qty { get; set; }
        public string productTypeTitle { get; set; }
        public int shopProductID { get; set; }
        public int userCartID { get; set; }
        public int sellerID { get; set; }
    }
}