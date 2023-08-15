using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class CreateSessionCreation
    {
        public int schoolSessionID { get; set; }
        public string schoolSessionTitle { get; set; }
        public string schoolSessionStartDate { get; set; }
        public string schoolSessionEndDate { get; set; }
        public string schoolSessionYear { get; set; }
        public string json { get; set; }
        public int userID { get; set; }
        public string spType { get; set; }
    }
}