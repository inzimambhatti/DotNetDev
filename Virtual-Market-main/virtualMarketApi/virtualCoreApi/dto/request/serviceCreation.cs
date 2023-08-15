using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace virtualCoreApi.Entities
{
    public class ServiceCreation
    {
        public int shopServiceID { get; set; }
        public string serviceTitle { get; set; }
        public float pricePerHour { get; set; }
        public string serviceDescription { get; set; }
        public string applicationEDocPath { get; set; }
        public string applicationEdocExtenstion { get; set; }
        public string applicationEDoc { get; set; }
        public int shopID { get; set; }
        public int userID { get; set; }
    }
}