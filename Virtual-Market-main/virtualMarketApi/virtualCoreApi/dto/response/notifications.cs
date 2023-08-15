using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace virtualCoreApi.Entities
{
    public class Notifications
    {
        public int notificationID { get; set; }
        public string notificationTitle { get; set; }
        public string notificationSubTitle { get; set; }
        public string userName { get; set; }
        public string sellerName { get; set; }
        public int userID { get; set; }
        public int sellerID { get; set; }
        public string createdOn { get; set; }
    }
}