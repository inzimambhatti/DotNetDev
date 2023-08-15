using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class FeeRegistrationCreation
    {
        public string json { get; set; }
        public string feeGenerateDate { get; set; }
        public int userID { get; set; }
        public string spType { get; set; }
        public int studentID { get; set; }
    }
}