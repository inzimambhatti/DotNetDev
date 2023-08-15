using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class StudentPromotionCreation
    {
        public int studentRegistrationID { get; set; }
        public int classSectionID { get; set; }
        public int branch_department_section_id { get; set; }
        public int departmentTypeID { get; set; }
        public string promotionType { get; set; }
        public int studentClassID { get; set; }
        public int classID { get; set; }
        public int sectionID { get; set; }
        public string remarks { get; set; }
        public int userID { get; set; }
        public string json { get; set; }
        public string spType { get; set; }
        public int newSectionID { get; set; }
        public int transfer_branch_department_section_id { get; set; }
        public int transferDepartmentTypeID { get; set; }
        public int transferSectionID { get; set; }
        public int transferClassID { get; set; }
    }
}