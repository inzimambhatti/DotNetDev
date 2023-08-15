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
    public class FeeElementTypeController : ControllerBase
    {
        private readonly IOptions<conStr> _dbCon;
        private string cmd;

        public FeeElementTypeController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        }

        [HttpGet("getFeeElementType")]
        public IActionResult getFeeElementType(int feesElementTypeID)
        {
            try
            {
                if(feesElementTypeID == 0){
                    cmd = "SELECT * FROM tbl_fees_element_type_h where isDeleted = 0";
                }else{
                    cmd = "SELECT * FROM tbl_fees_element_type_h where feesElementTypeID = " + feesElementTypeID + "";
                }
                
                var appMenu = dapperQuery.Qry<FeeElementType>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("saveFeeElementType")]
        public IActionResult saveFeeElementType(FeeElementTypeCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_fee_element_type_h",model,_dbCon);

                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }
    }
}