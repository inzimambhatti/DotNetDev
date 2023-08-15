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
    public class FeeDashboardController : ControllerBase
    {
        private readonly IOptions<conStr> _dbCon;
        private string cmd;

        public FeeDashboardController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        }

        [HttpGet("getFeeTotalStudents")]
        public IActionResult getFeeTotalStudents(int branchID,int month,int year)
        {
            try
            {
                if (branchID == 0)
                {
                    cmd = @"select sum(totalStudent) as totalStudent,
                            sum(totalAmount) as totalAmount,sum(paidFee) as paidFee,Sum(paidFeeStudent) as paidFeeStudent,
                            Sum(RemainingFee) as RemainingFee,Sum(RemainingFeeStudent) as RemainingFeeStudent, 
                            sum(lateFine) as lateFine,Sum(lateFineStudents) as lateFineStudents
                            from fun_feeTotalStudent(" + year + @"," + month + @")";
                }
                else
                {
                    cmd = "select * from fun_feeTotalStudent(" + year + "," + month + ")  where branch_department_section_id = " + branchID + " ";
                }

                var appMenu = dapperQuery.Qry<TotalStudentsFee>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getFeeElementsCollection")]
        public IActionResult getFeeElementsCollection(int branchID,int month,int year)
        {
            try
            {
                if (branchID == 0)
                {
                    cmd = @"select smfd.feesElementID,e.feesElementTitle,SUM(smfd.studentMonthlyFeesAmount) as totalAmount,
                            (select SUM(subsmfd.studentMonthlyFeesAmount) from tbl_student_monthly_fees_h subsmf Inner Join tbl_student_monthly_fees_detail_h subsmfd on subsmfd.studentMonthlyFeesID = subsmf.studentMonthlyFeesID
                            Inner Join tbl_student subs on subs.studentID = subsmf.studentID Inner Join tbl_student_class subsc on subsc.studentID = subs.studentID Inner Join tbl_class_section subcs on subcs.classSectionID = subsc.classID Inner Join tbl_class_department subcd on subcd.classDepartmentID = subcs.classDepartmentID
                            where subsmf.isDeleted = 0 and subsmfd.isDeleted = 0 and subs.isDeleted = 0 and subsc.isDeleted = 0 and subcs.isDeleted = 0 and subcd.isDeleted = 0 and subsmf.isGenerated = 1 and subsmf.isPublished = 1 
                            and subsmf.closingFlag = 1 and (subsc.studentClassFlag = 1 OR subsc.studentClassFlag is null) and MONTH(subsmf.studentMonthlyFeesDate) = MONTH(smf.studentMonthlyFeesDate) 
                            and YEAR(subsmf.studentMonthlyFeesDate) = YEAR(smf.studentMonthlyFeesDate) and subsmfd.feesElementID = smfd.feesElementID) as paidFee from tbl_student_monthly_fees_h smf Inner Join tbl_student_monthly_fees_detail_h smfd on smfd.studentMonthlyFeesID = smf.studentMonthlyFeesID 
                            Inner Join tbl_student s on s.studentID = smf.studentID Inner Join tbl_student_class sc on sc.studentID = s.studentID Inner Join tbl_class_section cs on cs.classSectionID = sc.classID Inner Join tbl_class_department cd on cd.classDepartmentID = cs.classDepartmentID
                            Inner Join tbl_elements_h e on e.feesElementID = smfd.feesElementID where smf.isDeleted = 0 and smfd.isDeleted = 0 and s.isDeleted = 0 and sc.isDeleted = 0 and cs.isDeleted = 0 and cd.isDeleted = 0 and smf.isGenerated = 1 and smf.isPublished = 1 
                            and MONTH(smf.studentMonthlyFeesDate) = " + month + @" and YEAR(smf.studentMonthlyFeesDate) = " + year + @" and (sc.studentClassFlag = 1 OR sc.studentClassFlag is null) and smfd.studentMonthlyFeesAmount > 0 
                            GROUP BY smfd.feesElementID,e.feesElementTitle,smf.studentMonthlyFeesDate";
                }
                else
                {
                    cmd = @"select smfd.feesElementID,e.feesElementTitle,SUM(smfd.studentMonthlyFeesAmount) as totalAmount,cd.branch_department_section_id,
                            (select SUM(subsmfd.studentMonthlyFeesAmount) from tbl_student_monthly_fees_h subsmf Inner Join tbl_student_monthly_fees_detail_h subsmfd on subsmfd.studentMonthlyFeesID = subsmf.studentMonthlyFeesID
                            Inner Join tbl_student subs on subs.studentID = subsmf.studentID Inner Join tbl_student_class subsc on subsc.studentID = subs.studentID Inner Join tbl_class_section subcs on subcs.classSectionID = subsc.classID Inner Join tbl_class_department subcd on subcd.classDepartmentID = subcs.classDepartmentID
                            where subsmf.isDeleted = 0 and subsmfd.isDeleted = 0 and subs.isDeleted = 0 and subsc.isDeleted = 0 and subcs.isDeleted = 0 and subcd.isDeleted = 0 and subsmf.isGenerated = 1 and subsmf.isPublished = 1 
                            and subsmf.closingFlag = 1 and subcd.branch_department_section_id = cd.branch_department_section_id and (subsc.studentClassFlag = 1 OR subsc.studentClassFlag is null) and MONTH(subsmf.studentMonthlyFeesDate) = MONTH(smf.studentMonthlyFeesDate) 
                            and YEAR(subsmf.studentMonthlyFeesDate) = YEAR(smf.studentMonthlyFeesDate) and subsmfd.feesElementID = smfd.feesElementID) as paidFee from tbl_student_monthly_fees_h smf Inner Join tbl_student_monthly_fees_detail_h smfd on smfd.studentMonthlyFeesID = smf.studentMonthlyFeesID 
                            Inner Join tbl_student s on s.studentID = smf.studentID Inner Join tbl_student_class sc on sc.studentID = s.studentID Inner Join tbl_class_section cs on cs.classSectionID = sc.classID Inner Join tbl_class_department cd on cd.classDepartmentID = cs.classDepartmentID
                            Inner Join tbl_elements_h e on e.feesElementID = smfd.feesElementID where smf.isDeleted = 0 and smfd.isDeleted = 0 and s.isDeleted = 0 and sc.isDeleted = 0 and cs.isDeleted = 0 and cd.isDeleted = 0 and smf.isGenerated = 1 and smf.isPublished = 1 
                            and MONTH(smf.studentMonthlyFeesDate) = " + month + @" and YEAR(smf.studentMonthlyFeesDate) = " + year + @" and (sc.studentClassFlag = 1 OR sc.studentClassFlag is null) and smfd.studentMonthlyFeesAmount > 0 
                            and cd.branch_department_section_id = " + branchID + @"
                            GROUP BY smfd.feesElementID,e.feesElementTitle,cd.branch_department_section_id,smf.studentMonthlyFeesDate";
                }

                var appMenu = dapperQuery.Qry<FeeElementsCollection>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("get12MonthFee")]
        public IActionResult get12MonthFee(int branchID,int month,int year)
        {
            try
            {
                if (branchID == 0)
                {
                    cmd = @"select Distinct SUM(totalStudent) as totalStudent,Sum(totalAmount) as totalAmount, 
                            SUM(paidFee) as paidFee,SUM(RemainingFee) as RemainingFee,SUM(lateFine) as lateFine,[month]
                            from fun_fee12MonthGraph(" + year + @"," + month + @")
                            GROUP BY [month]";
                }
                else
                {
                    cmd = "select Distinct * from fun_fee12MonthGraph(" + year + "," + month + ") where branch_department_section_id = " + branchID + " ";
                }

                var appMenu = dapperQuery.Qry<FeeOf12Month>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getFeeDefaulter")]
        public IActionResult getFeeDefaulter(int branchID,int month,int year)
        {
            try
            {
                if (branchID == 0)
                {
                    cmd = @"select DISTINCT * from fun_feeDefaulter(" + year + "," + month + ")";
                }
                else
                {
                    cmd = "select DISTINCT * from fun_feeDefaulter(" + year + "," + month + ") where branch_department_section_id = " + branchID + " ";
                }

                var appMenu = dapperQuery.Qry<FeeDefaulter>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }
        
    }
}