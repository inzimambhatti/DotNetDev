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
    public class FeeController : ControllerBase
    {
        private readonly IOptions<conStr> _dbCon;
        private string cmd;

        public FeeController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        }
    
        [HttpGet("getFeePlanDetail")]
        public IActionResult getFeePlanDetail(int feeElementsID)
        {
            try
            {
                cmd = "SELECT * FROM view_feePlan where feeElementsID = "+feeElementsID+"";
                var appMenu = dapperQuery.Qry<FeePlan>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getFeeByStudent")]
        public IActionResult getFeeByStudent(int studentID,int year)
        {
            try
            {
                cmd = @"select DISTINCT smf.studentID,month(smf.studentMonthlyFeesDate) as [month],YEAR(smf.studentMonthlyFeesDate) as [year],
                        SUM(smfd.studentMonthlyFeesAmount) as totalAmount,
                        case when smf.closingFlag = 1 then 'Paid' else 'Due' end [status]
                        from tbl_student_monthly_fees_h smf inner join 
                        tbl_student_monthly_fees_Detail_h smfd on smfd.studentMonthlyFeesID = smf.studentMonthlyFeesID 
                        where studentID = " + studentID + @" 
                        and smf.isDeleted = 0 and smfd.isDeleted = 0 and smf.isGenerated = 1
                        and smf.isPublished = 1 and YEAR(smf.studentMonthlyFeesDate) = " + year + @"
                        GROUP BY smf.studentID,smf.studentMonthlyFeesDate,smf.closingFlag";
                var appMenu = dapperQuery.Qry<StudentFeeInfo>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getFeeReconciliationTotal")]
        public IActionResult getFeeReconciliationTotal(int oldMonth,int oldYear,int newMonth,int newYear,int branch_department_section_id,int departmentTypeID,int classID,int sectionID)
        {
            try
            {
                cmd = @"DECLARE	@return_value int

                        EXEC	@return_value = [dbo].[sp_feeReconciliationTotal]
                                @oldMonth = " + oldMonth + @",
                                @oldYear = " + oldYear + @",
                                @newMonth = " + newMonth + @",
                                @newYear = " + newYear + @",
                                @branch_department_section_id = " + branch_department_section_id + @",
                                @departmentTypeID = " + departmentTypeID + @",
                                @classID = " + classID + @",
                                @sectionID = " + sectionID + " ";
                var appMenu = dapperQuery.Qry<TotalOfAmount>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getFeeReconciliationElement")]
        public IActionResult getFeeReconciliationElement(int oldMonth,int oldYear,int newMonth,int newYear,int branch_department_section_id,int departmentTypeID,int classID,int sectionID)
        {
            try
            {
                if (branch_department_section_id == 0 && departmentTypeID == 0 && classID == 0 && sectionID == 0)
                {
                    cmd = "select DISTINCT feesElementID,feesElementTitle,sum(amount) as amount,nature from fun_feeReconciliationView(" + oldMonth + "," + oldYear + "," + newMonth + "," + newYear + ") GROUP BY feesElementID,feesElementTitle,nature ";
                }
                else if (branch_department_section_id != 0 && departmentTypeID == 0 && classID == 0 && sectionID == 0)
                {
                    cmd = "select DISTINCT feesElementID,feesElementTitle,sum(amount) as amount,nature from fun_feeReconciliationView(" + oldMonth + "," + oldYear + "," + newMonth + "," + newYear + ") where branch_department_section_id = " + branch_department_section_id + " GROUP BY feesElementID,feesElementTitle,nature ";
                }
                else if (branch_department_section_id != 0 && departmentTypeID != 0 && classID == 0 && sectionID == 0)
                {
                    cmd = "select DISTINCT feesElementID,feesElementTitle,sum(amount) as amount,nature from fun_feeReconciliationView(" + oldMonth + "," + oldYear + "," + newMonth + "," + newYear + ") where branch_department_section_id = " + branch_department_section_id + " and departmentTypeID = " + departmentTypeID + " GROUP BY feesElementID,feesElementTitle,nature ";
                }
                else if (branch_department_section_id != 0 && departmentTypeID != 0 && classID != 0 && sectionID == 0)
                {
                    cmd = "select DISTINCT feesElementID,feesElementTitle,sum(amount) as amount,nature from fun_feeReconciliationView(" + oldMonth + "," + oldYear + "," + newMonth + "," + newYear + ") where branch_department_section_id = " + branch_department_section_id + " and departmentTypeID = " + departmentTypeID + " and classID = " + classID + " GROUP BY feesElementID,feesElementTitle,nature ";
                }
                else if (branch_department_section_id != 0 && departmentTypeID != 0 && classID != 0 && sectionID != 0)
                {
                    cmd = "select DISTINCT feesElementID,feesElementTitle,sum(amount) as amount,nature from fun_feeReconciliationView(" + oldMonth + "," + oldYear + "," + newMonth + "," + newYear + ") where branch_department_section_id = " + branch_department_section_id + " and departmentTypeID = " + departmentTypeID + " and classID = " + classID + " and sectionID = " + sectionID + " GROUP BY feesElementID,feesElementTitle,nature ";
                }
                
                var appMenu = dapperQuery.Qry<ReconciliationElement>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getStudentFeeReconciliation")]
        public IActionResult getStudentFeeReconciliation(string nature,int feesElementID,int oldMonth,int oldYear,int newMonth,int newYear,int branch_department_section_id,int departmentTypeID,int classID,int sectionID)
        {
            try
            {
                if (branch_department_section_id == 0 && departmentTypeID == 0 && classID == 0 && sectionID == 0)
                {
                    cmd = @"select DISTINCT studentID,studentName,studentRegistrationCode,feesElementID,feesElementTitle,
                            sum(amount) as amount,nature from fun_feeReconciliationView(" + oldMonth + @"," + oldYear + @"," + newMonth + @"," + newYear + @") 
                            where feesElementID = " + feesElementID + @" and nature = '" + nature + @"'
                            GROUP BY feesElementID,feesElementTitle,nature,studentID,studentName,studentRegistrationCode ";
                }
                else if (branch_department_section_id != 0 && departmentTypeID == 0 && classID == 0 && sectionID == 0)
                {
                    cmd = @"select DISTINCT studentID,studentName,studentRegistrationCode,feesElementID,feesElementTitle,
                            sum(amount) as amount,nature from fun_feeReconciliationView(" + oldMonth + @"," + oldYear + @"," + newMonth + @"," + newYear + @") 
                            where feesElementID = " + feesElementID + @" and nature = '" + nature + @"' and branch_department_section_id = " + branch_department_section_id + @"
                            GROUP BY feesElementID,feesElementTitle,nature ,studentID,studentName,studentRegistrationCode";
                }
                else if (branch_department_section_id != 0 && departmentTypeID != 0 && classID == 0 && sectionID == 0)
                {
                    cmd = @"select DISTINCT studentID,studentName,studentRegistrationCode,feesElementID,feesElementTitle,
                            sum(amount) as amount,nature from fun_feeReconciliationView(" + oldMonth + @"," + oldYear + @"," + newMonth + @"," + newYear + @") 
                            where feesElementID = " + feesElementID + @" and nature = '" + nature + @"' and branch_department_section_id = " + branch_department_section_id + @" and departmentTypeID = " + departmentTypeID + @"
                            GROUP BY feesElementID,feesElementTitle,nature,studentID,studentName,studentRegistrationCode ";
                }
                else if (branch_department_section_id != 0 && departmentTypeID != 0 && classID != 0 && sectionID == 0)
                {
                    cmd = @"select DISTINCT studentID,studentName,studentRegistrationCode,feesElementID,feesElementTitle,
                            sum(amount) as amount,nature from fun_feeReconciliationView(" + oldMonth + @"," + oldYear + @"," + newMonth + @"," + newYear + @") 
                            where feesElementID = " + feesElementID + @" and nature = '" + nature + @"' and branch_department_section_id = " + branch_department_section_id + @" and departmentTypeID = " + departmentTypeID + @" and classID = " + classID + @"
                            GROUP BY feesElementID,feesElementTitle,nature,studentID,studentName,studentRegistrationCode ";
                }
                else if (branch_department_section_id != 0 && departmentTypeID != 0 && classID != 0 && sectionID != 0)
                {
                    cmd = @"select DISTINCT studentID,studentName,studentRegistrationCode,feesElementID,feesElementTitle,
                            sum(amount) as amount,nature from fun_feeReconciliationView(" + oldMonth + @"," + oldYear + @"," + newMonth + @"," + newYear + @") 
                            where feesElementID = " + feesElementID + @" and nature = '" + nature + @"' and branch_department_section_id = " + branch_department_section_id + @" and departmentTypeID = " + departmentTypeID + @" and classID = " + classID + @" and sectionID = " + sectionID + @"
                            GROUP BY feesElementID,feesElementTitle,nature,studentID,studentName,studentRegistrationCode  ";
                }
                
                var appMenu = dapperQuery.Qry<StudentReconciliation>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }


        [HttpGet("getFeePlanTotal")]
        public IActionResult getFeePlanTotal()
        {
            try
            {
                cmd = "SELECT * FROM view_feePlanTotalAmount";
                var appMenu = dapperQuery.Qry<FeePlanDetailList>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        // [HttpGet("getFeePlan")]
        // public IActionResult getFeePlan()
        // {
        //     try
        //     {
        //         cmd = "SELECT * FROM view_feePlan";
        //         var appMenu = dapperQuery.Qry<FeePlan>(cmd, _dbCon);
        //         return Ok(appMenu);
        //     }
        //     catch (Exception e)
        //     {
        //         return Ok(e);
        //     }
        // }

        [HttpGet("getFeeProgramType")]
        public IActionResult getFeeProgramType()
        {
            try
            {
                cmd = "SELECT * FROM tbl_fee_Program_type where isDeleted=0";
                var appMenu = dapperQuery.Qry<ProgramType>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getFeePlanType")]
        public IActionResult getFeePlanType()
        {
            try
            {
                cmd = "SELECT * FROM tbl_fee_plan_type where isDeleted=0";
                var appMenu = dapperQuery.Qry<PlanType>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getFeeElement")]
        public IActionResult getFeeElement(int branchID,int feeProgramTypeID,int feePlanTypeID)
        {
            try
            {
                cmd = "SELECT * FROM tbl_fee_elements where branch_id = " + branchID + " and feeProgramTypeID = " + feeProgramTypeID + " and feePlanTypeID = " + feePlanTypeID + "";
                var appMenu = dapperQuery.Qry<FeeElement>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getFeesPlan")]
        public IActionResult getFeesPlan ( int feesPlanID){
            try
            {
                cmd = "SELECT * FROM tbl_fees_plan_h where feesPlanID = "+ feesPlanID +"";
                var appMenu = dapperQuery.Qry<FeePlan>(cmd , _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                
                return Ok (e);
            }
        }

        [HttpGet("getBranchFee")]
        public IActionResult getBranchFee()
        {
            try
            {
                cmd = "SELECT * FROM View_BranchFee";
                var appMenu = dapperQuery.Qry<BranchFee>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("saveFeeConfiguration")]
        public IActionResult saveFeeConfiguration(FeeConfigurationCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_feeConfigurationCrud",model,_dbCon);

                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("getElementTypeList")]
        public IActionResult getElementTypeList(FeeElementTypeListCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_elementTypeList", model, _dbCon);

                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }
        
        [HttpPost("getfeesPlanDetailList")]
        public IActionResult getfeesPlanDetailList(feesPlanDetailListCreation model){
            try
            {
               var response = dapperQuery.SPReturn("sp_feesPlanDetailList", model , _dbCon);
               return Ok(response); 
            }
            catch (Exception e)
            {
                
                return Ok(e);
            }
        }

       [HttpPost("saveBranchFee")]
        public IActionResult saveBranchFee(BranchFeeCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_branchFee",model,_dbCon);

                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }

    }

}