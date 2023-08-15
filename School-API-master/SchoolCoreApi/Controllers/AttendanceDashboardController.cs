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
    public class AttendanceDashboardController : ControllerBase
    {
        private readonly IOptions<conStr> _dbCon;
        private string cmd;

        public AttendanceDashboardController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        }

        [HttpGet("getBranchTotalStudent")]
        public IActionResult getBranchTotalStudent(int branch_department_section_id)
        {
            try
            {
                if (branch_department_section_id == 0)
                {
                    cmd = @"SELECT COUNT(s.studentRegistrationCode) AS totalNoOfStudents,Count(DiSTINCt cs.classID) as classCount
                        FROM dbo.tbl_class_department cd 
                        INNER JOIN dbo.tbl_class_section cs ON cd.classDepartmentID = cs.classDepartmentID 
                        INNER JOIN dbo.tbl_student_registration sr ON cs.classSectionID = sr.classSectionID 
                        INNER JOIN  dbo.tbl_student s ON sr.studentRegistrationID = s.studentRegistrationID 
                        WHERE (s.isDeleted = 0) ";
                }
                else
                {
                    cmd = @"SELECT COUNT(s.studentRegistrationCode) AS totalNoOfStudents,Count(DiSTINCt cs.classID) as classCount
                        FROM dbo.tbl_class_department cd 
                        INNER JOIN dbo.tbl_class_section cs ON cd.classDepartmentID = cs.classDepartmentID 
                        INNER JOIN dbo.tbl_student_registration sr ON cs.classSectionID = sr.classSectionID 
                        INNER JOIN  dbo.tbl_student s ON sr.studentRegistrationID = s.studentRegistrationID 
                        WHERE (s.isDeleted = 0) AND (cd.branch_department_section_id = " + branch_department_section_id + ")";   
                }
                
                var appMenu = dapperQuery.Qry<TotalStudents>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getOneWeekAttendance")]
        public IActionResult getOneWeekAttendance(int branch_department_section_id,string attendanceDate)
        {
            try
            {
                if (branch_department_section_id == 0)
                {
                    cmd = @"SELECT 
                        SUM(CASE WHEN a.attendanceFlag = 'P' THEN 1 ELSE 0 END) AS presentStudents,
                        SUM(CASE WHEN a.attendanceFlag = 'A' THEN 1 ELSE 0 END) AS absentStudents,
                        SUM(CASE WHEN a.attendanceFlag = 'L' THEN 1 ELSE 0 END) AS onLeaveStudents,
                        DATENAME(WEEKDAY, a.attendanceDate) AS dayName
                        FROM dbo.tbl_class_department cd
                        INNER JOIN dbo.tbl_class_section cs ON cd.classDepartmentID = cs.classDepartmentID 
                        INNER JOIN dbo.tbl_student_registration sr ON cs.classSectionID = sr.classSectionID 
                        INNER JOIN dbo.tbl_student s ON sr.studentRegistrationID = s.studentRegistrationID 
                        INNER JOIN dbo.tbl_attendance AS a ON s.studentID = a.studentID 
                        WHERE  
                        a.attendanceDate >= DATEADD(DAY, -6, CAST('" + attendanceDate + @"' AS DATE)) AND
                        a.attendanceDate <= CAST('" + attendanceDate + @"' AS DATE) 
                        GROUP BY DATENAME(WEEKDAY, a.attendanceDate)
                        ORDER BY MIN(a.attendanceDate)";
                }
                else
                {
                    cmd = @"SELECT 
                        SUM(CASE WHEN a.attendanceFlag = 'P' THEN 1 ELSE 0 END) AS presentStudents,
                        SUM(CASE WHEN a.attendanceFlag = 'A' THEN 1 ELSE 0 END) AS absentStudents,
                        SUM(CASE WHEN a.attendanceFlag = 'L' THEN 1 ELSE 0 END) AS onLeaveStudents,
                        DATENAME(WEEKDAY, a.attendanceDate) AS dayName
                        FROM dbo.tbl_class_department cd
                        INNER JOIN dbo.tbl_class_section cs ON cd.classDepartmentID = cs.classDepartmentID 
                        INNER JOIN dbo.tbl_student_registration sr ON cs.classSectionID = sr.classSectionID 
                        INNER JOIN dbo.tbl_student s ON sr.studentRegistrationID = s.studentRegistrationID 
                        INNER JOIN dbo.tbl_attendance AS a ON s.studentID = a.studentID 
                        WHERE cd.branch_department_section_id = " + branch_department_section_id + @" AND 
                        a.attendanceDate >= DATEADD(DAY, -6, CAST('" + attendanceDate + @"' AS DATE)) AND
                        a.attendanceDate <= CAST('" + attendanceDate + @"' AS DATE) 
                        GROUP BY DATENAME(WEEKDAY, a.attendanceDate)
                        ORDER BY MIN(a.attendanceDate)";
                }
                var appMenu = dapperQuery.Qry<OneWeekAttendance>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getDrillDownAttendance")]
        public IActionResult getDrillDownAttendance(int branch_department_section_id,string attendanceDate)
        {
            try
            {
                if (branch_department_section_id == 0)
                {
                    cmd = @"SELECT COUNT(DISTINCT s.studentRegistrationCode) AS totalClassStudent,
                            COALESCE(SUM(CASE WHEN a.attendanceFlag = 'P' THEN 1 ELSE 0 END), 0) AS presentClassStudents,
                            COALESCE(SUM(CASE WHEN a.attendanceFlag = 'A' THEN 1 ELSE 0 END), 0) AS absentClassStudents,
                            COALESCE(SUM(CASE WHEN a.attendanceFlag = 'L' THEN 1 ELSE 0 END), 0) AS onLeaveClassStudents,
                            c.classID,c.className,
                            (SELECT COUNT(DISTINCT subs.studentRegistrationCode) AS totalSectionStudent,
                            COALESCE(SUM(CASE WHEN suba.attendanceFlag = 'P' THEN 1 ELSE 0 END), 0) AS presentSectionStudents,
                            COALESCE(SUM(CASE WHEN suba.attendanceFlag = 'A' THEN 1 ELSE 0 END), 0) AS absentSectionStudents,
                            COALESCE(SUM(CASE WHEN suba.attendanceFlag = 'L' THEN 1 ELSE 0 END), 0) AS onLeaveSectionStudents,
                            subsec.sectionID,subsec.sectionName FROM dbo.tbl_class_department subcd
                            INNER JOIN dbo.tbl_class_section subcs ON subcd.classDepartmentID = subcs.classDepartmentID 
                            INNER JOIN dbo.tbl_student_registration subsr ON subcs.classSectionID = subsr.classSectionID 
                            INNER JOIN dbo.tbl_student subs ON subsr.studentRegistrationID = subs.studentRegistrationID 
                            LEFT OUTER JOIN dbo.tbl_attendance AS suba ON subs.studentID = suba.studentID AND 
                            cast(suba.attendanceDate as date) = cast(getdate() as date)
                            INNER JOIN tbl_section subsec ON subsec.sectionID = subcs.sectionID  
                            WHERE subcs.classID = cs.classID
                            GROUP BY subsec.sectionID, subsec.sectionName FOR JSON AUTO) AS sectionJson 
                            FROM dbo.tbl_class_department cd
                            INNER JOIN dbo.tbl_class_section cs ON cd.classDepartmentID = cs.classDepartmentID 
                            INNER JOIN dbo.tbl_student_registration sr ON cs.classSectionID = sr.classSectionID 
                            INNER JOIN dbo.tbl_student s ON sr.studentRegistrationID = s.studentRegistrationID 
                            LEFT OUTER JOIN dbo.tbl_attendance AS a ON s.studentID = a.studentID AND 
                            cast(a.attendanceDate as date) = cast(getdate() as date)
                            INNER JOIN dbo.tbl_calss c ON c.classID = cs.classID where s.isDeleted = 0
                            GROUP BY c.classID, c.className, cs.classID ORDER BY MIN(cs.classID)";
                }
                else
                {
                    cmd = @"SELECT COUNT(DISTINCT s.studentRegistrationCode) AS totalClassStudent,
                            COALESCE(SUM(CASE WHEN a.attendanceFlag = 'P' THEN 1 ELSE 0 END), 0) AS presentClassStudents,
                            COALESCE(SUM(CASE WHEN a.attendanceFlag = 'A' THEN 1 ELSE 0 END), 0) AS absentClassStudents,
                            COALESCE(SUM(CASE WHEN a.attendanceFlag = 'L' THEN 1 ELSE 0 END), 0) AS onLeaveClassStudents,
                            c.classID,c.className,
                            (SELECT COUNT(DISTINCT subs.studentRegistrationCode) AS totalSectionStudent,
                            COALESCE(SUM(CASE WHEN suba.attendanceFlag = 'P' THEN 1 ELSE 0 END), 0) AS presentSectionStudents,
                            COALESCE(SUM(CASE WHEN suba.attendanceFlag = 'A' THEN 1 ELSE 0 END), 0) AS absentSectionStudents,
                            COALESCE(SUM(CASE WHEN suba.attendanceFlag = 'L' THEN 1 ELSE 0 END), 0) AS onLeaveSectionStudents,
                            subsec.sectionID,subsec.sectionName FROM dbo.tbl_class_department subcd
                            INNER JOIN dbo.tbl_class_section subcs ON subcd.classDepartmentID = subcs.classDepartmentID 
                            INNER JOIN dbo.tbl_student_registration subsr ON subcs.classSectionID = subsr.classSectionID 
                            INNER JOIN dbo.tbl_student subs ON subsr.studentRegistrationID = subs.studentRegistrationID 
                            LEFT OUTER JOIN dbo.tbl_attendance AS suba ON subs.studentID = suba.studentID AND 
                            cast(suba.attendanceDate as date) = cast('" + attendanceDate + @"' as date)
                            INNER JOIN tbl_section subsec ON subsec.sectionID = subcs.sectionID 
                            WHERE subs.isDeleted = 0 and subcs.classID = cs.classID and subcd.branch_department_section_id = cd.branch_department_section_id
                            GROUP BY subsec.sectionID, subsec.sectionName,subcd.branch_department_section_id FOR JSON AUTO) AS sectionJson 
                            FROM dbo.tbl_class_department cd
                            INNER JOIN dbo.tbl_class_section cs ON cd.classDepartmentID = cs.classDepartmentID 
                            INNER JOIN dbo.tbl_student_registration sr ON cs.classSectionID = sr.classSectionID 
                            INNER JOIN dbo.tbl_student s ON sr.studentRegistrationID = s.studentRegistrationID 
                            LEFT OUTER JOIN dbo.tbl_attendance AS a ON s.studentID = a.studentID AND 
                            cast(a.attendanceDate as date) = cast('" + attendanceDate + @"' as date)
                            INNER JOIN dbo.tbl_calss c ON c.classID = cs.classID 
                            where s.isDeleted = 0 and cd.branch_department_section_id = " + branch_department_section_id + @"
                            GROUP BY c.classID, c.className, cs.classID,cd.branch_department_section_id ORDER BY MIN(cs.classID)";
                }    
                var appMenu = dapperQuery.Qry<DrilldownAttendance>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getPresentStudent")]
        public IActionResult getPresentStudent(int branch_department_section_id,string attendanceDate)
        {
            try
            {
                if (branch_department_section_id == 0)
                {
                    cmd = @"select count( Distinct studentRegistrationCode) as todayPresentStudents,Count(DiSTINCt cs.classID) as classCount
                        FROM dbo.tbl_class_department cd
                        INNER JOIN dbo.tbl_class_section cs ON cd.classDepartmentID = cs.classDepartmentID 
                        INNER JOIN dbo.tbl_student_registration sr ON cs.classSectionID = sr.classSectionID 
                        INNER JOIN dbo.tbl_student s ON sr.studentRegistrationID = s.studentRegistrationID 
                        INNER JOIN dbo.tbl_attendance a  ON s.studentID = a.studentID 
                        where attendanceDate = '" + attendanceDate + "' and attendanceFlag = 'P'";
                }
                else
                {
                    cmd = @"select count( Distinct studentRegistrationCode) as todayPresentStudents,Count(DiSTINCt cs.classID) as classCount
                        FROM dbo.tbl_class_department cd
                        INNER JOIN dbo.tbl_class_section cs ON cd.classDepartmentID = cs.classDepartmentID 
                        INNER JOIN dbo.tbl_student_registration sr ON cs.classSectionID = sr.classSectionID 
                        INNER JOIN dbo.tbl_student s ON sr.studentRegistrationID = s.studentRegistrationID 
                        INNER JOIN dbo.tbl_attendance a  ON s.studentID = a.studentID 
                        where cd.branch_department_section_id = " + branch_department_section_id + " and attendanceDate = '" + attendanceDate + "' and attendanceFlag = 'P'";
                }
                var appMenu = dapperQuery.Qry<PresentStudent>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getAbsentStudent")]
        public IActionResult getAbsentStudent(int branch_department_section_id,string attendanceDate)
        {
            try
            {
                if (branch_department_section_id == 0)
                {
                    cmd = @"select count(Distinct studentRegistrationCode) as todayAbsentStudents,Count(DiSTINCt cs.classID) as classCount
                        FROM dbo.tbl_class_department cd 
                        INNER JOIN dbo.tbl_class_section cs ON cd.classDepartmentID = cs.classDepartmentID 
                        INNER JOIN dbo.tbl_student_registration sr ON cs.classSectionID = sr.classSectionID 
                        INNER JOIN dbo.tbl_student s ON sr.studentRegistrationID = s.studentRegistrationID 
                        INNER JOIN dbo.tbl_attendance a ON s.studentID = a.studentID 
                        where attendanceDate = '" + attendanceDate + "' and attendanceFlag = 'A'";   
                }
                else
                {
                    cmd = @"select count(Distinct studentRegistrationCode) as todayAbsentStudents,Count(DiSTINCt cs.classID) as classCount
                        FROM dbo.tbl_class_department cd 
                        INNER JOIN dbo.tbl_class_section cs ON cd.classDepartmentID = cs.classDepartmentID 
                        INNER JOIN dbo.tbl_student_registration sr ON cs.classSectionID = sr.classSectionID 
                        INNER JOIN dbo.tbl_student s ON sr.studentRegistrationID = s.studentRegistrationID 
                        INNER JOIN dbo.tbl_attendance a ON s.studentID = a.studentID 
                        where cd.branch_department_section_id = " + branch_department_section_id + " and attendanceDate = '" + attendanceDate + "' and attendanceFlag = 'A'";
                }
                var appMenu = dapperQuery.Qry<AbsentStudent>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getOnLeaveStudent")]
        public IActionResult getOnLeaveStudent(int branch_department_section_id,string attendanceDate)
        {
            try
            {
                if (branch_department_section_id == 0)
                {
                    cmd = "select count(Distinct studentRegistrationCode) as todayOnLeaveStudents,Count(DiSTINCt dbo.tbl_class_section.classID) as classCount FROM dbo.tbl_class_department INNER JOIN dbo.tbl_class_section ON dbo.tbl_class_department.classDepartmentID = dbo.tbl_class_section.classDepartmentID INNER JOIN dbo.tbl_student_registration ON dbo.tbl_class_section.classSectionID = dbo.tbl_student_registration.classSectionID INNER JOIN dbo.tbl_student ON dbo.tbl_student_registration.studentRegistrationID = dbo.tbl_student.studentRegistrationID INNER JOIN dbo.tbl_attendance AS tbl_attendance ON dbo.tbl_student.studentID = tbl_attendance.studentID where attendanceDate = '" + attendanceDate + "' and attendanceFlag = 'L'";
                }
                else
                {
                    cmd = "select count(Distinct studentRegistrationCode) as todayOnLeaveStudents,Count(DiSTINCt dbo.tbl_class_section.classID) as classCount FROM dbo.tbl_class_department INNER JOIN dbo.tbl_class_section ON dbo.tbl_class_department.classDepartmentID = dbo.tbl_class_section.classDepartmentID INNER JOIN dbo.tbl_student_registration ON dbo.tbl_class_section.classSectionID = dbo.tbl_student_registration.classSectionID INNER JOIN dbo.tbl_student ON dbo.tbl_student_registration.studentRegistrationID = dbo.tbl_student.studentRegistrationID INNER JOIN dbo.tbl_attendance AS tbl_attendance ON dbo.tbl_student.studentID = tbl_attendance.studentID where dbo.tbl_class_department.branch_department_section_id = " + branch_department_section_id + " and attendanceDate = '" + attendanceDate + "' and attendanceFlag = 'L'";
                }
                var appMenu = dapperQuery.Qry<LeaveStudent>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getBoysPresence")]
        public IActionResult getBoysPresence(int branch_department_section_id,string attendanceDate)
        {
            try
            {
                if (branch_department_section_id == 0)
                {
                    cmd = "SELECT Distinct COALESCE((COUNT(case when attendanceFlag = 'P' then 1 end)),0) as totalPresentBoys, COALESCE((COUNT(case when attendanceFlag = 'A' then 1 end)),0) as totalAbsentBoys, COALESCE((COUNT(case when attendanceFlag = 'L' then 1 end)),0) as totalLeaveBoys FROM dbo.tbl_class_department INNER JOIN dbo.tbl_class_section ON dbo.tbl_class_department.classDepartmentID = dbo.tbl_class_section.classDepartmentID INNER JOIN dbo.tbl_student_registration ON dbo.tbl_class_section.classSectionID = dbo.tbl_student_registration.classSectionID INNER JOIN dbo.tbl_student ON dbo.tbl_student_registration.studentRegistrationID = dbo.tbl_student.studentRegistrationID INNER JOIN dbo.tbl_attendance AS tbl_attendance ON dbo.tbl_student.studentID = tbl_attendance.studentID WHERE (dbo.tbl_student.isDeleted = 0) AND (dbo.tbl_class_department.departmentTypeID = 1) and (attendanceDate = '" + attendanceDate + "')";
                }
                else
                {
                    cmd = "SELECT Distinct COALESCE((COUNT(case when attendanceFlag = 'P' then 1 end)),0) as totalPresentBoys, COALESCE((COUNT(case when attendanceFlag = 'A' then 1 end)),0) as totalAbsentBoys, COALESCE((COUNT(case when attendanceFlag = 'L' then 1 end)),0) as totalLeaveBoys FROM dbo.tbl_class_department INNER JOIN dbo.tbl_class_section ON dbo.tbl_class_department.classDepartmentID = dbo.tbl_class_section.classDepartmentID INNER JOIN dbo.tbl_student_registration ON dbo.tbl_class_section.classSectionID = dbo.tbl_student_registration.classSectionID INNER JOIN dbo.tbl_student ON dbo.tbl_student_registration.studentRegistrationID = dbo.tbl_student.studentRegistrationID INNER JOIN dbo.tbl_attendance AS tbl_attendance ON dbo.tbl_student.studentID = tbl_attendance.studentID WHERE (dbo.tbl_student.isDeleted = 0) AND (dbo.tbl_class_department.branch_department_section_id = " + branch_department_section_id + ") AND (dbo.tbl_class_department.departmentTypeID = 1) and (attendanceDate = '" + attendanceDate + "')";
                }
                var appMenu = dapperQuery.Qry<DailyPresence>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getGirlsPresence")]
        public IActionResult getGirlsPresence(int branch_department_section_id,string attendanceDate)
        {
            try
            {
                if (branch_department_section_id == 0)
                {
                    cmd = "SELECT Distinct COALESCE((COUNT(case when attendanceFlag = 'P' then 1 end)),0) as totalPresentGirls, COALESCE((COUNT(case when attendanceFlag = 'A' then 1 end)),0) as totalAbsentGirls, COALESCE((COUNT(case when attendanceFlag = 'L' then 1 end)),0) as totalLeaveGirls FROM dbo.tbl_class_department INNER JOIN dbo.tbl_class_section ON dbo.tbl_class_department.classDepartmentID = dbo.tbl_class_section.classDepartmentID INNER JOIN dbo.tbl_student_registration ON dbo.tbl_class_section.classSectionID = dbo.tbl_student_registration.classSectionID INNER JOIN dbo.tbl_student ON dbo.tbl_student_registration.studentRegistrationID = dbo.tbl_student.studentRegistrationID INNER JOIN dbo.tbl_attendance AS tbl_attendance ON dbo.tbl_student.studentID = tbl_attendance.studentID WHERE (dbo.tbl_student.isDeleted = 0) AND (dbo.tbl_class_department.departmentTypeID = 2) and (attendanceDate = '" + attendanceDate + "')";
                }
                else
                {
                    cmd = "SELECT Distinct COALESCE((COUNT(case when attendanceFlag = 'P' then 1 end)),0) as totalPresentGirls, COALESCE((COUNT(case when attendanceFlag = 'A' then 1 end)),0) as totalAbsentGirls, COALESCE((COUNT(case when attendanceFlag = 'L' then 1 end)),0) as totalLeaveGirls FROM dbo.tbl_class_department INNER JOIN dbo.tbl_class_section ON dbo.tbl_class_department.classDepartmentID = dbo.tbl_class_section.classDepartmentID INNER JOIN dbo.tbl_student_registration ON dbo.tbl_class_section.classSectionID = dbo.tbl_student_registration.classSectionID INNER JOIN dbo.tbl_student ON dbo.tbl_student_registration.studentRegistrationID = dbo.tbl_student.studentRegistrationID INNER JOIN dbo.tbl_attendance AS tbl_attendance ON dbo.tbl_student.studentID = tbl_attendance.studentID WHERE (dbo.tbl_student.isDeleted = 0) AND (dbo.tbl_class_department.branch_department_section_id = " + branch_department_section_id + ") AND (dbo.tbl_class_department.departmentTypeID = 2) and (attendanceDate = '" + attendanceDate + "')";
                }
                var appMenu = dapperQuery.Qry<DailyPresence>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getOverallAttendance")]
        public IActionResult getOverallAttendance(string attendanceDate)
        {
            try
            {
                cmd = "select * from fun_overallAttendance('" + attendanceDate + "')";
                var appMenu = dapperQuery.Qry<OverallAttendance>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getFrequentAbsent")]
        public IActionResult getFrequentAbsent(int branch_department_section_id,string attendanceDate)
        {
            try
            {
                cmd = "select a.studentID,count(*) as absentCount,s.studentName,s.studentEdoc,c.classID,c.className FROM dbo.tbl_class_department as cd INNER JOIN dbo.tbl_class_section as cs ON cd.classDepartmentID = cs.classDepartmentID INNER JOIN dbo.tbl_calss as c ON cs.classID = c.classID INNER JOIN dbo.tbl_student_registration as sr ON cs.classSectionID = sr.classSectionID INNER JOIN dbo.tbl_student as s ON sr.studentRegistrationID = s.studentRegistrationID INNER JOIN dbo.tbl_attendance AS a ON s.studentID = a.studentID and month(a.attendanceDate) = month('" + attendanceDate + "') GROUP BY a.studentID,cd.branch_department_section_id,a.attendanceFlag,s.studentName,c.classID,c.className,s.studentEdoc having cd.branch_department_section_id = " + branch_department_section_id + " and a.attendanceFlag = 'A'";
                var appMenu = dapperQuery.Qry<FrequentAbsent>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getAbsentOfBranch")]
        public IActionResult getAbsentOfBranch(string attendanceDate)
        {
            try
            {
                cmd = "SELECT Distinct cd.branch_department_section_id,ISNULL((COUNT(case when attendanceFlag = 'A' then 1 end)),0) as totalAbsentStudent FROM dbo.tbl_class_department as cd LEFT OUTER JOIN dbo.tbl_class_section as cs ON cd.classDepartmentID = cs.classDepartmentID LEFT OUTER JOIN dbo.tbl_student_registration as sr ON cs.classSectionID = sr.classSectionID LEFT OUTER JOIN dbo.tbl_student as s ON sr.studentRegistrationID = s.studentRegistrationID LEFT OUTER JOIN dbo.tbl_attendance as a ON s.studentID = a.studentID and (attendanceDate = '" + attendanceDate + "') group by cd.branch_department_section_id";
                var appMenu = dapperQuery.Qry<BranchAbsent>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getAbsentOfClass")]
        public IActionResult getAbsentOfClass(int branch_department_section_id,string attendanceDate)
        {
            try
            {
                cmd = "select distinct c.classID,c.className,ISNULL((COUNT(case when attendanceFlag = 'A' then 1 end)),0) as totalAbsentStudent FROM dbo.tbl_class_department as cd INNER JOIN dbo.tbl_class_section as cs ON cd.classDepartmentID = cs.classDepartmentID INNER JOIN dbo.tbl_calss as c ON cs.classID = c.classID INNER JOIN dbo.tbl_student_registration as sr ON cs.classSectionID = sr.classSectionID INNER JOIN dbo.tbl_student as s ON sr.studentRegistrationID = s.studentRegistrationID left outer JOIN dbo.tbl_attendance AS a ON s.studentID = a.studentID and day(a.attendanceDate) = day('" + attendanceDate + "') GROUP BY a.attendanceFlag,a.attendanceDate,c.classID,c.className,cd.branch_department_section_id having cd.branch_department_section_id = " + branch_department_section_id + "";
                var appMenu = dapperQuery.Qry<BranchAbsent>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getTotalBoys")]
        public IActionResult getTotalBoys(int branch_department_section_id)
        {
            try
            {
                cmd = "SELECT COUNT(Distinct  dbo.tbl_student.studentRegistrationCode) AS branchTotalBoys FROM dbo.tbl_class_department INNER JOIN dbo.tbl_class_section ON dbo.tbl_class_department.classDepartmentID = dbo.tbl_class_section.classDepartmentID INNER JOIN dbo.tbl_student_registration ON dbo.tbl_class_section.classSectionID = dbo.tbl_student_registration.classSectionID INNER JOIN dbo.tbl_student ON dbo.tbl_student_registration.studentRegistrationID = dbo.tbl_student.studentRegistrationID WHERE (dbo.tbl_student.isDeleted = 0) AND (dbo.tbl_class_department.branch_department_section_id = " + branch_department_section_id + ") AND (dbo.tbl_class_department.departmentTypeID = 1)";
                var appMenu = dapperQuery.Qry<TotalBoys>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getTotalGirls")]
        public IActionResult getTotalGirls(int branch_department_section_id)
        {
            try
            {
                cmd = "SELECT COUNT( Distinct dbo.tbl_student.studentRegistrationCode) AS branchTotalGirls FROM dbo.tbl_class_department INNER JOIN dbo.tbl_class_section ON dbo.tbl_class_department.classDepartmentID = dbo.tbl_class_section.classDepartmentID INNER JOIN dbo.tbl_student_registration ON dbo.tbl_class_section.classSectionID = dbo.tbl_student_registration.classSectionID INNER JOIN dbo.tbl_student ON dbo.tbl_student_registration.studentRegistrationID = dbo.tbl_student.studentRegistrationID WHERE (dbo.tbl_student.isDeleted = 0) AND (dbo.tbl_class_department.branch_department_section_id = " + branch_department_section_id + ") AND (dbo.tbl_class_department.departmentTypeID = 2)";
                var appMenu = dapperQuery.Qry<TotalGirls>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }
    
        
    }
}