using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class StudentRegistrationCreation
    {
        public int newStudentRegistrationID { get; set; }
        public string studentRegistrationCode { get; set; }
        public string studentName { get; set; }
        public string studentDOB { get; set; }
        public string studentHealthHistory { get; set; }
        public string studentGender { get; set; }
        public string studentReligon { get; set; }
        public int nationalityID { get; set; }
        public string studentPresentAddress { get; set; }
        public string studentHobbies { get; set; }
        public string studentAchievements { get; set; }
        public string studentClassPreviouslyAttended { get; set; }
        public int motherTongueID { get; set; }
        public string studentPreviousSchoolAttended { get; set; }
        public int registrationStatusID { get; set; }
        public int calanderISchoollD { get; set; }
        public int city_id { get; set; }
        public int branch_department_section_id { get; set; }
        public int departmentTypeID { get; set; }
        public int classID { get; set; }
        public int sectionID { get; set; }
        public string student_picture_path { get; set; }
        public string student_picture { get; set; }
        public string student_picture_extension { get; set; }
        public int userID { get; set; }
        public string spType { get; set; }
        public string studentRegistrationDate { get; set; }
    }
}