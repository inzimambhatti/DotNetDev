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
    public class EventDashboardController : ControllerBase
    {
        private readonly IOptions<conStr> _dbCon;
        private string cmd;

        public EventDashboardController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        }

        [HttpGet("getExamType")]
        public IActionResult getExamType()
        {
            try
            {
                cmd = @"select eventID,eventTitle from tbl_events where eventNatureID = 1 and isDeleted = 0";
                
                var appMenu = dapperQuery.Qry<ExamType>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getUpcomingEvents")]
        public IActionResult getUpcomingEvents()
        {
            try
            {
                
                cmd = @"select se.sessionEventID,CONVERT(varchar(20),cast(se.sessionEventStartDateTime as time), 100) as sessionEventStartDateTime,
                        CONVERT(varchar(20),cast(se.sessionEventEndDateTime as time), 100) as sessionEventEndDateTime,
                            se.sessionEventStartDate,se.eventID,e.eventTitle,DATENAME(MONTH,se.sessionEventStartDate) as [month]
                        from tbl_session_events se 
                            LEFT Join tbl_markable_event_detail med on med.sessionEventID = se.sessionEventID
                            LEFT join tbl_class_section_subjects css on css.classSectionSubjectID = med.classSectionSubjectID
                            Inner Join tbl_events e on e.eventID = se.eventID
                            Inner join tbl_event_type et on et.eventTypeID = e.eventTypeID
                        where se.isDeleted = 0 and e.isDeleted = 0
                            and et.isMarkable = 0
                            and cast(se.sessionEventStartDate as date) >= CAST(GETDATE() as date)";
                
                var appMenu = dapperQuery.Qry<UpcomingEvents>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getTaskAssigned")]
        public IActionResult getTaskAssigned(int branchID,string fromDate)
        {
            try
            {
                if (branchID == 0)
                {
                    cmd = @"select DISTINCT t.teacherID,t.teacherFirstName,t.teacherLastName,med.markableEventDetailFromDate,
                            cd.branch_department_section_id,se.eventID,e.eventTitle,
                            case when (select top 1 mer.markableEventResultID from tbl_markable_events_results mer
                            where mer.markableEventDetailID = med.markableEventDetailID and mer.markableEventResultMarks > 0) > 0
                            then 'Complete' else 'Pending' end as [status]
                        from tbl_session_events se 
                            INNER Join tbl_markable_event_detail med on med.sessionEventID = se.sessionEventID
                            INNER join tbl_class_section_subjects css on css.classSectionSubjectID = med.classSectionSubjectID
                            Inner Join tbl_class_section cs on cs.classSectionID = css.classSectionID
                            Inner Join tbl_teacher_class_section_subject tcss on tcss.classSectionSubjectID = css.classSectionSubjectID
                            Inner Join tbl_class_department cd on cd.classDepartmentID = cs.classDepartmentID
                            Inner Join tbl_teacher t on t.teacherID = tcss.teacherID
                            Inner Join tbl_events e on e.eventID = se.eventID
                            Inner join tbl_event_type et on et.eventTypeID = e.eventTypeID
                        where se.isDeleted = 0 and e.isDeleted = 0 and et.isMarkable = 1
                            and cast(se.sessionEventStartDate as date) >= CAST('" + fromDate + @"' as date)";
                }
                else
                {
                    cmd = @"select DISTINCT t.teacherID,t.teacherFirstName,t.teacherLastName,med.markableEventDetailFromDate,
                            cd.branch_department_section_id,se.eventID,e.eventTitle,
                            case when (select top 1 mer.markableEventResultID from tbl_markable_events_results mer
                            where mer.markableEventDetailID = med.markableEventDetailID and mer.markableEventResultMarks > 0) > 0
                            then 'Complete' else 'Pending' end as [status]
                        from tbl_session_events se 
                            INNER Join tbl_markable_event_detail med on med.sessionEventID = se.sessionEventID
                            INNER join tbl_class_section_subjects css on css.classSectionSubjectID = med.classSectionSubjectID
                            Inner Join tbl_class_section cs on cs.classSectionID = css.classSectionID
                            Inner Join tbl_teacher_class_section_subject tcss on tcss.classSectionSubjectID = css.classSectionSubjectID
                            Inner Join tbl_class_department cd on cd.classDepartmentID = cs.classDepartmentID
                            Inner Join tbl_teacher t on t.teacherID = tcss.teacherID
                            Inner Join tbl_events e on e.eventID = se.eventID
                            Inner join tbl_event_type et on et.eventTypeID = e.eventTypeID
                        where se.isDeleted = 0 and e.isDeleted = 0 and et.isMarkable = 1
                            and cast(se.sessionEventStartDate as date) >= CAST('" + fromDate + @"' as date)
                            and cd.branch_department_section_id = " + branchID + @"";
                }
                
                var appMenu = dapperQuery.Qry<TaskAssigned>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getHomeWork")]
        public IActionResult getHomeWork(int branchID,string fromDate)
        {
            try
            {
                cmd = @"select DISTINCT cs.classID,c.className,cd.branch_department_section_id,
                            (select COUNT(DISTINCT subcss.subjectID) from tbl_class_department subcd
                            inner join tbl_class_section subcs on subcs.classDepartmentID = subcd.classDepartmentID
                            inner join tbl_class_section_subjects subcss on subcss.classSectionID = subcs.classSectionID
                            where subcd.branch_department_section_id = cd.branch_department_section_id and
                            subcs.classID = cs.classID) as totalSubject,
                            (select COUNT(DISTINCT subcss.subjectID) from tbl_session_events subse
                            inner join tbl_markable_event_detail submed on submed.sessionEventID = subse.sessionEventID
                            inner join tbl_class_section_subjects subcss on subcss.classSectionSubjectID = submed.classSectionSubjectID
                            inner join tbl_class_section subcs on subcs.classSectionID = subcss.classSectionID
                            inner join tbl_class_department subcd on subcd.classDepartmentID = subcs.classDepartmentID
                            where subse.isDeleted = 0 and submed.isDeleted = 0 and subcd.branch_department_section_id = cd.branch_department_section_id and
                            subcs.classID = cs.classID and 
                            cast(submed.markableEventDetailFromDate as date) = cast('" + fromDate + @"' as date)
                            and subse.eventID = 33
                            ) as effectiveSubject 
                        from 
                            tbl_class_section cs 
                            Inner Join tbl_class_department cd on cd.classDepartmentID = cs.classDepartmentID
                            Inner Join tbl_calss c on c.classID = cs.classID
                        where cd.branch_department_section_id = " + branchID + @" and cs.isDeleted = 0
                        and cd.isDeleted = 0
                        ORDER BY cs.classID";
                
                var appMenu = dapperQuery.Qry<DashboardHomeWork>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getClassHomeWork")]
        public IActionResult getClassHomeWork(int branchID,string fromDate,int classID)
        {
            try
            {
                cmd = @"select DISTINCT t.teacherID,t.teacherFirstName,cs.classID,c.className,cd.branch_department_section_id,
                        css.subjectID,sub.subjectTitle,cs.sectionID,sec.sectionName,
                            (select COUNT(DISTINCT subcss.subjectID) from tbl_session_events subse
                            inner join tbl_markable_event_detail submed on submed.sessionEventID = subse.sessionEventID
                            inner join tbl_class_section_subjects subcss on subcss.classSectionSubjectID = submed.classSectionSubjectID
                            inner join tbl_class_section subcs on subcs.classSectionID = subcss.classSectionID
                            inner join tbl_class_department subcd on subcd.classDepartmentID = subcs.classDepartmentID
                            where subse.isDeleted = 0 and submed.isDeleted = 0 and subcd.branch_department_section_id = cd.branch_department_section_id and
                            subcs.classID = cs.classID and subcs.sectionID = cs.sectionID and
                            cast(submed.markableEventDetailFromDate as date) = cast('" + fromDate + @"' as date)
                            and subcss.subjectID = css.subjectID and subse.eventID = 33
                            ) as effectiveSubject 
                        from 
                            tbl_class_section cs 
                            Inner Join tbl_class_department cd on cd.classDepartmentID = cs.classDepartmentID
                            Inner join tbl_class_section_subjects css on css.classSectionID = cs.classSectionID
                            Inner Join tbl_teacher_class_section_subject tcss on tcss.classSectionSubjectID = css.classSectionSubjectID
                            Inner join tbl_teacher t on t.teacherID = tcss.teacherID
                            Inner Join tbl_calss c on c.classID = cs.classID
                            Inner Join tbl_subjects sub on sub.subjectID = css.subjectID
                            Inner Join tbl_section sec on sec.sectionID = cs.sectionID
                        where cd.branch_department_section_id = " + branchID + @" and cs.isDeleted = 0
                        and cs.classID = " + classID + @"
                        and cd.isDeleted = 0
                        ORDER BY cs.classID";
                
                var appMenu = dapperQuery.Qry<classDashboardHomeWork>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getBottomHomeWork")]
        public IActionResult getBottomHomeWork(int branchID,string fromDate)
        {
            try
            {
                if (branchID == 0)
                {
                    cmd = @"select DISTINCT TOP 10 cs.classID,CONCAT(c.className,' (',sec.sectionName,')') as className, css.subjectID,sub.subjectTitle,
                            (select COUNT(DISTINCT subcss.subjectID) from tbl_session_events subse inner join tbl_markable_event_detail submed on submed.sessionEventID = subse.sessionEventID
                            inner join tbl_class_section_subjects subcss on subcss.classSectionSubjectID = submed.classSectionSubjectID
                            inner join tbl_class_section subcs on subcs.classSectionID = subcss.classSectionID inner join tbl_class_department subcd on subcd.classDepartmentID = subcs.classDepartmentID
                            where subse.isDeleted = 0 and submed.isDeleted = 0 and subcd.branch_department_section_id = cd.branch_department_section_id and
                            subcs.classID = cs.classID and subcs.sectionID = cs.sectionID and MONTH(submed.markableEventDetailFromDate) = MONTH('" + fromDate + @"') and year(submed.markableEventDetailFromDate) = year('" + fromDate + @"') and subcss.subjectID = css.subjectID and subse.eventID = 33 ) as effectiveSubject 
                        from 
                            tbl_class_section cs Inner Join tbl_class_department cd on cd.classDepartmentID = cs.classDepartmentID
                            Inner join tbl_class_section_subjects css on css.classSectionID = cs.classSectionID Inner Join tbl_calss c on c.classID = cs.classID Inner Join tbl_subjects sub on sub.subjectID = css.subjectID
                            Inner Join tbl_section sec on sec.sectionID = cs.sectionID where 
                        --cd.branch_department_section_id = 1 and 
                        cs.isDeleted = 0 and cd.isDeleted = 0 ORDER BY effectiveSubject ASC";    
                }
                else
                {
                    cmd = @"select DISTINCT TOP 10 cs.classID,CONCAT(c.className,' (',sec.sectionName,')') as className, css.subjectID,sub.subjectTitle,
                            (select COUNT(DISTINCT subcss.subjectID) from tbl_session_events subse inner join tbl_markable_event_detail submed on submed.sessionEventID = subse.sessionEventID
                            inner join tbl_class_section_subjects subcss on subcss.classSectionSubjectID = submed.classSectionSubjectID
                            inner join tbl_class_section subcs on subcs.classSectionID = subcss.classSectionID inner join tbl_class_department subcd on subcd.classDepartmentID = subcs.classDepartmentID
                            where subse.isDeleted = 0 and submed.isDeleted = 0 and subcd.branch_department_section_id = cd.branch_department_section_id and
                            subcs.classID = cs.classID and subcs.sectionID = cs.sectionID and MONTH(submed.markableEventDetailFromDate) = MONTH('" + fromDate + @"') and year(submed.markableEventDetailFromDate) = year('" + fromDate + @"') and subcss.subjectID = css.subjectID and subse.eventID = 33 ) as effectiveSubject 
                        from 
                            tbl_class_section cs Inner Join tbl_class_department cd on cd.classDepartmentID = cs.classDepartmentID
                            Inner join tbl_class_section_subjects css on css.classSectionID = cs.classSectionID Inner Join tbl_calss c on c.classID = cs.classID Inner Join tbl_subjects sub on sub.subjectID = css.subjectID
                            Inner Join tbl_section sec on sec.sectionID = cs.sectionID where 
                        cd.branch_department_section_id = " + branchID + @" and 
                        cs.isDeleted = 0 and cd.isDeleted = 0 ORDER BY effectiveSubject ASC";
                }
                
                var appMenu = dapperQuery.Qry<BottomHomeWork>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getClassExamMarks")]
        public IActionResult getClassExamMarks(int branchID,int eventID)
        {
            try
            {
                cmd = @"select * from fun_classExamMarks(" + eventID + "," + branchID + ")";    
                var appMenu = dapperQuery.Qry<ClassExamMarks>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getClassSubjectExamMarks")]
        public IActionResult getClassSubjectExamMarks(int branchID,int eventID,int classID)
        {
            try
            {
                cmd = @"select * from fun_classSubjectExamMarks(" + eventID + "," + branchID + "," + classID + ")";    
                var appMenu = dapperQuery.Qry<ClassSubjectExamMarks>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

    }
}