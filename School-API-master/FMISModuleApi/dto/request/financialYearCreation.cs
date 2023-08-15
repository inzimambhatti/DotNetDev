using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMISModuleApi.Entities
{
    public class FinancialYearCreation
    {
        public int finacialyearID { get; set; }
        public string financialYearStartDate { get; set; }
        public string financialYearEndDate { get; set; }
        public string financialYearValue { get; set; }
        public int userID { get; set; }
        public string spType { get; set; }
    }
}