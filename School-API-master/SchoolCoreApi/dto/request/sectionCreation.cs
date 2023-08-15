using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class SectionCreation
    {
        public int newSectionID { get; set; }
        public string sectionName { get; set; }
        public string sectionDescription { get; set; }
        public int userID { get; set; }
        public string spType { get; set; }
    }
}