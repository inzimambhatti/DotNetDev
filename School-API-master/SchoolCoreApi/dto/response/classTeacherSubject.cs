namespace SchoolCoreApi.Entities
{
    public class ClassTeacherSubject
    {
        public int teacherID { get; set; }
        public int subjectID { get; set; }
        public string subjectTitle { get; set; }
        public int classID { get; set; }
        public int sectionID { get; set; }
        public int branch_department_section_id { get; set; }
    }
}