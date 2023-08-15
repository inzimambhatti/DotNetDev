using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolCoreApi.Services;
using Microsoft.Extensions.Options;
using SchoolCoreApi.Configuration;
using SchoolCoreApi.Entities;
using Dapper;
using System.Data;
using Newtonsoft.Json;

namespace SchoolCoreApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController  : ControllerBase
    {
        private readonly IOptions<conStr> _dbCon;
        private string cmd;

        public EventController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        }

        [HttpGet("getEventCalendar")]
        public IActionResult getEventCalendar(int teacherID,int branch_department_section_id,int departmentTypeID,int classID,int sectionID)
        {
            try
            {
                // if (teacherID == 0)
                // {
                //     cmd = "SELECT DISTINCT sessionEventID,[start],[end],eventID,title,schoolSessionID,isMarkable FROM view_eventCalender";    
                // }
                // else
                // {
                //     cmd = "SELECT DISTINCT * FROM view_eventCalender where teacherID = " + teacherID + " OR teacherID is null ";
                // }

                cmd = @"DECLARE	@return_value int
                        EXEC	@return_value = [dbo].[sp_eventCalenderView]
                                @branch_department_section_id = " + branch_department_section_id + @",
                                @departmentTypeID = " + departmentTypeID + @",
                                @classID = " + classID + @",
                                @sectionID = " + sectionID + @",
                                @teacherID = " + teacherID + @" ";

                var appMenu = dapperQuery.Qry<EventCalendar>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }
        
        [HttpGet("getMobileEventCalendar")]
        public IActionResult getMobileEventCalendar(int teacherID)
        {
            try
            {
                if (teacherID == 0)
                {
                    cmd = "SELECT * FROM view_mobileEvent_calendar ";    
                }
                else
                {
                    cmd = "SELECT * FROM view_mobileEvent_calendar where teacherID = " + teacherID + " OR teacherID is null ";
                }
                var appMenu = dapperQuery.Qry<MobileEventCalendar>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getEventNature")]
        public IActionResult getEventNature()
        {
            try
            {
                cmd = "select * from tbl_event_nature";
                var appMenu = dapperQuery.Qry<EventNature>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getTeacherBranches")]
        public IActionResult getTeacherBranches(int teacherID)
        {
            try
            {
                if (teacherID == 0)
                {
                    cmd = "SELECT distinct branch_department_section_id,branchName FROM view_teacherBranches";    
                }
                else
                {
                    cmd = "SELECT * FROM view_teacherBranches where teacherID = " + teacherID + "";    
                }
                
                var appMenu = dapperQuery.Qry<TeacherBranch>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getStudentHomeWork")]
        public IActionResult getStudentHomeWork(int studentID,string homeWorkDate)
        {
            try
            {
                cmd = "SELECT * FROM view_studentHomeWork where studentID = " + studentID + " and cast(markableEventDetailFromDate as date) = cast(' " + homeWorkDate + " ' as date)";
                var appMenu = dapperQuery.Qry<StudentHomeWork>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getStudentCopyChecking")]
        public IActionResult getStudentCopyChecking(int studentID,string homeWorkDate)
        {
            try
            {
                cmd = "SELECT * FROM view_studentCopyChecking where studentID = " + studentID + " and cast(markableEventDetailFromDate as date) = cast(' " + homeWorkDate + " ' as date)";
                var appMenu = dapperQuery.Qry<StudentCopyChecking>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getStudentClassWork")]
        public IActionResult getStudentClassWork(int studentID,string homeWorkDate)
        {
            try
            {
                cmd = "SELECT * FROM view_studentClassWork where studentID = " + studentID + " and cast(markableEventDetailFromDate as date) = cast(' " + homeWorkDate + " ' as date)";
                var appMenu = dapperQuery.Qry<StudentHomeWork>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getMobileAssesments")]
        public IActionResult getMobileAssesments(int studentID)
        {
            try
            {
                cmd = "select * from view_mobileAssesments where studentID = " + studentID + " ";
                var appMenu = dapperQuery.Qry<StudentMobileAssesments>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getStudentMobileExam")]
        public IActionResult getStudentMobileExam(int studentID)
        {
            try
            {
                cmd = "select * from view_studentExamMobile where studentID = " + studentID + " ";
                var appMenu = dapperQuery.Qry<StudentMobileAssesments>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getStudentClassTest")]
        public IActionResult getStudentClassTest(int studentID,string classTestDate)
        {
            try
            {
                cmd = "SELECT * FROM view_classTest where studentID = " + studentID + " and cast(markableEventDetailFromDate as date) = cast('" + classTestDate + "' as date)";
                var appMenu = dapperQuery.Qry<StudentHomeWork>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getHomeWorkLast7Days")]
        public IActionResult getHomeWorkLast7Days(int branchID,int departmentTypeID,int classID,int sectionID,int subjectID)
        {
            try
            {
                cmd = @"select DISTINCT TOP 7 med.markableEventDetailID, med.markableEventDetailDescription, med.markableEventDetailFromDate, tcss.teacherID, t.teacherFirstName, 
                        DATENAME(WEEKDAY, med.markableEventDetailFromDate) AS [dayName]
                        from tbl_markable_event_detail med 
                        inner join tbl_session_events se on se.sessionEventID = med.sessionEventID
                        INNER JOIN tbl_class_section_subjects css ON css.classSectionSubjectID = med.classSectionSubjectID
                        INNER JOIN tbl_class_section cs ON cs.classSectionID = css.classSectionID
                        INNER JOIN tbl_class_department cd ON cd.classDepartmentID = cs.classDepartmentID
                        Inner join tbl_student_class sr on sr.classID = cs.classSectionID
                        Inner join tbl_teacher_class_section_subject tcss on tcss.classSectionSubjectID = css.classSectionSubjectID
                        Inner join tbl_teacher t on t.teacherID = tcss.teacherID
                        where se.eventID = 33 and cd.branch_department_section_id = " + branchID + @" and cs.classID = " + classID + @"
                        and cs.sectionID = " + sectionID + @" and css.subjectID = " + subjectID + @" and cd.departmentTypeID = " + departmentTypeID + @" and med.isDeleted = 0 and se.isDeleted = 0
                        order by med.markableEventDetailFromDate DESC";
                var appMenu = dapperQuery.Qry<HomeWorkLast7Days>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getClassTestLast7Days")]
        public IActionResult getClassTestLast7Days(int branchID,int departmentTypeID,int classID,int sectionID,int subjectID)
        {
            try
            {
                cmd = @"select DISTINCT TOP 7 med.markableEventDetailID, med.markableEventDetailDescription, med.markableEventDetailFromDate, tcss.teacherID, t.teacherFirstName, 
                        DATENAME(WEEKDAY, med.markableEventDetailFromDate) AS [dayName]
                        from tbl_markable_event_detail med 
                        inner join tbl_session_events se on se.sessionEventID = med.sessionEventID
                        INNER JOIN tbl_class_section_subjects css ON css.classSectionSubjectID = med.classSectionSubjectID
                        INNER JOIN tbl_class_section cs ON cs.classSectionID = css.classSectionID
                        INNER JOIN tbl_class_department cd ON cd.classDepartmentID = cs.classDepartmentID
                        Inner join tbl_student_class sr on sr.classID = cs.classSectionID
                        Inner join tbl_teacher_class_section_subject tcss on tcss.classSectionSubjectID = css.classSectionSubjectID
                        Inner join tbl_teacher t on t.teacherID = tcss.teacherID
                        where se.eventID = 43 and cd.branch_department_section_id = " + branchID + @" and cs.classID = " + classID + @"
                        and cs.sectionID = " + sectionID + @" and css.subjectID = " + subjectID + @" and cd.departmentTypeID = " + departmentTypeID + @" and med.isDeleted = 0 and se.isDeleted = 0
                        and css.isDeleted = 0 and cs.isDeleted = 0 and cd.isDeleted = 0 and tcss.isDeleted = 0 and t.isDeleted = 0
                        order by med.markableEventDetailFromDate DESC";
                var appMenu = dapperQuery.Qry<HomeWorkLast7Days>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getClassWorkLast7Days")]
        public IActionResult getClassWorkLast7Days(int branchID,int departmentTypeID,int classID,int sectionID,int subjectID)
        {
            try
            {
                cmd = @"select DISTINCT TOP 7 med.markableEventDetailID, med.markableEventDetailDescription, med.markableEventDetailFromDate, tcss.teacherID, t.teacherFirstName, 
                        DATENAME(WEEKDAY, med.markableEventDetailFromDate) AS [dayName]
                        from tbl_markable_event_detail med 
                        inner join tbl_session_events se on se.sessionEventID = med.sessionEventID
                        INNER JOIN tbl_class_section_subjects css ON css.classSectionSubjectID = med.classSectionSubjectID
                        INNER JOIN tbl_class_section cs ON cs.classSectionID = css.classSectionID
                        INNER JOIN tbl_class_department cd ON cd.classDepartmentID = cs.classDepartmentID
                        Inner join tbl_student_class sr on sr.classID = cs.classSectionID
                        Inner join tbl_teacher_class_section_subject tcss on tcss.classSectionSubjectID = css.classSectionSubjectID
                        Inner join tbl_teacher t on t.teacherID = tcss.teacherID
                        where se.eventID = 34 and cd.branch_department_section_id = " + branchID + @" and cs.classID = " + classID + @"
                        and cs.sectionID = " + sectionID + @" and css.subjectID = " + subjectID + @" and cd.departmentTypeID = " + departmentTypeID + @" and med.isDeleted = 0 and se.isDeleted = 0
                        order by med.markableEventDetailFromDate DESC";
                var appMenu = dapperQuery.Qry<HomeWorkLast7Days>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getCopyChecking")]
        public IActionResult getCopyChecking(int branchID,int subjectID,int classID,int sectionID,int departmentTypeID,string date)
        {
            try
            {
                cmd = @"select DISTINCT s.studentID, s.studentName, s.studentRegistrationCode ,mer.remarks
                        from tbl_markable_event_detail med 
                        inner join tbl_session_events se on se.sessionEventID = med.sessionEventID
                        INNER JOIN tbl_class_section_subjects css ON css.classSectionSubjectID = med.classSectionSubjectID
                        INNER JOIN tbl_class_section cs ON cs.classSectionID = css.classSectionID
                        INNER JOIN tbl_class_department cd ON cd.classDepartmentID = cs.classDepartmentID
                        Inner join tbl_student_class sr on sr.classID = cs.classSectionID
                        INNER JOIN tbl_student s ON s.studentID = sr.studentID
                        Left Join tbl_markable_events_results mer on mer.markableEventDetailID = med.markableEventDetailID 
                        and mer.studentID = s.studentID
                        where se.eventID = 33 and cd.branch_department_section_id = " + branchID + @" and cs.classID = " + classID + @"
                        and cs.sectionID = " + sectionID + @" and css.subjectID = " + subjectID + " and cast(med.markableEventDetailFromDate as date ) = cast('" + date + "' as date) and cd.departmentTypeID = " + departmentTypeID + "";
                var appMenu = dapperQuery.Qry<CopyCheckingStudents>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getTeacherBranchDepartment")]
        public IActionResult getTeacherBranchDepartment(int teacherID,int branchID)
        {
            try
            {
                if (branchID == 0)
                {
                    cmd = "SELECT distinct * FROM view_teacherBranchDepartment where teacherID = " + teacherID + "";    
                }
                else if (teacherID == 0)
                {
                    cmd = "SELECT distinct departmentTypeID,departmentTypeName FROM view_teacherBranchDepartment where branch_department_section_id = " + branchID + "";
                }
                else
                {
                    cmd = "SELECT distinct * FROM view_teacherBranchDepartment where teacherID = " + teacherID + " and branch_department_section_id = " + branchID + "";
                }
                
                var appMenu = dapperQuery.Qry<TeacherBranchGroup>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getTeacherBranchSubject")]
        public IActionResult getTeacherBranchSubject(int teacherID,int branchID,int departmentTypeID)
        {
            try
            {
                if (branchID == 0)
                {
                    cmd = "SELECT distinct * FROM view_teacherBranchSubject where teacherID = " + teacherID + "";    
                }
                else if (teacherID == 0)
                {
                    cmd = "SELECT distinct subjectID,subjectTitle FROM view_teacherBranchSubject where branch_department_section_id = " + branchID + "";
                }
                else if (departmentTypeID == 0)
                {
                    cmd = "SELECT distinct * FROM view_teacherBranchSubject where teacherID = " + teacherID + " and branch_department_section_id = " + branchID + "  ";
                }
                else
                {
                    cmd = "SELECT distinct * FROM view_teacherBranchSubject where teacherID = " + teacherID + " and branch_department_section_id = " + branchID + " and departmentTypeID = " + departmentTypeID + " ";
                }
                
                var appMenu = dapperQuery.Qry<TeacherBranchSubject>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getTeachersSubjects")]
        public IActionResult getTeachersSubjects(int teacherID,int branchID,int departmentTypeID)
        {
            try
            {
                if (branchID == 0 && teacherID != 0)
                {
                    cmd = "SELECT distinct branch_department_section_id,subjectID,subjectTitle FROM view_teacherBranchSubject where teacherID = " + teacherID + "";    
                }
                else if (teacherID == 0 && branchID != 0 )
                {
                    cmd = "SELECT distinct subjectID,subjectTitle FROM view_teacherBranchSubject where branch_department_section_id = " + branchID + "";
                }
                else if (teacherID == 0 && branchID == 0 && departmentTypeID == 0)
                {
                    cmd = "SELECT distinct branch_department_section_id,subjectID,subjectTitle FROM view_teacherBranchSubject ";
                }
                else if (departmentTypeID == 0 && teacherID != 0 && branchID != 0)
                {
                    cmd = "SELECT distinct * FROM view_teacherBranchSubject where teacherID = " + teacherID + " and branch_department_section_id = " + branchID + "  ";
                }
                else
                {
                    cmd = "SELECT distinct * FROM view_teacherBranchSubject where teacherID = " + teacherID + " and branch_department_section_id = " + branchID + " and departmentTypeID = " + departmentTypeID + " ";
                }
                
                var appMenu = dapperQuery.Qry<getTeacherSubjectB>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }
        
        [HttpGet("getTeacherBranchSubjectClass")]
        public IActionResult getTeacherBranchSubjectClass(int teacherID,int branchID,int subjectID,int departmentTypeID)
        {
            try
            {
                if (branchID == 0 && subjectID == 0 && teacherID != 0 && departmentTypeID != 0)
                {
                    cmd = "SELECT * FROM view_teacherBranchSubjectClass where teacherID = " + teacherID + "";    
                }
                else if(teacherID == 0 && branchID != 0 && subjectID != 0 && departmentTypeID == 0)
                {
                    cmd = "SELECT distinct classID,className FROM view_teacherBranchSubjectClass where branch_department_section_id = " + branchID + " and subjectID = " + subjectID + " ";
                }
                else if(teacherID != 0 && branchID == 0 && subjectID == 0 && departmentTypeID == 0)
                {
                    cmd = "SELECT distinct classID,className,branch_department_section_id,subjectID,departmentTypeID FROM view_teacherBranchSubjectClass where teacherID = " + teacherID + " ";
                }
                else if(teacherID == 0 && branchID == 0 && subjectID == 0 && departmentTypeID == 0)
                {
                    cmd = "SELECT distinct classID,className,branch_department_section_id,subjectID,departmentTypeID FROM view_teacherBranchSubjectClass  ";
                }
                else if(teacherID != 0 && branchID != 0 && subjectID != 0 && departmentTypeID == 0)
                {
                    cmd = "SELECT * FROM view_teacherBranchSubjectClass where branch_department_section_id = " + branchID + " and subjectID = " + subjectID + " and teacherID = " + teacherID + "";
                }
                else
                {
                    cmd = "SELECT * FROM view_teacherBranchSubjectClass where branch_department_section_id = " + branchID + " and subjectID = " + subjectID + " and teacherID = " + teacherID + " and departmentTypeID = " + departmentTypeID + "";
                }
                
                var appMenu = dapperQuery.Qry<TeacherBranchSubjectClass>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }
        
        [HttpGet("getTeacherBranchSubjectClassSection")]
        public IActionResult getTeacherBranchSubjectClassSection(int teacherID,int branchID,int subjectID,int classID,int departmentTypeID)
        {
            try
            {
                if (branchID == 0 && subjectID == 0 && classID == 0 && departmentTypeID == 0 && teacherID != 0)
                {
                    cmd = @"SELECT distinct sectionID,sectionName,branch_department_section_id,classID,subjectID ,departmentTypeID
                            FROM view_teacherBranchSubjectClassSection 
                            where teacherID = " + teacherID + " ";    
                }
                else if (teacherID == 0 && branchID != 0 && subjectID != 0 && classID != 0 && departmentTypeID == 0)
                {
                    cmd = "SELECT Distinct sectionID,sectionName FROM view_teacherBranchSubjectClassSection where branch_department_section_id = " + branchID + " and subjectID = " + subjectID + " and classID = " + classID + " ";
                }
                else if (teacherID == 0 && branchID == 0 && subjectID == 0 && classID == 0 && departmentTypeID == 0)
                {
                    cmd = @"SELECT distinct sectionID,sectionName,branch_department_section_id,classID,subjectID ,departmentTypeID
                            FROM view_teacherBranchSubjectClassSection ";
                }
                else if (departmentTypeID == 0 && teacherID != 0 && branchID != 0 && subjectID != 0 && classID != 0)
                {
                    cmd = "SELECT * FROM view_teacherBranchSubjectClassSection where branch_department_section_id = " + branchID + " and subjectID = " + subjectID + " and classID = " + classID + " and teacherID = " + teacherID + "";
                }
                else
                {
                    cmd = "SELECT * FROM view_teacherBranchSubjectClassSection where branch_department_section_id = " + branchID + " and subjectID = " + subjectID + " and classID = " + classID + " and teacherID = " + teacherID + " and departmentTypeID = " + departmentTypeID + " ";
                }
                
                var appMenu = dapperQuery.Qry<TeacherBranchSubjectClassSection>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getEventByType")]
        public IActionResult getEventByType(int eventTypeID)
        {
            try
            {
                cmd = "SELECT * FROM tbl_events where eventTypeID = " + eventTypeID + " and isDeleted = 0";
                var appMenu = dapperQuery.Qry<EventTitle>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("saveEventCrud")]
        public IActionResult saveEventCrud(CreateEventCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_EventCrud",model,_dbCon);
                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("saveCreateEvent")]
        public IActionResult saveCreateEvent(EventCrudCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_MobileEvent",model,_dbCon);
                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("saveHomeWork")]
        public IActionResult saveHomeWork(HomeWorkCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_homeWork",model,_dbCon);
                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("saveClassTest")]
        public IActionResult saveClassTest(HomeWorkCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_classTest",model,_dbCon);
                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("saveClassWork")]
        public IActionResult saveClassWork(ClassWorkCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_classWork",model,_dbCon);
                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("saveCopyChecking")]
        public IActionResult saveCopyChecking(CopyCheckingCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_copyChecking",model,_dbCon);
                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }
        
    }
}