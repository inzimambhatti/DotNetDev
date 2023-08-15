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
    public class RegistrationFeeController : ControllerBase
    {
        private readonly IOptions<conStr> _dbCon;
        private string cmd;

        public RegistrationFeeController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        }
        
        [HttpGet("getNormalFeePlan")]
        public IActionResult getNormalFeePlan(int branch_id,int feeProgramTypeID)
        {
            try
            {
                cmd = "SELECT * FROM view_NormalFeePlan where branch_id = " + branch_id + " and feeProgramTypeID = " + feeProgramTypeID + "";
                var appMenu = dapperQuery.Qry<NormalFeePlan>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getAdmissionFeeInstallment")]
        public IActionResult getAdmissionFeeInstallment(int branch_id, int feeProgramTypeID)
        {
            try
            {
                cmd = "SELECT  ROW_NUMBER() Over (Order by FeeMasterPlanID) As rowNo,* FROM view_AdmissionFeeInstallment where branch_id = " + branch_id + " and feeProgramTypeID = " + feeProgramTypeID + "";
                var appMenu = dapperQuery.Qry<AdmissionFeeInstallment>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }
        
        [HttpGet("getAnnualFeeInstallment")]
        public IActionResult getAnnualFeeInstallment(int branch_id,int feeProgramTypeID)
        {
            try
            {
                cmd = "SELECT  ROW_NUMBER() Over (Order by FeeMasterPlanID) As rowNo,* FROM view_annualFeeInstallment where branch_id = " + branch_id + " and feeProgramTypeID = " + feeProgramTypeID + "";
                var appMenu = dapperQuery.Qry<AnnualFeeInstallment>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getStudentFeePlanList")]
        public IActionResult getStudentFeePlanList()
        {
            try
            {
                cmd = "SELECT  * FROM view_studentFeesPlanList";
                var appMenu = dapperQuery.Qry<StudentFeePlanList>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }        

        [HttpPost("getFeesPlanDetails")] 
        public IActionResult getFeesPlanDetails(StudentFeePlanDetails model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_get_student_fee_config", model, _dbCon);
                return Ok(response);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getFeePlanListAll")]
        public IActionResult getFeePlanListAll()
        {
            try
            {
                cmd = "SELECT * FROM tbl_fees_plan_h where isActive=1";
                var appMenu = dapperQuery.Qry<FeesPlanListAll>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getStudentFeePlan")]
        public IActionResult getStudentFeePlan(int studentFeesPlanDetailID)
        {
            try
            {
                
                cmd = "select * from view_studentFeesPlanDetails where studentFeesPlanDetailID = " + studentFeesPlanDetailID + "";
                var appMenu = dapperQuery.Qry<StudentFeePlan>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getEmployeeChild")]
        public IActionResult getEmployeeChild()
        {
            try
            {
                
                cmd = "select * from fun_employeeChild()";
                var appMenu = dapperQuery.Qry<EmployeeChild>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }   

        [HttpGet("getSiblingDiscount")]
        public IActionResult getSiblingDiscount(int studentID)
        {
            try
            {
                
                cmd = "select * from fun_siblingDiscount() where studentID = "+studentID+"";
                var appMenu = dapperQuery.Qry<SiblingDiscount>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }       

        [HttpPost("saveFeeRegistration")]
        public IActionResult saveFeeRegistration(FeeRegistrationCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_feeGeneration",model,_dbCon);
                
                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("saveStudentFeePlan")]
        public IActionResult saveStudentFeePlan(StudentFeePlanCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_studentFeesPlanDetails",model,_dbCon);
                
                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }

    }
}