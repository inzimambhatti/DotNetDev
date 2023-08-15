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
    public class ReportsController : ControllerBase
    {
        private readonly IOptions<conStr> _dbCon;
        private string cmd;

        public ReportsController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        }

        [HttpGet("getStudentReport")]
        public IActionResult getStudentReport(int branch_department_section_id,int departmentTypeID,int classID, int sectionID)
        {
            try
            {
                 if(branch_department_section_id != 0 && departmentTypeID == 0 && classID == 0 && sectionID == 0)
                {
                     cmd = "SELECT * FROM view_student_report where branch_department_section_id = "+branch_department_section_id+";";
                }
                else if(branch_department_section_id != 0 && departmentTypeID != 0 && classID == 0 && sectionID == 0)
                {
                   cmd = "SELECT * FROM view_student_report where  branch_department_section_id = "+branch_department_section_id+" and departmentTypeID = "+departmentTypeID+"";
                }
                else if(branch_department_section_id != 0 && departmentTypeID != 0 && classID != 0  && sectionID == 0)
                {
                   cmd = "SELECT * FROM view_student_report where branch_department_section_id = "+branch_department_section_id+" and departmentTypeID = "+departmentTypeID+" and classID = "+classID+"";
                }
                else if(branch_department_section_id != 0 && departmentTypeID != 0 && classID != 0 && sectionID != 0)
                {
                   cmd = "SELECT * FROM view_student_report where branch_department_section_id = "+branch_department_section_id+" and departmentTypeID = "+departmentTypeID+" and classID = "+classID+" and sectionID  = "+sectionID+"";
                }
                else
                {
                cmd = "SELECT * FROM view_student_report";

                }
                var appMenu = dapperQuery.Qry<StudentReport>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }       
    }
}