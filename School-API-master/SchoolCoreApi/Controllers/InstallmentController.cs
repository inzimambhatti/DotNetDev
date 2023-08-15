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
    public class InstallmentController : ControllerBase
    {
        private readonly IOptions<conStr> _dbCon;
        private string cmd;

        public InstallmentController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        }

        [HttpGet("getFeeInstallment")]
        public IActionResult getFeeInstallment()
        {
            try
            {
                cmd = "SELECT * FROM tb_stl_fee_installment_fee where isDeleted=0";
                var appMenu = dapperQuery.Qry<ClassReg>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }
    
        [HttpPost("saveInstallment")]
        public IActionResult saveInstallment(InstallmentCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_addInstallment",model,_dbCon);

                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }
    }
}