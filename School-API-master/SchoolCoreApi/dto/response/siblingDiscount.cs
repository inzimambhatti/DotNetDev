using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class SiblingDiscount
    {
        public int studentID { get; set; }
        public string parentName { get; set; }
        public string parentPassportOrCNIC { get; set; }
        public int parentID { get; set; }
        public string studentRegistrationDate { get; set; }
        public int siblings { get; set; }
        public int feesElementID { get; set; }
        public int feesElementTypeID { get; set; }
        public int siblingAmount { get; set; }
    }
}