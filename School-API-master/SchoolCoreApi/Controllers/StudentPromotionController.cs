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
    public class StudentPromotionController : ControllerBase
    {
        private readonly IOptions<conStr> _dbCon;
        private string cmd;

        public StudentPromotionController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        }  

        [HttpGet("getStudentPromotionDetails")]
        public IActionResult getStudentPromotionDetails(int classID, int sectionID, int branchID, int departmentTypeID)
        {
            try
            {
                if (sectionID == 0 && classID == 0 && branchID != 0 && departmentTypeID == 0)
                {
                    cmd = "SELECT * FROM fun_studentPromotion() where branch_department_section_id = " + branchID + " ";
                }
                else if(sectionID == 0 && classID == 0 && branchID != 0 && departmentTypeID != 0)
                {
                    cmd = "SELECT * FROM fun_studentPromotion() where branch_department_section_id = " + branchID + " and departmentTypeID = " + departmentTypeID + " ";
                }
                else if(sectionID == 0 && branchID != 0 && classID != 0 && departmentTypeID != 0)
                {
                    cmd = "SELECT * FROM fun_studentPromotion() where branch_department_section_id = " + branchID + " and classID = " + classID + " and departmentTypeID = " + departmentTypeID + "";
                }
                else
                {
                    cmd = "SELECT * FROM fun_studentPromotion() where branch_department_section_id = " + branchID + " and classID = " + classID + " AND sectionID = " + sectionID + " and departmentTypeID = " + departmentTypeID + " ";
                }
                var appMenu = dapperQuery.Qry<StudentPromotionDetails>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("saveStudentPromotion")]
        public IActionResult saveStudentPromotion(StudentPromotionCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_promoteStudent",model,_dbCon);

                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("saveStudentRemarks")]
        public IActionResult saveStudentRemarks(StudentRemarksCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_updateStudentRemarks",model,_dbCon);

                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }
    }
}