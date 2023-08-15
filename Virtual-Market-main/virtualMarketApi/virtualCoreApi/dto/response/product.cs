using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace virtualCoreApi.Entities
{
    public class Product
    {
        public int productID { get; set; }
        public string productName { get; set; }
        public int productTypeID { get; set; }
        public string productTypeTitle { get; set; }
        public int shopCategoryID { get; set; }
        public string shopCategoryTitle { get; set; }
        public float rateOfPurchase { get; set; }
        public float rateOfSale { get; set; }
        public float discount { get; set; }
        public int shopID { get; set; }
        public string productDescription { get; set; }
        public string productImagesJson { get; set; }
        // public string productEDocExtension { get; set; }
        public int measurementUnitID { get; set; }
        public string measurementUnitTitle { get; set; }
        public int qty { get; set; }
        public int sellerID { get; set; }
    }
}