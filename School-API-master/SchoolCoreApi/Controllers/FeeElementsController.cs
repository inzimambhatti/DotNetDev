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
    public class FeeElementsController : ControllerBase
    {
        private readonly IOptions<conStr> _dbCon;
        private string cmd;

        public FeeElementsController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        }
        [HttpGet("getFeeElement")]
        public IActionResult getFeeElement(int feesElementID)
        {
            try
            {
                if(feesElementID == 0){
                    cmd = "SELECT * FROM view_feesElementList";
                }else{
                    cmd = "SELECT * FROM view_feesElementList where feesElementID = "+feesElementID+"";
                }
                
                var appMenu = dapperQuery.Qry<FeeElement>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("saveFeeElement")]
        public IActionResult saveFeeElement(FeeElementCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_feesElements",model,_dbCon);

                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }
       
    }
}