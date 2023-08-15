using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMISApi.Entities
{
    public class ChartOfAccountCreation
    {
        public int coaID { get; set; }
        public string coaTitle { get; set; }
        public int level01 { get; set; }
        public int level02 { get; set; }
        public int level03 { get; set; }
        public int level04 { get; set; }
        public int level05 { get; set; }
        public string coaCode { get; set; }
        public string refCode { get; set; }
        public int userID { get; set; }
        public string spType { get; set; }
    }
}