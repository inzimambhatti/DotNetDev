using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace virtualCoreApi.Entities
{
    public class ShopCategory
    {
        public int shopCategoryID { get; set; }
        public string shopCategoryTitle { get; set; }
        public int shopTypeID { get; set; }
        public string shopTypeTitle { get; set; }
    }
}