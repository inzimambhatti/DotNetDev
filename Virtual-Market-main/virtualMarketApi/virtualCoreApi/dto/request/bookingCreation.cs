using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace virtualCoreApi.Entities
{
    public class BookingCreation
    {
        public int slotBookingID { get; set; }
        public string bookingDate { get; set; }
        public int userID { get; set; }
        public int productID { get; set; }
        public string bookingStartTime { get; set; }
        public string isAppointed { get; set; }
        public string bookingEndTime { get; set; }
        public string status { get; set; }
    }
}