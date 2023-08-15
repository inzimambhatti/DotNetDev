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
    public class EventCalendarController : ControllerBase
    {
        private readonly IOptions<conStr> _dbCon;
        private string cmd;

        public EventCalendarController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        }
    
        [HttpGet("getTeacherClasses")]
        public IActionResult getTeacherClasses(int teacherID)
        {
            try
            {
                cmd = @"select tcss.teacherID,s.subjectID,s.subjectTitle,c.classID,c.className,cs.sectionID,sec.sectionName,cd.branch_department_section_id 
                            ,concat (c.className, ' ' ,'(',sec.sectionName,')',' ', '(',s.subjectTitle,')') as classSectionSubject
                        from tbl_class_section_subjects as css inner join
                            tbl_teacher_class_section_subject as tcss on css.classSectionSubjectID = tcss.classSectionSubjectID inner join
                            tbl_subjects as s on css.subjectID = s.subjectID inner join 
                            tbl_class_section as cs on css.classSectionID = cs.classSectionID inner join
                            tbl_class_department as cd on cs.classDepartmentID = cd.classDepartmentID inner join
                            tbl_calss as c on cs.classID = c.classID inner join tbl_section as sec on cs.sectionID = sec.sectionID 
                        where tcss.teacherID = "+teacherID+" and  tcss.isDeleted = 0 and css.isDeleted = 0 and s.isDeleted = 0 and cs.isDeleted = 0 and c.isDeleted = 0 and cd.isDeleted = 0";
                var appMenu = dapperQuery.Qry<TeacherClasses>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getSessionClassEvent")]
        public IActionResult getSessionClassEvent(int sessionEventID)
        {
            try
            {
                cmd = "select Distinct eventID,eventTitle,classID,className from view_SessionEventClassSectionSubject where sessionEventID = " + sessionEventID + "";
                var appMenu = dapperQuery.Qry<SessionEventSubjectClass>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getSessionEventSubject")]
        public IActionResult getSessionEventSubject(int sessionEventID)
        {
            try
            {
                cmd = "select Distinct subjectID,subjectTitle from view_SessionEventClassSectionSubject where sessionEventID = " + sessionEventID + "";
                var appMenu = dapperQuery.Qry<SessionEventSubjectClass>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getSessionEvents")]
        public IActionResult getSessionEvents(int sessionEventID)
        {
            try
            {
                cmd = "select Distinct eventID,eventTitle from view_SessionEventClassSectionSubject where sessionEventID = " + sessionEventID + "";
                var appMenu = dapperQuery.Qry<SessionEventSubjectClass>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getDateSheetView")]
        public IActionResult getDateSheetView(int departmentTypeID,int branch_department_section_id,string fromDate,string toDate)
        {
            try
            {
                cmd = @"DECLARE	@return_value int

                        EXEC	@return_value = [dbo].[sp_dateSheetView]
                                @departmentTypeID = " + departmentTypeID + @",
                                @branch_department_section_id = " + branch_department_section_id + @",
                                @fromDate = N'" + fromDate + @"',
                                @toDate = N'" + toDate + @"' ";
                var appMenu = dapperQuery.Qry<DateSheet>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getSessionEventClass")]
        public IActionResult getSessionEventClass(int sessionEventID,int subjectID)
        {
            try
            {
                cmd = "select Distinct classID,className from view_SessionEventClassSectionSubject where sessionEventID = " + sessionEventID + " and subjectID = " + subjectID + "";
                var appMenu = dapperQuery.Qry<Class>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getSessionEventSection")]
        public IActionResult getSessionEventSection(int sessionEventID,int subjectID,int classID)
        {
            try
            {
                cmd = "select Distinct sectionID,sectionName from view_SessionEventClassSectionSubject where sessionEventID = " + sessionEventID + " and subjectID = " + subjectID + " and classID = " + classID + "";
                var appMenu = dapperQuery.Qry<Section>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getSessionEvent")]
        public IActionResult getSessionEvent(int sessionEventID)
        {
            try
            {
                cmd = "select Distinct eventID,eventTitle from view_SessionEventClassSectionSubject where sessionEventID = " + sessionEventID + "";
                var appMenu = dapperQuery.Qry<EventTitle>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }


        [HttpGet("getClassSubjectStudents")]
        public IActionResult getClassSubjectStudents(int teacherID, int classID, int sectionID, int subjectID, int sessionEventID)
        {
            try
            {
                cmd = @"SELECT DISTINCT cs.classID,cs.sectionID,cd.branch_department_section_id,s.studentID, s.studentName,
                        s.studentRegistrationCode,med.totalMarks,mer.markableEventResultMarks,med.markableEventDetailID
                        FROM tbl_student AS s 
                        INNER JOIN tbl_student_class AS sc ON s.studentID = sc.studentID 
                        INNER JOIN tbl_class_section AS cs ON sc.classID = cs.classSectionID 
                        INNER JOIN tbl_class_department AS cd ON cs.classDepartmentID = cd.classDepartmentID 
                        INNER JOIN tbl_class_section_subjects AS css ON cs.classSectionID = css.classSectionID 
                        INNER JOIN tbl_teacher_class_section_subject AS tcss ON css.classSectionSubjectID = tcss.classSectionSubjectID 
                        INNER JOIN tbl_subjects AS sub ON css.subjectID = sub.subjectID 
                        INNER JOIN tbl_markable_event_detail AS med ON css.classSectionSubjectID = med.classSectionSubjectID 
                        Inner join tbl_session_events se on se.sessionEventID = med.sessionEventID
                        LEFT join tbl_markable_events_results AS mer on mer.markableEventDetailID = med.markableEventDetailID
                        and s.studentID = mer.studentID
                        WHERE s.isDeleted = 0 AND sc.isDeleted = 0 AND cs.isDeleted = 0 AND tcss.isDeleted = 0 AND cd.isDeleted = 0 AND css.isDeleted = 0 AND med.isDeleted = 0
                        AND tcss.teacherID = " + teacherID + " and cs.classID = " + classID + " AND cs.sectionID = " + sectionID + " AND css.subjectID = " + subjectID + " and se.sessionEventID =  " + sessionEventID + "";
    
                var appMenu = dapperQuery.Qry<ClassSubjectStudents>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }
        [HttpGet("getClassTeacherSubject")]
        public IActionResult getClassTeacherSubject(int teacherID, int classID, int sectionID)
        {
            try
            {
                cmd = @"select tcss.teacherID,s.subjectID,s.subjectTitle,c.classID,cs.sectionID,cd.branch_department_section_id from tbl_class_section_subjects as css inner join
                            tbl_teacher_class_section_subject as tcss on css.classSectionSubjectID = tcss.classSectionSubjectID inner join
                            tbl_subjects as s on css.subjectID = s.subjectID inner join 
                            tbl_class_section as cs on css.classSectionID = cs.classSectionID inner join
                            tbl_class_department as cd on cs.classDepartmentID = cd.classDepartmentID inner join
                            tbl_calss as c on cs.classID = c.classID inner join tbl_section as sec on cs.sectionID = sec.sectionID 
                        where tcss.teacherID = "+teacherID+" and cs.classID= "+classID+" and cs.sectionID= "+sectionID+"  and  tcss.isDeleted = 0 and css.isDeleted = 0 and s.isDeleted = 0 and cs.isDeleted = 0 and c.isDeleted = 0 and cd.isDeleted = 0";
                var appMenu = dapperQuery.Qry<ClassTeacherSubject>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getEvent")]
        public IActionResult getEvent(string date)
        {
            try
            {
                cmd = @"select med.markableEventDetailID,e.eventTitle from tbl_events as e inner join
                            tbl_session_events as se on e.eventID = se.eventID inner join
                            tbl_markable_event_detail as med on se.sessionEventID = med.sessionEventID
                        where e.isDeleted =  0 and med.isDeleted = 0 and '"+date+"' between med.markableEventDetailFromDate and med.markableEventDetailToDate";
                var appMenu = dapperQuery.Qry<MarkableEventTitle>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        
        [HttpPost("saveMarkableEventResult")]
        public IActionResult saveMarkableEventResult(MarkableEventResultCreation model){
            try
            {
               var response = dapperQuery.SPReturn("sp_markable_event_result", model , _dbCon);
               return Ok(response); 
            }
            catch (Exception e)
            {
                
                return Ok(e);
            }
        }
    }
}