using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMISModuleApi.dto.request
{
    public class companyCreation
    {
        public int new_company_id { get; set; }
        public string company_name { get; set; }
        public string company_short_name { get; set; }
        public string company_ntn { get; set; }
        public string company_strn { get; set; }
        public string company_registeration_date { get; set; }
        public string company_registeration_no { get; set; }
        public string company_picture_path { get; set; }
        public string company_picture { get; set; }
        public string company_picture_extension { get; set; }
        public int company_type_id { get; set; }
        //public int company_bus_id { get; set; }
        public string json { get; set; }
        public int userID { get; set; }
        public string spType { get; set; }
    }
}