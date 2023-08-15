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
    public class SectionController : ControllerBase
    {
        private readonly IOptions<conStr> _dbCon;
        private string cmd;

        public SectionController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        }

        [HttpGet("getSection")]
        public IActionResult getSection()
        {
            try
            {
                cmd = "SELECT sectionID,sectionName,sectionDescription FROM tbl_section where isDeleted=0";
                var appMenu = dapperQuery.Qry<Section>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("saveSection")]
        public IActionResult saveSection(SectionCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_section",model,_dbCon);

                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }
    }
}