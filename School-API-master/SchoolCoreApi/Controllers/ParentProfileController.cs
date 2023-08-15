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
    public class ParentProfileController : ControllerBase
    {
        private readonly IOptions<conStr> _dbCon;
        private string cmd;

        public ParentProfileController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        }

        [HttpGet("getParentData")]
        public IActionResult getParentData()
        {
            try
            {
                cmd = "SELECT * FROM view_parentGuardianProfile where isDeleted = 0";
                var appMenu = dapperQuery.Qry<ParentProfile>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getParentProfile")]
        public IActionResult getParentProfile(int studentID)
        {
            try
            {
                cmd = "SELECT * FROM view_parentGuardianProfile where studentID = " + studentID + " and isDeleted = 0";
                var appMenu = dapperQuery.Qry<ParentProfile>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getParentDetail")]
        public IActionResult getParentDetail(string parentCNIC)
        {
            try
            {
                cmd = "SELECT * FROM view_parentGuardianProfile where parentPassportOrCNIC = '" + parentCNIC + "' and isDeleted = 0";
                var appMenu = dapperQuery.Qry<ParentDetail>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("saveParentProfile")]
        public IActionResult saveParentProfile(ParentProfileCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_parentProfile",model,_dbCon);
                
                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }       
    }
}