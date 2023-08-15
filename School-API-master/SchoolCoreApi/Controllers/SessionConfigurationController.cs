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
    public class SessionConfigurationController : ControllerBase
    {
        private readonly IOptions<conStr> _dbCon;
        private string cmd;

        public SessionConfigurationController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        }

        [HttpGet("getSessionConfiguration")]
        public IActionResult getSessionConfiguration()
        {
            try
            {
                cmd = "SELECT * FROM view_sessionConfiguration";
                var appMenu = dapperQuery.Qry<SessionConfiguration>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getSessionDates")]
        public IActionResult getSessionDates(int schoolSessionID)
        {
            try
            {
                cmd = @"select distinct schoolSessionID,schoolSessionTitle,cast(schoolSessionStartDate as date) as schoolSessionStartDate,
                        cast(schoolSessionEndDate as date) schoolSessionEndDate
                        from tbl_school_session where isDeleted = 0 and schoolSessionID = " + schoolSessionID + "";
                var appMenu = dapperQuery.Qry<SessionDate>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getCurrentSession")]
        public IActionResult getCurrentSession()
        {
            try
            {
                cmd = @"select TOP 1 schoolSessionID,schoolSessionTitle from tbl_school_session 
                        where Year(schoolSessionStartDate) = YEAR(getdate()) 
                        and Year(schoolSessionEndDate) = Year(GETDATE())+1 
                        and isDeleted = 0";
                var appMenu = dapperQuery.Qry<CurrentSession>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getSessionTitle")]
        public IActionResult getSessionTitle()
        {
            try
            {
                cmd = @"SELECT DISTINCT ss.* FROM tbl_school_session ss
                        inner join dimcalendar d on d.schoolSessionID = ss.schoolSessionID
                        where ss.isDeleted = 0";
                var appMenu = dapperQuery.Qry<SessionTitle>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getEventTitle")]
        public IActionResult getEventTitle()
        {
            try
            {
                cmd = "SELECT * FROM tbl_events where isDeleted = 0";
                var appMenu = dapperQuery.Qry<EventTitle>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getSessionConfig")]
        public IActionResult getSessionConfig()
        {
            try
            {
                cmd = "SELECT * FROM view_sessionConfig";
                var appMenu = dapperQuery.Qry<SessionConfig>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getEventType")]
        public IActionResult getEventType()
        {
            try
            {
                cmd = "SELECT * FROM tbl_event_type where isDeleted=0";
                var appMenu = dapperQuery.Qry<EventType>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getCalendarData")]
        public IActionResult getCalendarData(int schoolSessionID)
        {
            try
            {
                cmd = "SELECT * FROM fun_calendarData("+@schoolSessionID+")";
                var appMenu = dapperQuery.Qry<CalendarData>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getEventParent")]
        public IActionResult getEventParent()
        {
            try
            {
                cmd = "SELECT * FROM view_EventCTE";
                var appMenu = dapperQuery.Qry<EventParent>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getEvent")]
        public IActionResult getEvent()
        {
            try
            {
                cmd = "SELECT * FROM view_eventTable";
                var appMenu = dapperQuery.Qry<Event>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getMarkableEvents")]
        public IActionResult getMarkableEvents()
        {
            try
            {
                cmd = "SELECT * FROM view_markableEvents";
                var appMenu = dapperQuery.Qry<MarkableEvent>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }
        // Coounting of All Events, Upcoming, Complete, Assigned and Important Events...
        [HttpGet("getCountOfAllEvents")]
        public IActionResult getCountOfAllEvents()
        {
            try
            {
                cmd = @"select count(sessionEventID) as sessionEventID 
                        from tbl_markable_event_detail where isDeleted=0";
                var appMenu = dapperQuery.Qry<AllEvent>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getImportantEvents")]
        public IActionResult getImportantEvents()
        {
            try
            {
                cmd = @"select count(med.sessionEventID) as importantEvent
                        from tbl_events as e inner join 
                        tbl_session_events as se on se.eventID = e.eventID inner join 
                        tbl_markable_event_detail as med on med.sessionEventID = se.sessionEventID
                        where e.isDeleted=0 and med.isDeleted=0 and e.importantEvent=1
                        ";
                var appMenu = dapperQuery.Qry<ImportantEvents>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getCompletedEvents")]
        public IActionResult getCompletedEvents()
        {
            try
            {
                cmd = @"select count(markableEventDetailID)as completeEvents from tbl_markable_event_detail
                        where markableEventDetailToDate<GETDATE() and isDeleted=0
                        ";
                var appMenu = dapperQuery.Qry<CompletedEvents>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getUpcomingEvents")]
        public IActionResult getUpcomingEvents()
        {
            try
            {
                cmd = @"select count(markableEventDetailID) as upcomingEvents
                        from tbl_markable_event_detail
                        where GETDATE() <= markableEventDetailToDate and isDeleted=0
                        ";
                var appMenu = dapperQuery.Qry<UpcomingEvents>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getSessionEvent")]
        public IActionResult getSessionEvent(int schoolSessionID)
        {
            try
            {
                cmd = "SELECT * FROM fun_sessionEvent("+schoolSessionID+")";
                var appMenu = dapperQuery.Qry<SessionEvent>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }
    
        [HttpGet("getEventRole")]
        public IActionResult getEventRole(int eventID)
        {
            try
            {
                cmd = "select * from fun_getEventRole("+@eventID+")";
                var appMenu = dapperQuery.Qry<EventRole>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        // [HttpGet("getRoleList")]
        // public IActionResult getRoleList()
        // {
        //     try
        //     {
        //         cmd = "select * from view_roleList";
        //         var appMenu = dapperQuery.Qry<EventRoles>(cmd, _dbCon);
        //         return Ok(appMenu);
        //     }
        //     catch (Exception e)
        //     {
        //         return Ok(e);
        //     }
        // }
    
        [HttpPost("saveSessionConfiguration")]
        public IActionResult saveSessionConfiguration(SessionConfigurationCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_sessionConfiguration",model,_dbCon);

                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("saveCreateSession")]
        public IActionResult saveCreateSession(CreateSessionCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_createSession",model,_dbCon);

                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("saveEventType")]
        public IActionResult saveEventType(EventTypeCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_eventType",model,_dbCon);
                return Ok(response);
            }
            catch(Exception e)
            {
            return Ok(e);
            }
        }

        [HttpPost("saveEvent")]
        public IActionResult saveEvent(EventCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_createEvents",model,_dbCon);
                return Ok(response);
            }
            catch(Exception e)
            {
            return Ok(e);
            }
        }

        [HttpPost("saveRoleEvent")]
        public IActionResult saveRoleEvent(RoleEventCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_roleEvent",model,_dbCon);
                return Ok(response);
            }
            catch(Exception e)
            {
            return Ok(e);
            }
        }

        [HttpPost("saveMarkableEvent")]
        public IActionResult saveMarkableEvent(MarkableEventCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_createMarkableEvent",model,_dbCon);
                return Ok(response);
            }
            catch(Exception e)
            {
            return Ok(e);
            }
        }

        [HttpPost("saveSessionEventConfiguration")]
        public IActionResult saveSessionEventConfiguration(SessionEventCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_sessionEventConfiguration",model,_dbCon);
                return Ok(response);
            }
            catch(Exception e)
            {
            return Ok(e);
            }
        }
    }
}