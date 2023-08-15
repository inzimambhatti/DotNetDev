namespace SchoolCoreApi.Entities
{
    public class ClassAssignment
    {
        public int branch_department_section_id { get; set; }
        public int subjectID { get; set; }
        public int departmentTypeID { get; set; }
        public int classID { get; set; }
        public int sectionID { get; set; }
        public bool isClassIncharge { get; set; }
        public bool isAttendance { get; set; }
    }
}