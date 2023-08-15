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
    public class ChartOfAccountController : ControllerBase
    {
        private readonly IOptions<conStr> _dbCon;
        private string cmd;

        public ChartOfAccountController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        }

        [HttpGet("getChartOfAccount")]
        public IActionResult getChartOfAccount()
        {
            try
            {
                cmd = "SELECT * FROM view_chartOfAccount";
                var appMenu = dapperQuery.Qry<ChartOfAccount>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("saveChartOfAccount")]
        public IActionResult saveChartOfAccount(ChartOfAccountCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_chartOfAccount",model,_dbCon);

                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("saveChartOfAccountChildLevel")]
        public IActionResult saveChartOfAccountChildLevel(ChartOfAccountChildLevelCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_chartOfAccountChildLevel",model,_dbCon);

                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }

    }
}