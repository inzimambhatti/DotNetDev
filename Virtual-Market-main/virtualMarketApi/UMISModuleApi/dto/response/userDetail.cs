using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UMISModuleApi.dto.response
{
    public class userDetail
    {
        public int userID { get; set; }     
        public string userFullname { get; set; }     
        public string userEmail { get; set; }
        public string userMobile { get; set; }
        public string userPassword { get; set; }
        public int userMode { get; set; }
        public string userImageDoc { get; set; }
        public int userRating { get; set; }
        public int genderID { get; set; }
        public string genderTitle { get; set; }
        public int currencyID { get; set; }
        public string userEDocExtension { get; set; }
        public string postalCode { get; set; }
        public int cityID { get; set; }
        public string cityName { get; set; }
        public string userAddress { get; set; }
        public int userAddressID { get; set; }
        public int countryID { get; set; }
        public string countryName { get; set; }
    }
}