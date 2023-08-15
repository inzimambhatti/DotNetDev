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
    public class TeacherLoadController : ControllerBase
    {
        private readonly IOptions<conStr> _dbCon;
        private string cmd;

        public TeacherLoadController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        } 

        [HttpGet("getTeacherLoad")]
        public IActionResult getTeacherLoad(int branch_department_section_id,int departmentTypeID,int schoolSessionID,int subjectID)
        {
            try
            {
                cmd = @"DECLARE	@return_value int

                        EXEC	@return_value = [dbo].[sp_getTeacherLoad]
                                @branch_department_section_id = " + branch_department_section_id + @",
                                @departmentTypeID = " + departmentTypeID + @",
                                @schoolSessionID = " + schoolSessionID + @",
                                @subjectID = " + subjectID + " ";
                var appMenu = dapperQuery.Qry<TeacherLoad>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        } 

        [HttpGet("getBranchSubject")]
        public IActionResult getBranchSubject(int branch_department_section_id)
        {
            try
            {
                cmd = @"select distinct s.subjectID,s.subjectTitle from tbl_subjects s
                        inner join tbl_class_section_subjects css on css.subjectID = s.subjectID
                        inner join tbl_class_section cs on cs.classSectionID = css.classSectionID
                        inner join tbl_class_department cd on cd.classDepartmentID = cs.classDepartmentID
                        where css.isDeleted = 0 and cs.isDeleted = 0 and cd.isDeleted = 0 and s.isDeleted = 0 
                        and cd.branch_department_section_id = " + branch_department_section_id + @"";
                var appMenu = dapperQuery.Qry<Subjects>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        } 

        [HttpPost("saveTeacherLoad")]
        public IActionResult saveTeacherLoad(TeacherLoadCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_teacherLoad",model,_dbCon);

                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }   
        
    }
}