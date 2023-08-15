using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class FeeGenerationCreation
    {
        public string json { get; set; }
        public int StartMonth { get; set; }
        public int StartYear { get; set; }
        public int userID { get; set; }
        public string spType { get; set; } 
    }
}