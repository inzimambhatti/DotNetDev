namespace SchoolCoreApi.Entities
{
    public class ClassSubjectStudents
    {
        public int classID { get; set; }
        public int sectionID { get; set; }
        public int branch_department_section_id { get; set; }
        public int studentID { get; set; }
        public int totalMarks { get; set; }
        public int markableEventResultMarks { get; set; }
        public string studentName { get; set; }
        public string studentRegistrationCode { get; set; }
    }
}