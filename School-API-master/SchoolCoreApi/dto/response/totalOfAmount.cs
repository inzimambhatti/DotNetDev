using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class TotalOfAmount
    {
        public int currentMonthTotal { get; set; }
        public int previousMonthTotal { get; set; }
    }
}