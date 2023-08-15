using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class CopyCheckingCreation
    {
        public int userID { get; set; }
        public int branchID { get; set; }
        public int subjectID { get; set; }
        public int classID { get; set; }
        public int sectionID { get; set; }
        public int departmentTypeID { get; set; }
        public string json { get; set; }
        public string fromDate { get; set; }
    }
}