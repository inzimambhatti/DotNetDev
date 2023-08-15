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
    public class StudentRegistrationController : ControllerBase
    {
        private readonly IOptions<conStr> _dbCon;
        private string cmd;

        public StudentRegistrationController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        }

        [HttpGet("getUserBranches")]
        public IActionResult getUserBranches(int userID)
        {
            try
            {
                cmd = "select * from view_userBranchStudent where userID = " + userID + "";
                var appMenu = dapperQuery.Qry<StudentRegistration>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getStudentRegistration")]
        public IActionResult getStudentRegistration(int studentID,int userID)
        {
            try
            {
                if(studentID == 0){
                    cmd = "select * from view_userBranchStudent where userID = " + userID + "";
                }else{
                    cmd = "SELECT * FROM view_studentRegistration where studentRegistrationCode = " + studentID + "";
                }
                
                var appMenu = dapperQuery.Qry<StudentRegistration>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getStudentDetail")]
        public IActionResult getStudentDetail(int studentID)
        {
            try
            {
                cmd = "SELECT * FROM view_studentRegistration where studentID = " + studentID + "";
                
                var appMenu = dapperQuery.Qry<StudentRegistration>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getStudentList")]
        public IActionResult getStudentList(int branchID,int classID,int sectionID, int month, int year)
        {
            try
            {
                if (sectionID == 0 && branchID != 0 && classID != 0)
                {
                    cmd = "SELECT * FROM view_studentRegistration where branch_department_section_id = " + branchID + " and classID = " + classID + "  and month(studentMonthlyFeesDate) ="+month+" and year(studentMonthlyFeesDate) ="+year+"";
                }
                else if(sectionID == 0 && classID == 0 && branchID != 0)
                {
                    cmd = "SELECT * FROM view_studentRegistration where branch_department_section_id = " + branchID + "  and month(studentMonthlyFeesDate) ="+month+" and year(studentMonthlyFeesDate) ="+year+"";
                }
                else
                {
                    cmd = "SELECT * FROM view_studentRegistration where branch_department_section_id = " + branchID + " and classID = " + classID + " AND sectionID = " + sectionID + "   and month(studentMonthlyFeesDate) ="+month+" and year(studentMonthlyFeesDate) ="+year+"";
                }
                
                
                var appMenu = dapperQuery.Qry<StudentRegistration>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getStudentListTransaction")]
        public IActionResult getStudentListTransaction(int branchID,int classID,int sectionID)
        {
            try
            {
                if (sectionID == 0 && branchID != 0 && classID != 0)
                {
                    cmd = "SELECT * FROM view_studentRegistration where branch_department_section_id = " + branchID + " and classID = " + classID + " ";
                }
                else if(sectionID == 0 && classID == 0 && branchID != 0)
                {
                    cmd = "SELECT * FROM view_studentRegistration where branch_department_section_id = " + branchID + "";
                }
                else
                {
                    cmd = "SELECT * FROM view_studentRegistration where branch_department_section_id = " + branchID + " and classID = " + classID + " AND sectionID = " + sectionID + "";
                }
                
                
                var appMenu = dapperQuery.Qry<StudentRegistration>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getStudentRegFee")]
        public IActionResult getStudentRegFee(int studentID)
        {
            try
            {
                cmd = "SELECT * FROM view_studentRegFeeDetail where studentID = " + studentID + "";
                
                var appMenu = dapperQuery.Qry<StudentRegistrationFee>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getStudentInformation")]
        public IActionResult getStudentInformation(int studentID)
        {
            try
            {
                cmd = "SELECT * FROM view_studentParentInformation where studentID = " + studentID + "";
                
                var appMenu = dapperQuery.Qry<StudentParentInfo>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        // [HttpGet("getStudentInformation")]
        // public IActionResult getStudentInformation(int studentRegistrationCode)
        // {
        //     try
        //     {
        //         cmd = "SELECT studentID,studentName,studentPlaceOfBirth,studentGender,studentReligon,studentEdoc FROM tbl_student where studentRegistrationCode = " + studentRegistrationCode + "";
                
                
        //         var appMenu = dapperQuery.Qry<StudentInformation>(cmd, _dbCon);
        //         return Ok(appMenu);
        //     }
        //     catch (Exception e)
        //     {
        //         return Ok(e);
        //     }
        // }

        // [HttpGet("getStudent")]
        // public IActionResult getStudent(int studentID)
        // {
        //     try
        //     {
        //         cmd = "SELECT * FROM tbl_student where studentID = "+studentID+"";
        //         var appMenu = dapperQuery.Qry<StudentRegistration>(cmd, _dbCon);
        //         return Ok(appMenu);
        //     }
        //     catch (Exception e)
        //     {
        //         return Ok(e);
        //     }
        // }

        [HttpGet("getMotherTongue")]
        public IActionResult getMotherTongue()
        {
            try
            {
                cmd = "SELECT * FROM tbl_mother_tongue";
                var appMenu = dapperQuery.Qry<MotherTongue>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getParentType")]
        public IActionResult getParentType()
        {
            try
            {
                cmd = "SELECT * FROM tbl_parent_type";
                var appMenu = dapperQuery.Qry<ParentType>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getNationality")]
        public IActionResult getNationality()
        {
            try
            {
                cmd = "SELECT * FROM tbl_nationality";
                var appMenu = dapperQuery.Qry<Nationality>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getStudentClassBranch")]
        public IActionResult getStudentClassBranch(int branch_department_section_id,int departmentTypeID)
        {
            try
            {
                cmd = "select classID,className from view_studentClassBranch where branch_department_section_id="+branch_department_section_id+" and departmentTypeID ="+departmentTypeID+"";
                var appMenu = dapperQuery.Qry<StudentClassBranch>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getBranch")]
        public IActionResult getBranch()
        {
            try
            {
                cmd = "select * from view_branch";
                var appMenu = dapperQuery.Qry<Branch>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getClassBranch")]
        public IActionResult getClassBranch()
        {
            try
            {
                cmd = "select * from view_classBranch";
                var appMenu = dapperQuery.Qry<ClassBranch>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getClassBranchSection")]
        public IActionResult getClassBranchSection()
        {
            try
            {
                cmd = "select * from view_sectionClassBranch";
                var appMenu = dapperQuery.Qry<SectionClassBranch>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getStudentClassBranchSection")]
        public IActionResult getStudentClassBranchSection(int branch_department_section_id,int departmentTypeID,int classID)
        {
            try
            {
                cmd = "select sectionID,sectionName from view_studentClassBranchSection where branch_department_section_id="+branch_department_section_id+" and departmentTypeID = "+departmentTypeID+" and classID = "+classID+"";
                var appMenu = dapperQuery.Qry<StudentClassBranchSection>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getSchoolCalander")]
        public IActionResult getSchoolCalander()
        {
            try
            {
                cmd = "SELECT * FROM tbl_school_calander where isDeleted=0 order by sessionYear DESC";
                var appMenu = dapperQuery.Qry<SchoolCalander>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("saveStudentRegistration")]
        public IActionResult saveStudentRegistration(StudentRegistrationCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_studentRegistration",model,_dbCon);
                var data = response.Select(row => new { res = row.ToString() });
                bool result = data.First().res.Contains("Success");

                if (result == true && (model.student_picture_path != null && model.student_picture_path != "" && model.student_picture_path != "null"))
                {

                    var studentID = data.First().res.Split("|||")[1];

                    dapperQuery.saveImageFile(
                        model.student_picture_path,
                        studentID,
                        model.student_picture,
                        model.student_picture_extension);
                }

                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }
        
    }
}