using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace virtualCoreApi.Entities
{
    public class Service
    {
        public int shopServiceID { get; set; }
        public string serviceTitle { get; set; }
        public float pricePerHour { get; set; }
        public string serviceDescription { get; set; }
        public string serviceEDoc { get; set; }
        public string serviceEDocExtension { get; set; }
        public int shopID { get; set; }
    }
}