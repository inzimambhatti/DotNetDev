using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CMISModuleApi.Services;
using Microsoft.Extensions.Options;
using CMISModuleApi.Configuration;
using CMISModuleApi.Entities;
using Dapper;
using CMISModuleApi.dto.request;
using System.Data;
using Newtonsoft.Json;

namespace CMISModuleApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IOptions<conStr> _dbCon;
        private string cmd;

        public DepartmentController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        }

        [HttpGet("getDepartment")]
        public IActionResult getDepartment()
        {
            try
            {
                cmd = "SELECT * FROM view_department";
                var appMenu = dapperQuery.Qry<department>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

        [HttpGet("getDepartmentContact")]
        public IActionResult getDepartmentContact(int branchID,int companyID,int deptID)
        {
            try
            {
                cmd = "SELECT * FROM view_departmentContact where branch_id="+branchID+" and company_id="+companyID+" and department_section_id="+deptID+"";
                var appMenu = dapperQuery.Qry<DepartmentContact>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

        [HttpPost("saveDepartment")]
        public IActionResult saveDepartment(departmentCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_departmentCrud",model,_dbCon);

                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }
        [HttpPost("saveDepartmentContact")]
        public IActionResult saveDepartmentContact(DepartmentContactCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_departmentContactCrud",model,_dbCon);

                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }
    }
}