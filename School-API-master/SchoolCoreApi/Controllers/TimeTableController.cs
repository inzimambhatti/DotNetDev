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
    public class TimeTableController : ControllerBase
    {
        private readonly IOptions<conStr> _dbCon;
        private string cmd;

        public TimeTableController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        }

        [HttpGet("getBranchTimeTableDate")]
        public IActionResult getBranchTimeTableDate(int branch_department_section_id,int departmentTypeID)
        {
            try
            {
                cmd = @"select * from fun_branchTimeTableDates(" + branch_department_section_id + "," + departmentTypeID + ")";
                var appMenu = dapperQuery.Qry<BranchTimeTableDate>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getCalendarDaySlotTeacher")]
        public IActionResult getCalendarDaySlotTeacher(int calendarDaySlotNo,int calenderDay,int branch_department_section_id,int subjectID,int classID)
        {
            try
            {
                cmd = @"select Distinct tsd.timeSlotDetailID,t.teacherID,t.teacherFirstName+ ' ' + t.teacherLastName as teacherName 
                        from tbl_time_slot ts inner join tbl_time_slot_detail tsd on tsd.timeSlotID = ts.timeSlotID
                        inner join tbl_time_table_class_section_subject ttcss on ttcss.timeSlotDetailID = tsd.timeSlotDetailID
                        inner join tbl_teacher_class_section_subject tcss on tcss.teacherClassSectionSubjectID = ttcss.teacherClassSectionSubjectID
                        inner join tbl_class_section_subjects css on css.classSectionSubjectID = tcss.classSectionSubjectID
                        inner join tbl_class_section cs on cs.classSectionID = tsd.classSectionID inner join tbl_class_department cd on cd.classDepartmentID = cs.classDepartmentID
                        inner join tbl_teacher t on t.teacherID = tcss.teacherID inner join tbl_section sec on sec.sectionID = cs.sectionID
                        where ts.isDeleted = 0 and tsd.isDeleted = 0 and cs.isDeleted = 0 and cd.isDeleted = 0 and ttcss.isDeleted = 0 and tcss.isDeleted = 0 and css.isDeleted = 0 and t.isDeleted = 0
                        and tsd.calendarDaySlotNo = " + calendarDaySlotNo + " and tsd.calenderDay = " + calenderDay + " and cd.branch_department_section_id = " + branch_department_section_id + " and css.subjectID = " + subjectID + " and cs.classID = " + classID + " ";
                var appMenu = dapperQuery.Qry<DaySlotTeacher>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getTimeTableClasses")]
        public IActionResult getTimeTableClasses(int branch_department_section_id,int departmentTypeID,int timeSlotID)
        {
            try
            {
                cmd = @"select distinct cs.classID,c.className from tbl_time_slot_detail tsd
                        inner join tbl_class_section cs on cs.classSectionID = tsd.classSectionID
                        inner join tbl_class_department cd on cd.classDepartmentID = cs.classDepartmentID
                        inner join tbl_calss c on c.classID = cs.classID
                        where tsd.isDeleted = 0 and cs.isDeleted = 0 and cd.isDeleted = 0
                        and cd.branch_department_section_id = " + branch_department_section_id + @" and cd.departmentTypeID = " + departmentTypeID + " and tsd.timeSlotID = " + timeSlotID + " order by cs.classID asc";
                var appMenu = dapperQuery.Qry<TimeTableClasses>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }
        
        [HttpGet("getSessionCalenderDays")]
        public IActionResult getSessionCalenderDays(string fromDate, string toDate)
        {
            try
            {
                cmd = @"select distinct d.calendarDayOfWeek as calenderDay,d.calendarDayName,RIGHT(CONVERT(varchar(20), CAST(d.calenderDayStartTime AS time), 100), 7) as calenderDayStartTime,
                        RIGHT(CONVERT(varchar(20), CAST(d.calenderDayEndTime AS time), 100), 7) as calenderDayEndTime,d.isHalfDay
                        from [dbo].[dimcalendar] d
                        Inner join tbl_school_session ss on ss.schoolSessionID = d.schoolSessionID
                        where cast(calendarDate as date) >= CAST('" + fromDate + @"' as date)
                        and cast(calendarDate as date) <= CAST('" + toDate + @"' as date) and (isWeakend != 1 or isWeakend is null)
                        and (uncertainEventFlag = 0 OR uncertainEventFlag is null)";
                var appMenu = dapperQuery.Qry<SchoolCalenderDays>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getTimeTableClassSectionSubject")]
        public IActionResult getTimeTableClassSectionSubject(int branch_department_section_id,int departmentTypeID,int classID,int subjectID, string fromDate,string toDate)
        {
            try
            {
                cmd = @"DECLARE	@return_value int
                        EXEC	@return_value = [dbo].[sp_TimeTableClassSectionSubject]
                                @branch_department_section_id = " + branch_department_section_id + @",
                                @departmentTypeID = " + departmentTypeID + @" ,
                                @classID = " + classID + @",
                                @subjectID = " + subjectID + @", 
                                @fromDate = N'" + fromDate + @"',
                                @toDate = N'" + toDate + @"' ";
                var appMenu = dapperQuery.Qry<TimeTableClassSectionSubject>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }


        [HttpGet("getTeacherTimeTable")]
        public IActionResult getTeacherTimeTable(int teacherID,string fromToDate)
        {
            try
            {
                cmd = @"DECLARE	@return_value int

                        EXEC	@return_value = [dbo].[sp_teacherTimeTableView]
                                @teacherID = " + teacherID + @",
		                        @fromToDate = N'" + fromToDate + @"' ";
                var appMenu = dapperQuery.Qry<TeacherTimeTable>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getStudentTimeTable")]
        public IActionResult getStudentTimeTable(int studentID,string fromToDate)
        {
            try
            {
                cmd = @"DECLARE	@return_value int

                        EXEC	@return_value = [dbo].[sp_studentTimeTableView]
                                @studentID = " + studentID + @",
		                        @fromToDate = N'" + fromToDate + @"' ";
                var appMenu = dapperQuery.Qry<TeacherTimeTable>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getFixturesTimeTable")]
        public IActionResult getFixturesTimeTable(int branch_department_section_id,int departmentTypeID,int classID,string timeTableDate)
        {
            try
            {
                cmd = @"DECLARE	@return_value int

                            EXEC	@return_value = [dbo].[sp_FixturesTimeTableView]
                                    @branch_department_section_id = " + branch_department_section_id + @",
                                    @departmentTypeID = " + departmentTypeID + @",
                                    @classID = " + classID + @",
                                    @timeTableDate = N'" + timeTableDate + @"'";
                var appMenu = dapperQuery.Qry<FixturesTimeTable>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getClassTimeTable")]
        public IActionResult getClassTimeTable(int branch_department_section_id,int departmentTypeID,int classID,int sectionID,string fromToDate)
        {
            try
            {
                cmd = @"DECLARE	@return_value int

                        EXEC	@return_value = [dbo].[sp_ClassTimeTableView]
                                @branch_department_section_id = " + branch_department_section_id + @",
                                @departmentTypeID = " + departmentTypeID + @",
                                @classID = " + classID + @",
                                @sectionID = " + sectionID + @",
                                @fromToDate = N'" + fromToDate + @"'";
                var appMenu = dapperQuery.Qry<ClassTimeTable>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getSlotList")]
        public IActionResult getSlotList()
        {
            try
            {
                cmd = @"select ROW_NUMBER() over(order by timeSlotTypeID) as slotNo,* from view_slotList";
                var appMenu = dapperQuery.Qry<SlotNoList>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }


        [HttpGet("getTimeTableConfiguration")]
        public IActionResult getTimeTableConfiguration()
        {
            try
            {
                cmd = @"select ts.timeSlotID,ts.timeSlotTitle,ss.schoolSessionID,ss.schoolSessionTitle,ss.schoolSessionYear from tbl_time_slot ts
                    Inner join tbl_school_session ss on ss.schoolSessionID = ts.schoolSessionID
                    where ss.isDeleted = 0 and ts.isDeleted = 0";
                var appMenu = dapperQuery.Qry<TimeTableConfiguration>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getTimeTableConfigByID")]
        public IActionResult getTimeTableConfigByID(int timeSlotID)
        {
            try
            {
                cmd = @"select * from view_timeTableConfig where timeSlotID = " + timeSlotID + "";
                var appMenu = dapperQuery.Qry<TimeTableConfigDetail>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("saveTimeTableConfig")]
        public IActionResult saveTimeTableConfig(TimeTableConfigCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_TimeTableConfig",model,_dbCon);
                
                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("saveTimeTable")]
        public IActionResult saveTimeTable(TimeTableCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_timeTable",model,_dbCon);
                
                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("saveSwapSlots")]
        public IActionResult saveSwapSlots(SwapSlotCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_swapTeacherTimeTable",model,_dbCon);
                
                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }
    }
}