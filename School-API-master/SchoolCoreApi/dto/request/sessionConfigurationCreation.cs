using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class SessionConfigurationCreation
    {
        public int calanderISchoollD { get; set; }
        public string calanderSchoolDescription { get; set; }
        public string sessionStartDate { get; set; }
        public string sessionEndDate { get; set; }
        public string sessionYear { get; set; }
        public int userID { get; set; }
        public string spType { get; set; }
    }
}