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
    public class AttendanceController : ControllerBase
    {
        private readonly IOptions<conStr> _dbCon;
        private string cmd;

        public AttendanceController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        }

        [HttpGet("getLeaveType")]
        public IActionResult getLeaveType()
        {
            try
            {
                cmd = "SELECT * FROM tbl_leave_type where isDeleted=0";
                var appMenu = dapperQuery.Qry<LeaveType>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getParentChildren")]
        public IActionResult getParentChildren(int userID)
        {
            try
            {
                cmd = "SELECT * FROM view_parentChildren where userID = " + userID + "";
                var appMenu = dapperQuery.Qry<ParentChildren>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getAttendanceOfCurDate")]
        public IActionResult getAttendanceOfCurDate(int studentID,string attendanceDate)
        {
            try
            {
                cmd = "select studentID,attendanceID,attendanceDate,attendanceFlag from tbl_attendance where studentID = " + studentID + " and "+
                      " cast(attendanceDate as date) = cast('" + attendanceDate + "' as date)";
                var appMenu = dapperQuery.Qry<CurDateAttendance>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getAttendanceOfMonth")]
        public IActionResult getAttendanceOfMonth(int studentID,int year)
        {
            try
            {
                cmd = @"SELECT COUNT(DISTINCT a.attendanceDate) AS daysPresent,DATENAME(MONTH, a.attendanceDate) AS mName,MONTH(a.attendanceDate) AS [month], 
                        COUNT(DISTINCT CASE WHEN c.isWeakend = 0 THEN c.calendarDate END) AS totalWorkingDays, 
                        COUNT(DISTINCT CASE WHEN c.isWeakend = 1 THEN c.calendarDate END) AS officialLeaves FROM tbl_attendance a 
                        left outer JOIN dimcalendar c ON MONTH(a.attendanceDate) = MONTH(c.calendarDate) AND 
                        YEAR(a.attendanceDate) = YEAR(c.calendarDate) WHERE a.studentID = " + studentID + @" AND a.attendanceFlag = 'P' and YEAR(a.attendanceDate) = " + year + @"
                        GROUP BY DATENAME(MONTH, a.attendanceDate),MONTH(a.attendanceDate) ORDER BY DATENAME(MONTH, a.attendanceDate) DESC";
                var appMenu = dapperQuery.Qry<AttendanceOfMonth>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }


        [HttpGet("getAttendanceOfMonthDetail")]
        public IActionResult getAttendanceOfMonthDetail(int studentID,int year,int month)
        {
            try
            {
                cmd = @"SELECT c.calendarDate, CASE WHEN c.isWeakend = 1 THEN 'OP' ELSE a.attendanceFlag END AS attendanceFlag,
                        DATENAME(MONTH, c.calendarDate) AS mName,    
                        COUNT(DISTINCT CASE WHEN c.isWeakend = 1 THEN c.calendarDate END) AS officialLeaves FROM dimcalendar c 
                        LEFT OUTER JOIN tbl_attendance a ON c.calendarDate = a.attendanceDate AND a.studentID = " + studentID + @"
                        AND a.attendanceFlag IN ('P', 'A', 'L') -- select only 'P', 'A', or 'L' attendanceFlag
                        WHERE YEAR(c.calendarDate) = " + year + @" AND MONTH(c.calendarDate) = " + month + @"
                        GROUP BY c.calendarDate, c.isWeakend, CASE WHEN c.isWeakend = 1 THEN 'OP' ELSE a.attendanceFlag END, DATENAME(MONTH, a.attendanceDate) ";
                var appMenu = dapperQuery.Qry<AttendanceMonthDetail>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getAttendanceDetail")]
        public IActionResult getAttendanceDetail(int sectionID,int classID,int departmentTypeID,int branch_department_section_id,string attendanceDate)
        {
            try
            {
                cmd = "SELECT * FROM fun_attendanceView("+sectionID+","+classID+","+departmentTypeID+","+branch_department_section_id+",'"+attendanceDate+"')";
                var appMenu = dapperQuery.Qry<AttendanceDetail>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }
        [HttpGet("getAttendanceReport")]
        public IActionResult getAttendanceReport(string attendanceDate,int branch_department_section_id,int departmentTypeID,int classID, int sectionID)
        {
            try
            {
                if(attendanceDate != "" && branch_department_section_id == 0 && departmentTypeID == 0 && classID == 0 && sectionID == 0){
                    cmd = "SELECT * FROM view_attendanceReport where attendanceDate = '"+attendanceDate+"'";
                }else if(attendanceDate != "" && branch_department_section_id != 0 && departmentTypeID == 0 && classID == 0 && sectionID == 0)
                {
                     cmd = "SELECT * FROM view_attendanceReport where attendanceDate = '"+attendanceDate+"' and branch_department_section_id = "+branch_department_section_id+"";
                }
                else if(attendanceDate != "" && branch_department_section_id != 0 && departmentTypeID != 0 && classID == 0 && sectionID == 0)
                {
                   cmd = "SELECT * FROM view_attendanceReport where attendanceDate = '"+attendanceDate+"' and branch_department_section_id = "+branch_department_section_id+" and departmentTypeID = "+departmentTypeID+"";
                }
                else if(attendanceDate != "" && branch_department_section_id != 0 && departmentTypeID != 0 && classID != 0  && sectionID == 0)
                {
                   cmd = "SELECT * FROM view_attendanceReport where attendanceDate = '"+attendanceDate+"' and branch_department_section_id = "+branch_department_section_id+" and departmentTypeID = "+departmentTypeID+" and classID = "+classID+"";
                }
                else if(attendanceDate != "" && branch_department_section_id != 0 && departmentTypeID != 0 && classID != 0 && sectionID != 0)
                {
                   cmd = "SELECT * FROM view_attendanceReport where attendanceDate = '"+attendanceDate+"' and branch_department_section_id = "+branch_department_section_id+" and departmentTypeID = "+departmentTypeID+" and classID = "+classID+" and sectionID  = "+sectionID+"";
                }
                else
                {
                cmd = "SELECT * FROM view_attendanceReport";

                }
                var appMenu = dapperQuery.Qry<AttendanceReport>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }
        

        [HttpGet("getMobileDashboardClass")]
        public IActionResult getMobileDashboardClass(int teacherID,string attendanceDate)
        {
            try
            {
                cmd = "select * from fun_mobileDashboardClass("+teacherID+",'"+attendanceDate+"')";
                var appMenu = dapperQuery.Qry<ClassAttendance>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getStudentLeave")]
        public IActionResult getStudentLeave(int studentID)
        {
            try
            {
                cmd = "SELECT * FROM tbl_student_leave where studentID = "+studentID+" and  isDeleted=0";
                var appMenu = dapperQuery.Qry<LeaveType>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getStudentAttendance")]
        public IActionResult getStudentAttendance(int branch_department_section_id,int classID,int sectionID,int departmentTypeID)
        {
            try
            {
                cmd = "SELECT * from view_studentAttendance where branch_department_section_id="+branch_department_section_id+" and classID = "+classID+" and sectionID = "+sectionID+" and departmentTypeID = "+departmentTypeID+" and isDeleted =0";
                var appMenu = dapperQuery.Qry<AllStudent>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("saveLeave")]
        public IActionResult saveLeave(LeaveCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_leave",model,_dbCon);
                
                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("saveAttendance")]
        public IActionResult saveAttendance(AttendanceCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_Attendance",model,_dbCon);
                
                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }       
    }
}