using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace virtualCoreApi.Entities
{
    public class CustomerOfSeller
    {
        public int customerID { get; set; }
        public string userFullName { get; set; }
        public int genderID { get; set; }
        public string genderTitle { get; set; }
        public string userEmail { get; set; }
        public string userEDocExtension { get; set; }
        public string userImageDoc { get; set; }
        public string userMobile { get; set; }
        public int sellerID { get; set; }
        public int orderID { get; set; }
        public string address { get; set; }
    }
}