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
    public class SectionController : ControllerBase
    {
        private readonly IOptions<conStr> _dbCon;
        private string cmd;

        public SectionController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        }

        [HttpGet("getSections")]
        public IActionResult getSections()
        {
            try
            { 
                cmd = "SELECT * FROM view_section";
                var appMenu = dapperQuery.Qry<section>(cmd, _dbCon);
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
                var response = dapperQuery.SPReturn("sp_sectionCrud",model,_dbCon);

                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }
    }
}