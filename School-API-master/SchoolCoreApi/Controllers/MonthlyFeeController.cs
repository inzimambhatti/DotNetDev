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
    public class MonthlyFeeController : ControllerBase
    {
        private readonly IOptions<conStr> _dbCon;
        private string cmd;

        public MonthlyFeeController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        }

        [HttpGet("getMonthlyFeePlan")]
        public IActionResult getMonthlyFeePlan(int branch_id,int feeProgramTypeID)
        {
            try
            {
                cmd = "SELECT * FROM view_monthlyNormalFeePlan where branch_id = " + branch_id + " and feeProgramTypeID = " + feeProgramTypeID + "";
                var appMenu = dapperQuery.Qry<MonthlyMasterFeePlan>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getTeacherFeeDiscount")]
        public IActionResult getTeacherFeeDiscount(int branch_id,int feeProgramTypeID)
        {
            try
            {
                cmd = "SELECT * FROM view_monthlyFeeTeacherDiscount where branch_id = " + branch_id + " and feeProgramTypeID = " + feeProgramTypeID + "";
                var appMenu = dapperQuery.Qry<MonthlyMasterFeePlan>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getSiblingFeeDiscount")]
        public IActionResult getSiblingFeeDiscount(int branch_id,int feeProgramTypeID)
        {
            try
            {
                cmd = "SELECT * FROM view_monthlyFeeSiblingDiscount where branch_id = " + branch_id + " and feeProgramTypeID = " + feeProgramTypeID + "";
                var appMenu = dapperQuery.Qry<SiblingDiscountPlan>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getOtherParticulars")]
        public IActionResult getOtherParticulars(int branch_id,int feeProgramTypeID)
        {
            try
            {
                cmd = "SELECT * FROM view_otherParticulars where branch_id = " + branch_id + " and feeProgramTypeID = " + feeProgramTypeID + "";
                var appMenu = dapperQuery.Qry<SiblingDiscountPlan>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }
        [HttpGet("getTotalFeeGenerated")]
        public IActionResult getTotalFeeGenerated(int branch_department_section_id)
        {
            try
            {
                cmd = "SELECT COUNT(smf.studentID) AS totalFeeGenerated FROM dbo.tbl_student_monthly_fees_h AS smf INNER JOIN dbo.tbl_student_monthly_fees_detail_h AS smfd ON smf.studentMonthlyFeesID = smfd.studentMonthlyFeesID INNER JOIN dbo.tbl_student ON smf.studentID = dbo.tbl_student.studentID INNER JOIN dbo.tbl_student_registration ON dbo.tbl_student.studentRegistrationID = dbo.tbl_student_registration.studentRegistrationID INNER JOIN dbo.tbl_class_section ON dbo.tbl_student_registration.classSectionID = dbo.tbl_class_section.classSectionID INNER JOIN dbo.tbl_class_department ON dbo.tbl_class_section.classDepartmentID = dbo.tbl_class_department.classDepartmentID WHERE        (MONTH(smf.studentMonthlyFeesDate) = MONTH(GETDATE())) AND (smf.closingFlag = 1)  AND (dbo.tbl_class_department.branch_department_section_id = " + branch_department_section_id + ") ";
                var appMenu = dapperQuery.Qry<FeeGeneration>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getFeeGenerationRemaining")]
        public IActionResult getFeeGenerationRemaining(int branch_department_section_id)
        {
            try
            {
                cmd = "SELECT COUNT(s.studentID) AS remaining FROM dbo.tbl_student_monthly_fees_h AS smf INNER JOIN dbo.tbl_student AS s ON smf.studentID = s.studentID INNER JOIN dbo.tbl_student_registration ON s.studentRegistrationID = dbo.tbl_student_registration.studentRegistrationID INNER JOIN dbo.tbl_class_section ON dbo.tbl_student_registration.classSectionID = dbo.tbl_class_section.classSectionID INNER JOIN dbo.tbl_class_department ON dbo.tbl_class_section.classDepartmentID = dbo.tbl_class_department.classDepartmentID WHERE (MONTH(smf.studentMonthlyFeesDate) <> MONTH(GETDATE())) AND (dbo.tbl_class_department.branch_department_section_id = " + branch_department_section_id + ")";
                var appMenu = dapperQuery.Qry<FeeGenerationRemaining>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getFeeGenerationTotal")]
        public IActionResult getFeeGenerationTotal(int branch_department_section_id)
        {
            try
            {
                cmd = "SELECT COUNT(dbo.tbl_student.studentID) AS total FROM dbo.tbl_class_department INNER JOIN dbo.tbl_class_section ON dbo.tbl_class_department.classDepartmentID = dbo.tbl_class_section.classDepartmentID INNER JOIN dbo.tbl_student_registration ON dbo.tbl_class_section.classSectionID = dbo.tbl_student_registration.classSectionID INNER JOIN  dbo.tbl_student ON dbo.tbl_student_registration.studentRegistrationID = dbo.tbl_student.studentRegistrationID WHERE (dbo.tbl_student.isDeleted = 0) AND (dbo.tbl_class_department.branch_department_section_id = " + branch_department_section_id + ")";
                var appMenu = dapperQuery.Qry<FeeGenerationTotal>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }
        
        [HttpGet("getstudentFeeChallan")]
        public IActionResult getstudentFeeChallan(int studentID,int month,int year)
        {
            try
            {
                if(studentID != 0 && month == 0 && year == 0)
                {
                    cmd = "SELECT * FROM fun_studentFeeChallan() where studentID = " + studentID+" ";
                }
                else
                {
                    cmd = "SELECT * FROM fun_studentFeeChallan() where studentID = " + studentID+" and feeChallanMonth = " + month + " and feeChallanYear = " + year + "";
                }
                var appMenu = dapperQuery.Qry<StudentFeeChallan>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getstudentHistry")]
        public IActionResult getstudentHistry(int studentID)
        {
            try
            {
                cmd = "SELECT * FROM view_studentHistory where studentID = "+ studentID +"";
                var appMenu = dapperQuery.Qry<StudentHistory>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getRemainingStudent")]
        public IActionResult getRemainingStudent()
        {
            try
            {
                cmd = "SELECT * FROM view_remaining";
                var appMenu = dapperQuery.Qry<RemainigStudents>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        } 

        [HttpGet("getBranchClassSectionOfTransaction")]
        public IActionResult getBranchClassSectionOfTransaction(int feesTransactionID)
        {
            try
            {
                cmd = "select distinct clsDept.branch_department_section_id,bl.branch_name,cls.classID,cls.className,sec.sectionID,sec.sectionName "+
                        " from tbl_student as std inner join  tbl_student_fees_plan_h as smf on std.studentID = smf.studentID  inner join "+
                        " tbl_student_fees_plan_details_h as sfpd on sfpd.studentFeesPlanID = smf.studenFeesPlanID inner join tbl_student_registration  as stdReg on std.studentRegistrationID = stdReg.studentRegistrationID inner join "+
                        " tbl_class_section as clsSec on stdReg.classSectionID = clsSec.classSectionID inner join tbl_calss as cls on clsSec.classID = cls.classID inner join "+
                        " tbl_section sec on clsSec.sectionID = sec.sectionID inner join tbl_class_department clsDept on clsSec.classDepartmentID = clsDept.classDepartmentID inner join "+
                        " aimsCMIS.dbo.tbl_branch_department_sections AS bds ON bds.branch_department_section_id = clsDept.branch_department_section_id LEFT OUTER JOIN "+
                        " aimsCMIS.dbo.tbl_company_branch AS cb ON cb.company_branch_id = bds.company_branch_id LEFT OUTER JOIN aimsCMIS.dbo.tbl_branches_loc AS bl ON bl.branch_id = cb.branch_id "+
                        " where std.isDeleted = 0 and stdReg.isDeleted = 0 and clsSec.isDeleted = 0 and cls.isDeleted = 0 and sec.isDeleted = 0 and clsDept.isDeleted = 0  and bds.isDeleted = 0 and cb.isDeleted = 0 and bl.isDeleted = 0 and sfpd.isDeleted = 0 and sfpd.feesTransactionID = " + feesTransactionID  + "";
                var appMenu = dapperQuery.Qry<FeesTransactionBranches>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        } 

        [HttpGet("getClassBranch")]
        public IActionResult getClassBranch(int branch_department_section_id)
        {
            try
            {
                cmd = @"SELECT DISTINCT cd.branch_department_section_id, c.className, cs.classID
                        FROM   dbo.tbl_class_department AS cd INNER JOIN
                            dbo.tbl_class_section AS cs ON cd.classDepartmentID = cs.classDepartmentID INNER JOIN
                            dbo.tbl_calss AS c ON cs.classID = c.classID INNER JOIN
                            dbo.tbl_department_type AS dt ON cd.departmentTypeID = dt.departmentTypeID 
                        where cd.isDeleted = 0 and cs.isDeleted = 0 and dt.isDeleted = 0 and c.isDeleted = 0 and cd.branch_department_section_id = "+branch_department_section_id+"";
                var appMenu = dapperQuery.Qry<ClassBranchGen>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }
        [HttpGet("getClassBranchSection")]
        public IActionResult getClassBranchSection(int branch_department_section_id, int classID)
        {
            try
            {
                cmd = @"SELECT DISTINCT cd.branch_department_section_id,s.sectionName, cs.classID, s.sectionID 
                        FROM   dbo.tbl_class_department AS cd INNER JOIN
                                    dbo.tbl_class_section AS cs ON cd.classDepartmentID = cs.classDepartmentID INNER JOIN
                                    dbo.tbl_calss AS c ON cs.classID = c.classID INNER JOIN
                                    dbo.tbl_section AS s ON cs.sectionID = s.sectionID INNER JOIN
                                    dbo.tbl_department_type AS dt ON cd.departmentTypeID = dt.departmentTypeID
                        WHERE (cs.isDeleted = 0) and cd.branch_department_section_id = "+branch_department_section_id+" and c.classID = "+classID+"";
                var appMenu = dapperQuery.Qry<ClassBranchSectionGen>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getFeeGenerationDetails")]
        public IActionResult getFeeGenerationDetails(int month, int year, int classID, int sectionID, int branchID)
        {
            try
            {
                if (sectionID == 0 && branchID != 0 && classID != 0)
                {
                    cmd = "SELECT * FROM fun_feeGeneration("+month+","+year+") where branch_department_section_id = " + branchID + " and classID = " + classID + " order by studentName asc";
                }
                else if(sectionID == 0 && classID == 0 && branchID != 0)
                {
                    cmd = "SELECT * FROM fun_feeGeneration("+month+","+year+") where branch_department_section_id = " + branchID + " order by studentName asc";
                }
                else
                {
                    cmd = "SELECT * FROM fun_feeGeneration("+month+","+year+") where branch_department_section_id = " + branchID + " and classID = " + classID + " AND sectionID = " + sectionID + " order by studentName asc";
                }
                var appMenu = dapperQuery.Qry<FeeGenerationDetails>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getFeeGenerationBulk")]
        public IActionResult getFeeGenerationBulk(int month, int year, int classID, int sectionID, int branchID)
        {
            try
            {
                if (sectionID == 0 && branchID != 0 && classID != 0)
                {
                    cmd = "SELECT * FROM fun_studentChallanPdf("+month+","+year+") where branch_department_section_id = " + branchID + " and classID = " + classID + "";
                }
                else if(sectionID == 0 && classID == 0 && branchID != 0)
                {
                    cmd = "SELECT * FROM fun_studentChallanPdf("+month+","+year+") where branch_department_section_id = " + branchID + "";
                }
                else
                {
                    cmd = "SELECT * FROM fun_studentChallanPdf("+month+","+year+") where branch_department_section_id = " + branchID + " and classID = " + classID + " AND sectionID = " + sectionID + "";
                }
                var appMenu = dapperQuery.Qry<StudentFeeGeneration>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }       

        [HttpPost("saveMonthlyFeeGeneration")]
        public IActionResult saveMonthlyFeeGeneration(FeeRegistrationCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_monthlyFeeGeneration",model,_dbCon);
                
                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("saveFeeGenerationCrud")]
        public IActionResult saveFeeGenerationCrud(FeeGenerationCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_feeGenerationCrud",model,_dbCon);
                
                return Ok(response);
            }
            catch(Exception e)
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
    }
}