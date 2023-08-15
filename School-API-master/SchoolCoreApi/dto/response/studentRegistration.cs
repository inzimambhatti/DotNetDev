using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class StudentRegistration
    {
        public int feesPlanID { get; set; }
        public int studentID { get; set; }
        public int statusCheck { get; set; }
        public int feeGenerated { get; set; }
        public string studentMonthlyFeesDate { get; set; }
        public int appliedUnApplied { get; set; }
        public string studentName { get; set; }
        public string studentDOB { get; set; }
        public string studentHealthHistory { get; set; }
        public string studentPlaceOfBirth { get; set; }
        public string studentGender { get; set; }
        public string studentPresentAddress { get; set; }
        public string studentReligon { get; set; }
        public string studentHobbies { get; set; }
        public string studentAchievements { get; set; }
        public string studentClassPreviouslyAttended { get; set; }
        public int parentID { get; set; }
        public string studentPreviousSchoolAttended { get; set; }
        public int studentRegistrationID { get; set; }
        public string studentEdoc { get; set; }
        public int city_id { get; set; }
        public string city_name { get; set; }
        public int countory_id { get; set; }
        public string studentRegistrationCode { get; set; }
        public int motherTongueID { get; set; }
        public int nationalityID { get; set; }
        public int calanderISchoollD { get; set; }
        public int classID { get; set; }
        public string className { get; set; }
        public int sectionID { get; set; }
        public string sectionName { get; set; }
        public int departmentTypeID { get; set; }
        public int branch_id { get; set; }
        public string branch_name { get; set; }
        public int branch_department_section_id { get; set; }
        public int classSectionID { get; set; }
        public int siblings { get; set; }
        public string studentRegistrationDate { get; set; }
        public int newAddmission { get; set; }
        public int isActive { get; set; }
    }
}