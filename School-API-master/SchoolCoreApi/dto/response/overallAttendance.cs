using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class OverallAttendance
    {
        public string branch_name { get; set; }
        public int branch_department_section_id { get; set; }
        public int totalPresentStudent { get; set; }
        public int totalAbsentStudent { get; set; }
        public int totalLeaveStudent { get; set; }
    }
}