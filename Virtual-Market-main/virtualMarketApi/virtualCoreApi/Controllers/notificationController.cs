using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using virtualCoreApi.Entities;
using virtualCoreApi.Services;
using Dapper;
using System.Data;
using Npgsql;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace virtualCoreApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class notificationController : ControllerBase
    {
        private string cmd,cmd2,cmd3;
        private readonly IOptions<conStr> _dbCon;

        public notificationController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        }

        [HttpGet("getChats")]
        public IActionResult getChats(int userID,int sellerID)
        {
            try
            {
                if (userID == 0)
                {
                    cmd = "SELECT * FROM public.view_chats WHERE \"sellerID\" = " + sellerID + ";";    
                }
                else
                {
                    cmd = "SELECT * FROM public.view_chats WHERE \"userID\" = " + userID + ";";
                } 
                
                
                var appMenu = dapperQuery.Qry<Chats>(cmd, _dbCon);

                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

        [HttpGet("getNotifications")]
        public IActionResult getNotifications(int userID,int sellerID)
        {
            try
            {
                if (userID == 0)
                {
                    cmd = "SELECT * FROM tbl_notification_log WHERE \"sellerID\" = " + sellerID + ";"; 
                }
                else
                {
                    cmd = "SELECT * FROM tbl_notification_log WHERE \"userID\" = " + userID + ";";
                }
                
                var appMenu = dapperQuery.Qry<Notifications>(cmd, _dbCon);

                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

        [HttpPost("saveNotification")]
        public IActionResult saveNotification(NotificationCreation obj)
        {
            try
            {
                DateTime curDate = DateTime.Today;

                DateTime curTime = DateTime.Now;

                var time = curTime.ToString("HH:mm");
                var found = false;
                var name = "";
                var newNotificationID = 0;
                
                List<Notifications> appMenuProduct = new List<Notifications>();
                cmd = "select \"notificationTitle\" from tbl_notification_log WHERE \"notificationID\"= " + obj.notificationID + " ";
                appMenuProduct = (List<Notifications>)dapperQuery.QryResult<Notifications>(cmd, _dbCon);

                if (appMenuProduct.Count > 0)
                    name = appMenuProduct[0].notificationTitle;

                List<Notifications> appMenuProductID = new List<Notifications>();
                cmd2 = "select \"notificationID\" from tbl_notification_log ORDER BY \"notificationID\" DESC LIMIT 1";
                appMenuProductID = (List<Notifications>)dapperQuery.QryResult<Notifications>(cmd2, _dbCon);

                if(appMenuProductID.Count == 0)
                    {
                        newNotificationID = 1;
                    }else{
                        newNotificationID = appMenuProductID[0].notificationID+1;
                    }

                int rowAffected = 0;
                var response = "";
                
                if(name=="")
                {
                    cmd3 = "insert into public.tbl_notification_log (\"notificationID\",\"notificationTitle\",\"notificationSubTitle\",\"userName\",\"sellerName\",\"userID\",\"sellerID\",\"createdOn\") Values (" + newNotificationID + ", '" + obj.notificationTitle + "', '" + obj.notificationSubTitle + "','" + obj.userName + "','" + obj.sellerName + "'," + obj.userID + "," + obj.sellerID + ",'" + obj.createdOn + "')";
                }
                else
                {
                    found = true;
                }
            

                if (found == false)
                {
                    using (NpgsqlConnection con = new NpgsqlConnection(_dbCon.Value.dbCon))
                    {
                        rowAffected = con.Execute(cmd3);
                    }
                }

                if (rowAffected > 0)
                {
                    response = "Success";
                    return Ok(new { message = response });
                    
                }
                else
                {
                    if (found == true)
                    {
                        response = "Notification already exist";
                    }
                    else
                    {
                        response = "Server Issue";
                    }
                    return BadRequest(new { message = response });
                }

                
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }
        
    }
}