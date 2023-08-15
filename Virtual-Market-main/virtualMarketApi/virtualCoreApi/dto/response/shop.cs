using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace virtualCoreApi.Entities
{
    public class Shop
    {
        public int shopID { get; set; }
        public string shopName { get; set; }
        public string shopOwnerName { get; set; }
        public int shopTypeID { get; set; }
        public string shopTypeTitle { get; set; }
        public int shopCategoryID { get; set; }
        public string shopCategoryTitle { get; set; }
        public int userID { get; set; }
        public string shopDescription { get; set; }
        public string shopLogoDoc { get; set; }
        public string shopEDocExtension { get; set; }
        public string shopOpenTime { get; set; }
        public string shopClosingTime { get; set; }
        public int shopRating { get; set; }
        public int currencyID { get; set; }
        public string currencyTitle { get; set; }
        public int cityID { get; set; }
        public string cityName { get; set; }
        public string shopAddress { get; set; }
        public string shopMobile { get; set; }
        public bool isFavorite { get; set; }
        public float shopRatingAvg { get; set; }
        public string userEDocExtension { get; set; }
        public int loginUserID { get; set; }
    }
}