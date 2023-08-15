using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMISModuleApi.Entities
{
    public class FinancialYear
    {
        public string financialYearValue { get; set; }
        public string financialYearStartDate { get; set; }
        public string financialYearEndDate { get; set; }
        public string days { get; set; }
    }
}