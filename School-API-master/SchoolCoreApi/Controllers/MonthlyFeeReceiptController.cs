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
    public class MonthlyFeeReceiptController : ControllerBase
    {
        private readonly IOptions<conStr> _dbCon;
        private string cmd;

        public MonthlyFeeReceiptController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        }

        [HttpGet("getFeeInformation")]
        public IActionResult getFeeInformation(int studentID)
        {
            try
            {
                cmd = "SELECT * FROM view_feeInformation where studentID = " + studentID + "";
                
                var appMenu = dapperQuery.Qry<MonthlyFeeInformation>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }
        
        [HttpGet("getFeeCollection")]
        public IActionResult getFeeCollection(int sectionID, int classID, int branch_department_section_id, int month, int year)
        {
            try
            {

               if (sectionID == 0 && branch_department_section_id != 0 && classID != 0)
                {
                    cmd = "SELECT * FROM fun_feeCollection("+year+","+month+") where branch_department_section_id = " + branch_department_section_id + " and classID = " + classID + " and month(studentMonthlyFeesDate) ="+month+" and year(studentMonthlyFeesDate) ="+year+" Order by studentName ASC";
                }
                else if(sectionID == 0 && classID == 0 && branch_department_section_id != 0)
                {
                    cmd = "SELECT * FROM fun_feeCollection("+year+","+month+") where branch_department_section_id = " + branch_department_section_id + " and month(studentMonthlyFeesDate) ="+month+" and year(studentMonthlyFeesDate) ="+year+" Order by studentName ASC";
                }
                else
                {
                    cmd = "SELECT * FROM fun_feeCollection("+year+","+month+") where branch_department_section_id = " + branch_department_section_id + " and classID = " + classID + " AND sectionID = " + sectionID + " and month(studentMonthlyFeesDate) ="+month+" and year(studentMonthlyFeesDate) ="+year+" Order by studentName ASC";
                }
                
                var appMenu = dapperQuery.Qry<feeCollection>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }
        [HttpGet("getFeeCollectionChild")]
        public IActionResult getFeeCollectionChild(int sectionID, int classID, int branch_department_section_id, int month, int year)
        {
            try
            {

               if (sectionID == 0 && branch_department_section_id != 0 && classID != 0)
                {
                    cmd = "SELECT * FROM view_feeCollectionChild where branch_department_section_id = " + branch_department_section_id + " and classID = " + classID + " and month(studentMonthlyFeesDate) ="+month+" and year(studentMonthlyFeesDate) ="+year+" ";
                }
                else if(sectionID == 0 && classID == 0 && branch_department_section_id != 0)
                {
                    cmd = "SELECT * FROM view_feeCollectionChild where branch_department_section_id = " + branch_department_section_id + " and month(studentMonthlyFeesDate) ="+month+" and year(studentMonthlyFeesDate) ="+year+"";
                }
                else
                {
                    cmd = "SELECT * FROM view_feeCollectionChild where branch_department_section_id = " + branch_department_section_id + " and classID = " + classID + " AND sectionID = " + sectionID + " and month(studentMonthlyFeesDate) ="+month+" and year(studentMonthlyFeesDate) ="+year+"";
                }
                
                var appMenu = dapperQuery.Qry<FeeCollectionChild>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getFeeCollectionDetails")]
        public IActionResult getFeeCollectionDetails(int sectionID, int classID, int branch_department_section_id)
        {
            try
            {
                if (sectionID == 0 && branch_department_section_id != 0 && classID != 0)
                {
                    cmd = "SELECT * FROM view_feeCollectionDetails where branch_department_section_id = " + branch_department_section_id + " and classID = " + classID + "";
                }
                else if(sectionID == 0 && classID == 0 && branch_department_section_id != 0)
                {
                    cmd = "SELECT * FROM view_feeCollectionDetails where branch_department_section_id = " + branch_department_section_id + "";
                }
                else
                {
                    cmd = "SELECT * FROM view_feeCollectionDetails where branch_department_section_id = " + branch_department_section_id + " and classID = " + classID + " AND sectionID = " + sectionID + "";
                }
                              
                var appMenu = dapperQuery.Qry<FeeCollectionDetails>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getFeeCollectedDetail")]
        public IActionResult getFeeCollectedDetail()
        {
            try
            {
                    cmd = "SELECT * FROM view_feeCollectionCollected";
                var appMenu = dapperQuery.Qry<FeeCollectionCollected>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getRemainingFeeCollection")]
        public IActionResult getRemainingFeeCollection()
        {
            try
            {
                    cmd = "SELECT * FROM view_remainingDues";
                
                var appMenu = dapperQuery.Qry<RemainingFeeCollection>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getTotalFeeCollection")]
        public IActionResult getTotalFeeCollection()
        {
            try
            {
                cmd = "SELECT * FROM view_totalFeeCollection";
                var appMenu = dapperQuery.Qry<TotalFeeCollection>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("saveMonthlyFeeReceipt")]
        public IActionResult saveMonthlyFeeReceipt(MonthlyFeeReceiptCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_feeReceipt",model,_dbCon);
                
                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }   
        
        [HttpPost("saveFeeCollection")]
        public IActionResult saveFeeCollection(FeeCollectionCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_feeCollection",model,_dbCon);
                
                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }   

        [HttpPost("saveFeeImport")]
        public IActionResult saveFeeImport(FeeImportCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_feeCollectionImport",model,_dbCon);
                
                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }
        
    }
}