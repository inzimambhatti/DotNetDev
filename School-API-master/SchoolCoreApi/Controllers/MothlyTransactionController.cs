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
    public class MonthlyTransactionController : ControllerBase
    {
        private readonly IOptions<conStr> _dbCon;
        private string cmd;

        public MonthlyTransactionController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        }

        [HttpGet("getTransactionHistory")]
        public IActionResult getTransactionHistory()
        {
            try
            {
                cmd = "SELECT DISTINCT * FROM view_monthlyTransactionHistory Order by feesTransactionID Desc";
                var appMenu = dapperQuery.Qry<TransactionHistory>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getAppliedUnAppliedTransaction")]
        public IActionResult getAppliedUnAppliedTransaction(int feesTransactionID,int branchID,int classID,int sectionID)
        {
            try
            {
                if (sectionID == 0 && branchID != 0 && classID != 0)
                {
                    cmd = "SELECT * FROM fun_appliedUnAppliedTransaction("+feesTransactionID+") where branch_department_section_id = " + branchID + " and classID = " + classID + "";
                }
                else if(sectionID == 0 && classID == 0 && branchID != 0)
                {
                    cmd = "SELECT * FROM fun_appliedUnAppliedTransaction("+feesTransactionID+") where branch_department_section_id = " + branchID + "";
                }
                else
                {
                    cmd = "SELECT * FROM fun_appliedUnAppliedTransaction("+feesTransactionID+") where branch_department_section_id = " + branchID + " and classID = " + classID + " AND sectionID = " + sectionID + "";
                }
           
                var appMenu = dapperQuery.Qry<AppliedUnAppliedTransaction>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getUnAppliedTransaction")]
        public IActionResult getUnAppliedTransaction()
        {
            try
            {
                cmd = "SELECT * FROM view_unAppliedTransaction";
                var appMenu = dapperQuery.Qry<UnAppliedTransaction>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("saveMonthlyTransaction")]
        public IActionResult saveMonthlyTransaction(MonthlyTransactionCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_studentMonthlyFeesTransaction",model,_dbCon);
                
                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("saveMonthlyTransactionDetails")]
        public IActionResult saveMonthlyTransactionDetails(MonthlyTransactionDetailCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_monthlyFeesTransactionDetails",model,_dbCon);
                
                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }
    }
}