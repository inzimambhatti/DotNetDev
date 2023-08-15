using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class LeaveCreation
    {
        public int newLeaveID { get; set; }
        public string leaveStartDate { get; set; }
        public string leaveEndDate { get; set; }
        public int leaveTypeID { get; set; }
        public int studentID { get; set; }
        public int userID { get; set; }
        public string spType { get; set; }
    }
}