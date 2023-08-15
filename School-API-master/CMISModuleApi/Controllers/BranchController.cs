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
    public class BranchController : ControllerBase
    {
        private readonly IOptions<conStr> _dbCon;
        private string cmd;

        public BranchController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        }

        [HttpGet("getBranchList")]
        public IActionResult getBranchList()
        {
            try
            {
                cmd = "SELECT * FROM view_branchList";
                var appMenu = dapperQuery.Qry<Branch>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

        [HttpGet("getBranchName")]
        public IActionResult getBranchName( int branch_id)
        {
            try
            {
                cmd = "SELECT * FROM tbl_branches_loc where branch_id = "+branch_id+"";
                var appMenu = dapperQuery.Qry<BranchName>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

        [HttpGet("getBranchSetup")]
        public IActionResult getBranchSetup(int branchID, int companyID)
        {
            try
            {
                cmd = "SELECT * FROM view_branchSetup";
                var appMenu = dapperQuery.Qry<BranchSetup>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

        [HttpGet("getClassBranchDepartment")]
        public IActionResult getClassBranchDepartment()
        {
            try
            {
                cmd = "SELECT * FROM view_classBranchDepartment";
                var appMenu = dapperQuery.Qry<ClassBranchDepartment>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

        [HttpPost("getBranchSetupJson")]
        public IActionResult getBranchSetupJson(CompanyList model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_BranchSetupListDetail",model,_dbCon);
                return Ok(new { message = response });
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getCompanyBranchList")]
        public IActionResult getCompanyBranchList()
        {
            try
            {
                cmd = "SELECT * FROM view_companyBranch";
                var appMenu = dapperQuery.Qry<CompanyBranch>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getBranchDetail")]
        public IActionResult getBranchDetail(int branchID)
        {
            try
            {
                cmd = "SELECT * FROM view_branchDetail where branch_id="+branchID+"";
                var appMenu = dapperQuery.Qry<BranchDetail>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

        [HttpGet("getBranchType")]
        public IActionResult getBranchType()
        {
            try
            {
                cmd = "SELECT branch_type_id,branch_type_title FROM tbl_branch_type";
                var appMenu = dapperQuery.Qry<BranchType>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

        [HttpPost("saveBranch")]
        public IActionResult saveBranch(BranchCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_branchCrud", model, _dbCon);
             return Ok(response);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }
        
        [HttpPost("saveBranchSetup")]
        public IActionResult saveBranchSetup(BranchSetupCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_branchSetupCrud", model, _dbCon);
             return Ok(response);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }
    }
}