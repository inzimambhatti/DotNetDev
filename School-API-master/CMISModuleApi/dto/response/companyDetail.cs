using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMISModuleApi.Entities
{
    public class companyDetail
    {
        public int company_id { get; set; }
        public string company_name { get; set; }
        public string company_short_name { get; set; }
        public string company_ntn { get; set; }
        public string company_strn { get; set; }
        public string company_registeration_date { get; set; }
        public string company_registeration_no { get; set; }
        public string company_logo_path { get; set; }
        public int company_type_id { get; set; }
        public string company_type_title { get; set; }
        public string contact_info_title { get; set; }
        public int contact_info_id { get; set; }
        public int contact_type_id { get; set; }
        public string contact_type_title { get; set; }
    }
}