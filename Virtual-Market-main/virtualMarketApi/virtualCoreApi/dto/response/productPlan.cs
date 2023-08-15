using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace virtualCoreApi.Entities
{
    public class ProductPlan
    {
        public int planTypeID { get; set; }
        public float planTotalPrice { get; set; }
        public int noOfServices { get; set; }
        public int noOfSupplies { get; set; }
        public string planTypeTitle { get; set; }
        public int pricePerService { get; set; }
        public int pricePerSupply { get; set; }
        public int shopTypeID { get; set; }
    }
}