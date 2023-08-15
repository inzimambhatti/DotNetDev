using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class TeacherCreation
    {
        public int newTeacherID { get; set; }
        public string teacherFirstName { get; set; }
        public string teacherLastName { get; set; }
        public string teacherCNIC { get; set; }
        public string teacherGender { get; set; }
        public string teacherReligion { get; set; }
        public string teacherSubject { get; set; }
        public string teacherEmail { get; set; }
        public string teacherDOB { get; set; }
        public string teacherAddress { get; set; }
        public string teacherPhone { get; set; }
        public string teacherEmergencyNo { get; set; }
        public string teacherQualification { get; set; }
        public int nationalityID { get; set; }
        public string teacher_picture_path { get; set; }
        public string teacher_picture { get; set; }
        public string teacher_picture_extension { get; set; }
        public int userID { get; set; }
        public string json { get; set; }
        public string spType { get; set; }
        public string branchJson { get; set; }
    }
}