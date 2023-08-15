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
    public class ClassController : ControllerBase
    {
        private readonly IOptions<conStr> _dbCon;
        private string cmd;

        public ClassController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        }
    
        [HttpGet("getClassSection")]
        public IActionResult getClassSection()
        {
            try
            {
                cmd = "SELECT * FROM view_regClass";
                var appMenu = dapperQuery.Qry<ClassReg>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getClassRegDetail")]
        public IActionResult getClassRegDetail(int classID,int branch_department_section_id, int departmentTypeID)
        {
            try
            {
                cmd = "SELECT * FROM view_classRegistrationDetail where classID = " + classID + " and branch_department_section_id = " + branch_department_section_id + " and departmentTypeID= "+departmentTypeID+"";
                var appMenu = dapperQuery.Qry<ClassRegistrationDetail>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getDepartmentType")]
        public IActionResult getDepartmentType()
        {
            try
            {
                cmd = "SELECT departmentTypeID,departmentTypeName FROM tbl_department_type where isDeleted=0";
                var appMenu = dapperQuery.Qry<DepartmentType>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getClass")]
        public IActionResult getClass()
        {
            try
            {
                cmd = "SELECT classID,className FROM tbl_calss where isDeleted=0";
                var appMenu = dapperQuery.Qry<Class>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("saveClass")]
        public IActionResult saveClass(ClassRegCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_classRegistration",model,_dbCon);

                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }
        
    }
}