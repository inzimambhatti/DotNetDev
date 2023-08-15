using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace virtualCoreApi.Entities
{
    public class SlotBooking
    {
        public int slotBookingID { get; set; }
        public string bookingDate { get; set; }
        public int userID { get; set; }
        public string userFullName { get; set; }
        public string userMobile { get; set; }
        public string userEmail { get; set; }
        public int sellerID { get; set; }
        public float totalPrice { get; set; }
        public bool isAppointed { get; set; }
        public int productID { get; set; }
        public string productName { get; set; }
        public int productTypeID { get; set; }
        public string bookingStartTime { get; set; }
        public string bookingEndTime { get; set; }
        public int shopID { get; set; }
        public string shopName { get; set; }
        public string shopOwnerName { get; set; }
        public string status { get; set; }
        public int userMode { get; set; }
        public string userImageDoc { get; set; }
        public string userEDocExtension { get; set; }
        public string shopLogoDoc { get; set; }
        public string shopEDocExtension { get; set; }
        public string shopAddress { get; set; }
        public string shopMobile { get; set; }
        public string userAddress { get; set; }
        //public string userMobile { get; set; }
        public float shopRating { get; set; }
        public int roomID { get; set; }
    }

}