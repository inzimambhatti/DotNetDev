using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FMISApi.Services;
using Microsoft.Extensions.Options;
using FMISApi.Configuration;
using FMISApi.Entities;
using Dapper;
using System.Data;
using Newtonsoft.Json;

namespace FMISApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FinancialYearController : ControllerBase
    {
        private readonly IOptions<conStr> _dbCon;
        private string cmd;

        public FinancialYearController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        }

        [HttpGet("getFinancialYear")]
        public IActionResult getFinancialYear()
        {
            try
            {
                cmd = "SELECT * FROM view_financialYear";
                var appMenu = dapperQuery.Qry<FinancialYear>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("saveFinancialYear")]
        public IActionResult saveFinancialYear(FinancialYearCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_financialYear",model,_dbCon);

                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }

    }
}