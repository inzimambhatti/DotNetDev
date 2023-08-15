using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class DashboardHomeWork
    {
        public int classID { get; set; }
        public string className { get; set; }
        public int branch_department_section_id { get; set; }
        public int totalSubject { get; set; }
        public int effectiveSubject { get; set; }
    }
}