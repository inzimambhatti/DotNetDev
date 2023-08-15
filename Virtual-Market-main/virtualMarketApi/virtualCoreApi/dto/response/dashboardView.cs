using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace virtualCoreApi.Entities
{
    public class DashboardView
    {
        public int TotalOrders { get; set; }
        public float TotalSales { get; set; }
        public float profit { get; set; }
        public int activeOrders { get; set; }
        public float avtiveOrdersAmount { get; set; }
        public int cancelOrders { get; set; }
        public float cancelOrdersAmount { get; set; }
        public int totalCustomer { get; set; }
        public int totalClicks { get; set; }
        public int TotalSuppliesSale { get; set; }
        public float TotalSuppliesAmount { get; set; }
        public int TotalServices { get; set; }
        public float TotalServicesAmount { get; set; }
        public int completedOrders { get; set; }
        public int marketCount { get; set; }
        public float avgShopRating { get; set; }
        public int userID { get; set; }
        public int shopID { get; set; }

    }
}