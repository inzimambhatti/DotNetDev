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
    public class ReportCardController  : ControllerBase
    {
        private readonly IOptions<conStr> _dbCon;
        private string cmd;

        public ReportCardController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        }

        [HttpGet("getStudentsReportCard")]
        public IActionResult getStudentsReportCard(int schoolSessionID,int branch_department_section_id,int departmentTypeID,int classID,int sectionID,int percentage)
        {
            try
            {
                if (schoolSessionID != 0 && branch_department_section_id != 0 && departmentTypeID == 0 && classID == 0 && sectionID == 0 && percentage == 0)
                {
                    cmd = "SELECT DISTINCT * from view_studentsReportCard where schoolSessionID = " + schoolSessionID + " and branch_department_section_id = " + branch_department_section_id + " ";    
                }
                else if (schoolSessionID != 0 && branch_department_section_id == 0 && departmentTypeID == 0 && classID == 0 && sectionID == 0 && percentage == 0)
                {
                    cmd = "SELECT DISTINCT * from view_studentsReportCard where schoolSessionID = " + schoolSessionID + " ";
                }

                var appMenu = dapperQuery.Qry<StudentsReportCard>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        // [HttpGet("getSessionReportCard")]
        // public IActionResult getSessionReportCard(int studentID)
        // {
        //     try
        //     {
        //         cmd = "select * from fun_sessionReportCardView(" + studentID + ")";    
        //         var appMenu = dapperQuery.Qry<SessionReportCard>(cmd, _dbCon);
        //         return Ok(appMenu);
        //     }
        //     catch (Exception e)
        //     {
        //         return Ok(e);
        //     }
        // }

        [HttpGet("getSessionReportCard")]
        public IActionResult getSessionReportCard(int studentID,int schoolSessionID)
        {
            try
            {
                cmd = @"Exec sp_sessionReportCardView
                            @studentID = " + studentID + @",
                            @schoolSessionID = " + schoolSessionID + " ";    
                var appMenu = dapperQuery.Qry<SessionReportCard>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }
        
    }
}