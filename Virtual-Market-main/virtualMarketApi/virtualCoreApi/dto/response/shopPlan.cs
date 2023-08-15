using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace virtualCoreApi.Entities
{
    public class ShopPlan
    {
        public int shopPanID { get; set; }
        public float shopPlanPrice { get; set; }
        public int suppliesLimit { get; set; }
        public int servicesLimit { get; set; }
    }
}