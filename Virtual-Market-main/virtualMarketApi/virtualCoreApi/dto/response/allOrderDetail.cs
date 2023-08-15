using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace virtualCoreApi.Entities
{
    public class AllOrderDetail
    {
        public string shopName { get; set; }       //shop Data
        public string shopOwnerName { get; set; }  //shop Data
        public string customerName { get; set; }   //Buyer Data
        public int orderID { get; set; }           //Order Data
        public string status { get; set; }         //Order Data
        public string mobile { get; set; }         //Buyer Data
        public string address { get; set; }        //Buyer Data
        public string orderDate { get; set; }      //Order Data
        public int userID { get; set; }            //Buyer Data
        public string userFullName { get; set; }   //BUyer Data
        public string userEmail { get; set; }      //Buyer Data
        public string userMobile { get; set; }
        public float totalPrice { get; set; }      //OrderData
        public int shopID { get; set; }
        public int productTypeID { get; set; }
        public int userMode { get; set; }
        public string shopLogoDoc { get; set; }
        public string shopEDocExtension { get; set; }
        public string userImageDoc { get; set; }
        public string userEDocExtension { get; set; }
        public int sellerID { get; set; }
        public int roomID { get; set; }

    }
}