using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class TaskAssigned
    {
        public int teacherID { get; set; }
        public string teacherFirstName { get; set; }
        public string teacherLastName { get; set; }
        public string markableEventDetailFromDate { get; set; }
        public int branch_department_section_id { get; set; }
        public string status { get; set; }
        public int eventID { get; set; }
        public string eventTitle { get; set; }
    }
}