using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace virtualCoreApi.Entities
{
    public class SellerOfCustomer
    {
        public int sellerID { get; set; }
        public string userFullName { get; set; }
        public int genderID { get; set; }
        public string genderTitle { get; set; }
        public string sellerEmail { get; set; }
        public string userEDocExtension { get; set; }
        public string userImageDoc { get; set; }
        public string sellerMobile { get; set; }
        public int customerID { get; set; }
        public string shopName { get; set; }
        public string shopAddress { get; set; }
        public float shopRatingAvg { get; set; }
        public string shopEDocExtension { get; set; }
        public string shopLogoDoc { get; set; }
        public string shopMobile { get; set; }
        public string userEmail { get; set; }
        // public int shopID { get; set; }

    }
}