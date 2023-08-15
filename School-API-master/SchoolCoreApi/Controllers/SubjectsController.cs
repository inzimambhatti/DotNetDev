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
    public class SubjectsController : ControllerBase
    {
        private readonly IOptions<conStr> _dbCon;
        private string cmd;

        public SubjectsController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        } 

        [HttpGet("getSubjects")]
        public IActionResult getSubjects()
        {
            try
            {
                cmd = "SELECT * FROM tbl_subjects where isDeleted = 0";
                var appMenu = dapperQuery.Qry<SubjectsDetail>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        } 

        [HttpGet("getClassSubject")]
        public IActionResult getClassSubject(int branch_department_section_id, int departmentTypeID,int classID)
        {
            try
            {
                cmd = @"select Distinct s.subjectID,s.subjectTitle from tbl_class_department cd
                        Inner Join tbl_class_section cs on cs.classDepartmentID = cd.classDepartmentID
                        Inner Join tbl_class_section_subjects css on css.classSectionID = cs.classSectionID
                        Inner Join tbl_subjects s on s.subjectID = css.subjectID
                        where s.isDeleted = 0 and css.isDeleted = 0 and 
                        cd.branch_department_section_id = " + branch_department_section_id + @" and cd.departmentTypeID = " + departmentTypeID + @" and 
                        cs.classID = " + classID + @"";
                var appMenu = dapperQuery.Qry<Subjects>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        } 

        [HttpGet("getStudentSubjectDetail")]
        public IActionResult getStudentSubjectDetail(int branch_department_section_id,int departmentTypeID, int classID, int sectionID, int subjectID )
        {
            try
            {
                cmd = "SELECT * FROM fun_studentSubjectDetail("+branch_department_section_id+","+departmentTypeID+","+classID+","+sectionID+","+subjectID+")";
                var appMenu = dapperQuery.Qry<StudentSubjectDetail>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getSections")]
        public IActionResult getSections(int branch_department_section_id, int departmentTypeID, int classID)
        {
            try
            {
                cmd = "SELECT * FROM fun_sections("+branch_department_section_id+","+departmentTypeID+","+classID+")";
                var appMenu = dapperQuery.Qry<Sections>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }  


        [HttpGet("getSectionsSubjects")]
        public IActionResult getSectionsSubjects(int branch_department_section_id, int departmentTypeID, int classID, string classSectionSubjectDate)
        {
            try
            {
                cmd = "DECLARE	@return_value int "+
                        " EXEC	@return_value = [dbo].[sp_getSectionSubject] "+
                        " @branch_department_section_id = " + branch_department_section_id + ",@classID = " + classID + ",@departmentTypeID = " + departmentTypeID + ",@classSectionSubjectDate = '" + classSectionSubjectDate + "' "+
                        " SELECT	'Return Value' = @return_value";
                var appMenu = dapperQuery.Qry<ClassSectionSubjects>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }  
        
        [HttpPost("saveSubjects")]
        public IActionResult saveSubjects(SubjectsCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_subjects",model,_dbCon);

                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }
        // Post Function For Section Subject Page
        [HttpPost("getSectionSubject")]
        public IActionResult getSectionSubject(SectionSubjectDetail model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_getSectionSubject",model,_dbCon);

                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("saveSectionSubject")]
        public IActionResult saveSectionSubject(SectionSubjectCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_sectionSubject",model,_dbCon);

                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("saveStudentSubject")]
        public IActionResult saveStudentSubject(StudentSubjectCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_studentSubject",model,_dbCon);

                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }
    }
}