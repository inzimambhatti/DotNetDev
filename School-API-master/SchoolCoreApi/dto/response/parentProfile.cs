using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class ParentProfile
    {
        public int parentID { get; set; }
        public string parentName { get; set; }
        public int parentTypeID { get; set; }
        public string parentTypeName { get; set; }
        public int studentID { get; set; }
        public string parentOccupation { get; set; }
        public int motherTongueID { get; set; }
        public string parentPassportOrCNIC { get; set; }
        public int nationalityID { get; set; }
        public string parentEmailAddress { get; set; }
        public string parentTelephoneOffice { get; set; }
        public string parentMobile { get; set; }
        public string parentResidenceAddress { get; set; }
        public int parentLiveOrDeceasedFlag { get; set; }
        public int countory_id { get; set; }
    }
}