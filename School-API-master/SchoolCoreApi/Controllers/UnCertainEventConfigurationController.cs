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
    public class UnCertainEventConfigurationController : ControllerBase
    {
        private readonly IOptions<conStr> _dbCon;
        private string cmd;

        public UnCertainEventConfigurationController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        }

        [HttpGet("getWeekDaysName")]
        public IActionResult getWeekDaysName()
        {
            try
            {
                cmd = "SELECT * FROM view_weekDaysName";
                var appMenu = dapperQuery.Qry<WeekDaysName>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getDayByDate")]
        public IActionResult getDayByDate(string fromDate,string toDate)
        {
            try
            {
                cmd = "select DISTINCT calendarDayOfWeek as calendarDay,calendarDayName as calendarDayName from dimcalendar where calendarDate between cast('" + fromDate + "' as date) and cast('" + toDate + "' as date) ";
                var appMenu = dapperQuery.Qry<WeekDaysName>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getSessionTitleDates")]
        public IActionResult getSessionTitleDates(int schoolSessionID)
        {
            try
            {
                cmd = "SELECT * FROM tbl_school_session where isDeleted = 0 and schoolSessionID = "+schoolSessionID+"";
                var appMenu = dapperQuery.Qry<SessionTitleDates>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }
        [HttpGet("getUncertainEvent")]
        public IActionResult getUncertainEvent()
        {
            try
            {
                cmd = "SELECT * FROM view_uncertainEvent";
                var appMenu = dapperQuery.Qry<UncertainEvent>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }
        [HttpGet("getUncertainEventCalendar")]
        public IActionResult getUncertainEventCalendar(int schoolSessionID, string calendarDate)
        {
            try
            {
                cmd = "SELECT * FROM fun_uncertainEventCalendar("+schoolSessionID+",'"+calendarDate+"')";
                var appMenu = dapperQuery.Qry<UncertainEventCalendar>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("saveUnCertainSessionEvent")]
        public IActionResult saveUnCertainSessionEvent(UncertainSessionEventCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_UncertainEventSession",model,_dbCon);
                return Ok(response);
            }
            catch(Exception e)
            {
            return Ok(e);
            }
        }
    }
}