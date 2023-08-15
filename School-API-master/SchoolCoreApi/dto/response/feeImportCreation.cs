using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class FeeImportCreation
    {
        public int studentMonthlyFeesID { get; set; }
        public string json { get; set; }
        public int userID { get; set; }
        public string spType { get; set; }
    }
}