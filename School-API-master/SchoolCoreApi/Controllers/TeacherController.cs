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
    public class TeacherController : ControllerBase
    {
        private readonly IOptions<conStr> _dbCon;
        private string cmd;

        public TeacherController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        }

        [HttpGet("getTeacher")]
        public IActionResult getTeacher()
        {
            try
            {
                cmd = "SELECT * FROM tbl_teacher where isDeleted=0";
                var appMenu = dapperQuery.Qry<Teacher>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getSubjectTeacher")]
        public IActionResult getSubjectTeacher(int subjectID)
        {
            try
            {
                cmd = @"select Distinct 
                        (select COUNT(tcss.classSectionSubjectID) from tbl_teacher_class_section_subject tcss
                        where tcss.teacherID = t.teacherID and tcss.isDeleted = 0) as classCount,
                        t.teacherID,t.teacherFirstName,t.teacherLastName,tcs.subjectID,tb.branch_department_section_id 
                        from tbl_teacher t
                        Inner join tbl_teacher_class_subject tcs on tcs.teacherID = t.teacherID
                        Inner Join tbl_subjects s on s.subjectID = tcs.subjectID
                        Inner join tbl_teacher_branch tb on tb.teacherID = t.teacherID
                        where t.isDeleted = 0 and tcs.isDeleted = 0 
                        and tcs.subjectID = " + subjectID + @"
                        GROUP BY t.teacherID,t.teacherFirstName,t.teacherLastName,tcs.subjectID,tb.branch_department_section_id ";
                var appMenu = dapperQuery.Qry<SubjectTeacher>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }
        
        [HttpGet("getSubjects")]
        public IActionResult getSubjects()
        {
            try
            {
                cmd = "SELECT * FROM tbl_subjects where isDeleted=0";
                var appMenu = dapperQuery.Qry<Subjects>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getTeacherDetail")]
        public IActionResult getTeacherDetail(int teacherID)
        {
            try
            {
                cmd = "SELECT * FROM view_teacherDetail where teacherID = "+teacherID+" and isDeleted = 0";
                var appMenu = dapperQuery.Qry<TeacherDetail>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }
        
        [HttpGet("getTeacherClassSection")]
        public IActionResult getTeacherClassSection(int teacherID)
        {
            try
            {
                
                if(teacherID == 0){
                    cmd = @"SELECT DISTINCT 
                            dbo.tbl_section.sectionName, dbo.tbl_section.sectionID, dbo.tbl_calss.classID, dbo.tbl_calss.className, dbo.tbl_department_type.departmentTypeID, dbo.tbl_department_type.departmentTypeName, 
                            cd.branch_department_section_id, bl.branch_id, bl.branch_name
                            FROM dbo.tbl_class_department AS cd INNER JOIN
                            aimsCMIS.dbo.tbl_branch_department_sections AS bds ON bds.branch_department_section_id = cd.branch_department_section_id INNER JOIN
                            aimsCMIS.dbo.tbl_company_branch AS cb ON cb.company_branch_id = bds.company_branch_id INNER JOIN
                            aimsCMIS.dbo.tbl_branches_loc AS bl ON bl.branch_id = cb.branch_id INNER JOIN
                            dbo.tbl_class_section ON cd.classDepartmentID = dbo.tbl_class_section.classDepartmentID INNER JOIN
                            dbo.tbl_department_type ON cd.departmentTypeID = dbo.tbl_department_type.departmentTypeID INNER JOIN
                            dbo.tbl_section ON dbo.tbl_class_section.sectionID = dbo.tbl_section.sectionID INNER JOIN
                            dbo.tbl_calss ON dbo.tbl_class_section.classID = dbo.tbl_calss.classID
                            WHERE (dbo.tbl_class_section.isDeleted = 0) ";
                }else{
                    cmd = "SELECT * FROM view_teacherDetail where teacherID = "+teacherID+" and isDeleted = 0";
                }

                var appMenu = dapperQuery.Qry<TeacherDetail>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getTeacherSubject")]
        public IActionResult getTeacherSubject(int teacherID)
        {
            try
            {   
                cmd = "SELECT * FROM fun_teacherSubject("+teacherID+")";
                var appMenu = dapperQuery.Qry<TeacherSubject>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getTeacherBranchs")]
        public IActionResult getTeacherBranchs(int teacherID)
        {
            try
            {   
                cmd = "SELECT * FROM fun_teacherBranches("+teacherID+")";
                var appMenu = dapperQuery.Qry<TeacherBranch>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getClassAssignment")]
        public IActionResult getClassAssignment(int teacherID, int classSectionSubjectID)
        {
            try
            {
                
                cmd =@"SELECT cd.branch_department_section_id, css.subjectID, cd.departmentTypeID, cs.classID, 
                        cs.sectionID, tcss.isClassIncharge, tcss.isAttendance
                        FROM   dbo.tbl_class_department cd 
                        INNER JOIN dbo.tbl_class_section cs ON cd.classDepartmentID = cs.classDepartmentID INNER JOIN
                        tbl_class_section_subjects as css on cs.classSectionID = css.classSectionID INNER JOIN
                        tbl_teacher_class_section_subject as tcss on tcss.classSectionSubjectID = css.classSectionSubjectID
                        where cs.isDeleted = 0 and css.isDeleted=0 and tcss.isDeleted=0 and 
                        tcss.classSectionSubjectID="+classSectionSubjectID+" and tcss.teacherID="+teacherID+"";
                var appMenu = dapperQuery.Qry<ClassAssignment>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("saveClassAssignment")]
        public IActionResult saveClassAssignment(ClassAssignmentCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_classAssignment",model,_dbCon);
                
                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("saveTeacher")]
        public IActionResult saveTeacher(TeacherCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_teacherRegistration",model,_dbCon);
                var data = response.Select(row => new { res = row.ToString() });
                bool result = data.First().res.Contains("Success");

                if (result == true && (model.teacher_picture_path != null && model.teacher_picture_path != "" && model.teacher_picture_path != "null"))
                {

                    var teacherID = data.First().res.Split("|||")[1];

                    dapperQuery.saveImageFile(
                        model.teacher_picture_path,
                        teacherID,
                        model.teacher_picture,
                        model.teacher_picture_extension);
                }
                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }
        
    }
}