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
    public class SubjectLoadController : ControllerBase
    {
        private readonly IOptions<conStr> _dbCon;
        private string cmd;

        public SubjectLoadController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        } 

        [HttpGet("getSubjectLoad")]
        public IActionResult getSubjectLoad(int branch_department_section_id,int departmentTypeID,int schoolSessionID)
        {
            try
            {
                cmd = @"DECLARE	@return_value int

                        EXEC	@return_value = [dbo].[sp_getSubjectLoad]
                                @branch_department_section_id = " + branch_department_section_id + @",
                                @departmentTypeID = " + departmentTypeID + @",
                                @schoolSessionID = " + schoolSessionID + " ";
                var appMenu = dapperQuery.Qry<SubjectLoad>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        } 

        [HttpGet("getBranchSubjectLoad")]
        public IActionResult getBranchSubjectLoad(int schoolSessionID,int branch_department_section_id)
        {
            try
            {
                cmd = @"SELECT DISTINCT * from fun_branchSubjectLoad(" + schoolSessionID + "," + branch_department_section_id + ")";
                var appMenu = dapperQuery.Qry<BranchSubjectLoad>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        } 

        [HttpGet("getSubjectLoadClasses")]
        public IActionResult getSubjectLoadClasses(int branch_department_section_id,int departmentTypeID)
        {
            try
            {
                cmd = @"select DISTINCT c.classID,s.sectionID,CONCAT(c.className,' (',s.sectionName,')') as className from tbl_class_department cd 
                        Inner join tbl_class_section cs on cs.classDepartmentID = cd.classDepartmentID
                        Inner join tbl_calss c on c.classID = cs.classID
                        Inner join tbl_section s on s.sectionID = cs.sectionID
                        where cd.branch_department_section_id = " + branch_department_section_id + @" and cd.departmentTypeID = " + departmentTypeID + @"
                        and cd.isDeleted = 0 and cs.isDeleted = 0 and c.isDeleted = 0";
                var appMenu = dapperQuery.Qry<SubjectLoadClasses>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        } 

        [HttpPost("saveSubjectLoad")]
        public IActionResult saveSubjectLoad(SubjectLoadCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_subjectLoad",model,_dbCon);

                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }
        
    }
}