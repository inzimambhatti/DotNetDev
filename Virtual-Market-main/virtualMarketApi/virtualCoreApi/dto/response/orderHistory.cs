using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace virtualCoreApi.Entities
{
    public class OrderHistory
    {
        public int userID { get; set; }
        public int orderID { get; set; }
        public string orderDate { get; set; }
        public string address { get; set; }
        public string userFullName { get; set; }
        public string userEmail { get; set; }
        public string userMobile { get; set; }
        public string userEDocExtension { get; set; }
        public string userImageDoc { get; set; }
        public int productID { get; set; }
        public string productName { get; set; }
        public string productDescription { get; set; }
        public string productEDoc { get; set; }
        public string productEDocExtension { get; set; }
        public float rateOfSale { get; set; }
        public int productTypeID { get; set; }
        public string productTypeTitle { get; set; }
        public int productCategoryID { get; set; }
        public string productCategoryTitle { get; set; }
        public int productAvailQuantity { get; set; }
        public int shopID { get; set; }
        public string shopName { get; set; }
        public string shopAddress { get; set; }
        public string shopMobile { get; set; }
        public string shopOpenTime { get; set; }
        public string shopClosingTime { get; set; }
        public string shopOwnerName { get; set; }
        public string shopLogoDoc { get; set; }
        public string shopEDocExtension { get; set; }
        public int shopCategoryID { get; set; }
        public string shopCategoryTitle { get; set; }
        public int shopTypeID { get; set; }
        public string shopTypeTitle { get; set; }
        public int orderedQuantity { get; set; }
        public string status { get; set; }
        
    }
}