using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace virtualCoreApi.Entities
{
    public class Chats
    {
        public int roomID { get; set; }
        public int userID { get; set; }
        public string userName { get; set; }
        public string userImageDoc { get; set; }
        public string userEDocExtension { get; set; }
        public int sellerID { get; set; }
        public string sellerName { get; set; }
        public string sellerImageDoc { get; set; }
        public string sellerEDocExtension { get; set; }
    }
}