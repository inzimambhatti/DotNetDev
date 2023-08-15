using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UMISModuleApi.dto.request
{
    public class userCreation
    {
        public int userID { get; set; }
        public string userFullName { get; set; }
        public string userEmail { get; set; }
        public string userMobile { get; set; }
        public string userPassword { get; set; }
        public int userMode { get; set; }
        public int registrationModeID { get; set; }
        public int genderID { get; set; }
        public int userRating { get; set; }
        public int currencyID { get; set; }
        public string userImageDoc { get; set; }
        public string applicationEDocPath { get; set; }
        public string applicationEdocExtenstion { get; set; }
    }
}