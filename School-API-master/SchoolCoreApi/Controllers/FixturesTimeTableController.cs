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
    public class FixturesTimeTableController : ControllerBase
    {
        private readonly IOptions<conStr> _dbCon;
        private string cmd;

        public FixturesTimeTableController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        }

        [HttpGet("getCalendarDays")]
        public IActionResult getCalendarDays()
        {
            try
            {
                cmd = @"select Distinct calendarDayName,calendarDayOfWeek as calenderDay 
                        from tbl_time_slot ts 
                        inner join tbl_time_slot_detail tsd on tsd.timeSlotID = ts.timeSlotID
                        inner join dimcalendar d on d.calendarDayOfWeek = tsd.calenderDay
                        where d.isWeakend != 1 order by calendarDayOfWeek asc";
                var appMenu = dapperQuery.Qry<CalendarDay>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getDaySlots")]
        public IActionResult getDaySlots(int calendarDay,int timeSlotID)
        {
            try
            {
                cmd = @"select Distinct calendarDaySlotNo as slotNo,convert(varchar(20),PARSE(timeSlotFromTime as time),100) as timeSlotFromTime,convert(varchar(20),PARSE(timeSlotToTime as time),100) as timeSlotToTime 
                        from tbl_time_slot_detail 
                        where calenderDay = " + calendarDay + " and timeSlotID = " + timeSlotID + " order by calendarDaySlotNo asc";
                var appMenu = dapperQuery.Qry<SlotNoList>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getTeacherOfBranch")]
        public IActionResult getTeacherOfBranch(int branch_department_section_id,int departmentTypeID)
        {
            try
            {
                
                cmd = @"select distinct teacherID,teacherName from view_fixtureTeacherClassSubject
                        where branch_department_section_id = " + branch_department_section_id + @"
                        and departmentTypeID = " + departmentTypeID + @" ";
                
                var appMenu = dapperQuery.Qry<TeacherInfo>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getSubjectOfTeacher")]
        public IActionResult getSubjectOfTeacher(int teacherID)
        {
            try
            {
                
                cmd = @"select distinct subjectID,subjectTitle from view_fixtureTeacherClassSubject
                        where teacherID = " + teacherID + @" ";
                
                var appMenu = dapperQuery.Qry<Subjects>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getClassTeacherSubject")]
        public IActionResult getClassTeacherSubject(int teacherID,int subjectID)
        {
            try
            {
                
                cmd = @"select distinct classID,className from view_fixtureTeacherClassSubject
                        where teacherID = " + teacherID + @" 
                        and subjectID = " + subjectID + " ";
                
                var appMenu = dapperQuery.Qry<Class>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }


        [HttpGet("getFixturesTeacherClassSubject")]
        public IActionResult getFixturesTeacherClassSubject(int branch_department_section_id,int departmentTypeID,int teacherID, int subjectID)
        {
            try
            {
                if (teacherID == 0)
                {
                    cmd = @"select distinct * from view_fixtureTeacherClassSubject
                        where branch_department_section_id = " + branch_department_section_id + @"
                        and departmentTypeID = " + departmentTypeID + @"
                        and subjectID = " + subjectID + " ";
                }
                else
                {
                    cmd = @"select distinct * from view_fixtureTeacherClassSubject
                        where branch_department_section_id = " + branch_department_section_id + @"
                        and departmentTypeID = " + departmentTypeID + @"
                        and subjectID = " + subjectID + " and teacherID = " + teacherID + "";   
                }
                
                var appMenu = dapperQuery.Qry<SubjectTeacherClassSection>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }
        
        [HttpPost("saveFixturesTimeTable")]
        public IActionResult saveFixturesTimeTable(FixturesTimeTableCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_fixturesTimeTable",model,_dbCon);
                
                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }

    }
}