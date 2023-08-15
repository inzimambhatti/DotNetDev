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
    public class AnnualFundController : ControllerBase
    {
        private readonly IOptions<conStr> _dbCon;
        private string cmd;

        public AnnualFundController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        }
        
        [HttpGet("getAnnualElements")]
        public IActionResult getAnnualElements()
        {
            try
            {
                cmd = @"select e.feesElementID,e.feesElementTitle,e.feesElementTypeID from tbl_elements_h e
                        inner join tbl_fees_element_type_h fe on fe.feesElementTypeID = e.feesElementTypeID
                        where fe.feesElementTypeID = 47 and isDeleted = 0";
                var appMenu = dapperQuery.Qry<AnnualElements>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getSessionMonth")]
        public IActionResult getSessionMonth(int schoolSessionID,int annualFundID,string fromDate)
        {
            try
            {
                if (schoolSessionID == 0)
                {
                    cmd = @"select DISTINCT Month(d.calendarDate) as [month],YEAR(d.calendarDate) as [year] 
                            from tbl_annual_fund_detail afd
                            inner join tbl_annual_fund af on af.annualFundID = afd.annualFundID
                            inner join tbl_school_session ss on ss.schoolSessionID = af.schoolSessionID
                            inner join dimcalendar d on d.schoolSessionID = ss.schoolSessionID
                            where af.annualFundID = " + annualFundID + @" and cast(d.calendarDate as date) >= cast('" + fromDate + @"' as date)
                            and ss.isDeleted = 0 ";
                }
                else if (annualFundID == 0)
                {
                    cmd = @"select DISTINCT Month(d.calendarDate) as [month],YEAR(d.calendarDate) as [year] 
                            from tbl_school_session ss 
                            inner join dimcalendar d on d.schoolSessionID = ss.schoolSessionID
                            where ss.schoolSessionID = " + schoolSessionID + @" 
                            and ss.isDeleted = 0 
                            order by YEAR(d.calendarDate) ASC";
                }
                    
                var appMenu = dapperQuery.Qry<SessionMonths>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getStudentFunds")]
        public IActionResult getStudentFunds(int studentID,int annualFundID)
        {
            try
            {
                cmd = @"select DISTINCT af.annualFundID,af.annualFundTitle,smf.studentMonthlyFeesDate ,
                        MONTH(smf.studentMonthlyFeesDate) as [month],DATENAME(MONTH,smf.studentMonthlyFeesDate) as [monthName]
                        from tbl_student_monthly_fees_h smf
                        inner join tbl_student_monthly_fees_detail_h smfd on smfd.studentMonthlyFeesID = smf.studentMonthlyFeesID
                        inner join tbl_annual_fund af on af.feesElementID = smfd.feesElementID
                        inner join tbl_annual_fund_detail afd on afd.annualFundID = af.annualFundID
                        where smf.studentID = " + studentID + @" and af.annualFundID = " + annualFundID + @" and smf.closingFlag = 0
                        and smf.isDeleted = 0 and smfd.isDeleted = 0 and af.isDeleted = 0 and afd.isDeleted = 0";
                var appMenu = dapperQuery.Qry<StudentFunds>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getStudentFeesMonth")]
        public IActionResult getStudentFeesMonth(int studentID,int annualFundID, string curDate)
        {
            try
            {
                cmd = @"select * from fun_studentFeesMonths(" + annualFundID + "," + studentID + ",'" + curDate + "') order by [year]";
                var appMenu = dapperQuery.Qry<StudentFeeMonth>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getStudentAnnualFundDetail")]
        public IActionResult getStudentAnnualFundDetail(int branch_department_section_id,int departmentTypeID,int classID,int sectionID,int annualFundID)
        {
            try
            {
                cmd = @"DECLARE	@return_value int

                        EXEC	@return_value = [dbo].[sp_studentAnnualFundDetail]
                                @branch_department_section_id = " + branch_department_section_id + @",
                                @departmentTypeID = " + departmentTypeID + @",
                                @classID = " + classID + @",
                                @sectionID = " + sectionID + @",
                                @annualFundID = " + annualFundID + @"";
                var appMenu = dapperQuery.Qry<StudentAnnualFundDetail>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getBranchAnnualFund")]
        public IActionResult getBranchAnnualFund(int branch_department_section_id)
        {
            try
            {
                cmd = @"select DISTINCT annualFundID,annualFundTitle,noOfInstallments from view_tbl_annualFund where branch_department_section_id = " + branch_department_section_id + "";
                
                var appMenu = dapperQuery.Qry<AnnualFund>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getInstallmentAmount")]
        public IActionResult getInstallmentAmount(int studentID,int annualFundID)
        {
            try
            {  
                cmd = @"select DISTINCT * from fun_annualInstallmentAmount(" + studentID + "," + annualFundID + ")";
                
                var appMenu = dapperQuery.Qry<AnnualInstallmentAmount>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getAnnualFund")]
        public IActionResult getAnnualFund(int annualFundID)
        {
            try
            {
                if (annualFundID == 0)
                {
                    cmd = @"select DISTINCT annualFundID,annualFundTitle,noOfInstallments,schoolSessionID,schoolSessionTitle,branch_department_section_id,branch_name from view_tbl_annualFund ";
                }
                else
                {
                    cmd = @"select DISTINCT * from view_tbl_annualFund where annualFundID = " + annualFundID + " ";
                }
                    
                
                var appMenu = dapperQuery.Qry<AnnualFund>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("saveAnnualFund")]
        public IActionResult saveAnnualFund(AnnualFundCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_annualFund",model,_dbCon);
                
                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("saveAnnualFundDetail")]
        public IActionResult saveAnnualFundDetail(AnnualFundDetailCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_annualFundDetail",model,_dbCon);
                
                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("saveStudentAnnualFund")]
        public IActionResult saveStudentAnnualFund(StudentAnnualFundCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_studentAnnualFund",model,_dbCon);
                
                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }
    
        
    }
}