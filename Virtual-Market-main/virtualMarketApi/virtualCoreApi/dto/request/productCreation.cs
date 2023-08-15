using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace virtualCoreApi.Entities
{
    public class ProductCreation
    {
        public int productID { get; set; }
        public string productName { get; set; }
        public int productTypeID { get; set; }
        public int shopCategoryID { get; set; }
        public Nullable<int> measurementUnitID { get; set; }
        public Nullable<float> rateOfPurchase { get; set; }
        public float rateOfSale { get; set; }
        public float discount { get; set; }
        public string productDescription { get; set; }
        public string json { get; set; }
        public int userID { get; set; }
        public int shopID { get; set; }
        public Nullable<int> qty { get; set; }
    }
}