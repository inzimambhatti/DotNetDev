using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class ClassReg   
    {
        public string className { get; set; }
        public int classID { get; set; }
        public int sectoinID { get; set; }
        public string sectionName { get; set; }
        public int classDepartmentID { get; set; }
        public int departmentTypeID { get; set; }
        public string departmentTypeName { get; set; }
        public string classDepartmentName { get; set; }
        public int classSectionID { get; set; }
        public int branch_department_section_id { get; set; }
        public int isDeleted { get; set; }
        public int branch_id { get; set; }
        public string branch_name { get; set; }
    }
}