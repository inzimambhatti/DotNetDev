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
    public class ContactController : ControllerBase
    {
        private readonly IOptions<conStr> _dbCon;
        private string cmd;

        public ContactController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        }

        [HttpGet("getContactType")]
        public IActionResult getContactType()
        {
            try
            {
                cmd = "SELECT contact_type_id,contact_type_title FROM tbl_contact_type_1";
                var appMenu = dapperQuery.Qry<ContactType>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }
        
    }
}