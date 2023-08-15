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

namespace virtualCoreApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserMarketController : ControllerBase
    {
        
        private string cmd,cmd2,cmd3;
        private readonly IOptions<conStr> _dbCon;

        public UserMarketController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        }
    
        [HttpGet("getUserMarket")]
        public IActionResult getUserMarket(int userID)
        {
            try
            {
                cmd = "select * from public.\"view_userMarket\" where \"loginUserID\" = " + userID + " ";
                var response = dapperQuery.Qry<Shop>(cmd, _dbCon);

                return Ok(response);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("saveUserMarket")]
        public IActionResult saveUserMarket(UserMarketCreation obj)
        {
            try
            {
                DateTime curDate = DateTime.Today;

                DateTime curTime = DateTime.Now;

                var time = curTime.ToString("HH:mm");
                var found = false;
                var name = 0;
                var newUserMarketID = 0;

                List<UserMarketCreation> appMenuMarket = new List<UserMarketCreation>();
                cmd = "SELECT \"userMarketID\" from tbl_user_market WHERE \"userID\"=" + obj.userID + " and \"shopID\"=" + obj.shopID + "";
                appMenuMarket = (List<UserMarketCreation>)dapperQuery.QryResult<UserMarketCreation>(cmd, _dbCon);

                if (appMenuMarket.Count > 0)
                    name = appMenuMarket[0].userMarketID;

                List<UserMarketCreation> appMenuMarketID = new List<UserMarketCreation>();
                cmd2 = "select \"userMarketID\" from tbl_user_market ORDER BY \"userMarketID\" DESC LIMIT 1";
                appMenuMarketID = (List<UserMarketCreation>)dapperQuery.QryResult<UserMarketCreation>(cmd2, _dbCon);

                if(appMenuMarketID.Count == 0)
                    {
                        newUserMarketID = 1;    
                    }else{
                        newUserMarketID = appMenuMarketID[0].userMarketID+1;
                    }

                int rowAffected = 0;
                var response = "";
                
                if(name == 0)
                {
                    cmd3 = "insert into public.tbl_user_market (\"userMarketID\",\"userID\",\"shopID\") Values (" + newUserMarketID + ", " + obj.userID + ", " + obj.shopID + ")";
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
                }
                else
                {
                    if (found == true)
                    {
                        response = "Shop already exist";
                    }
                    else
                    {
                        response = "Server Issue";
                    }
                }

                return Ok(new { message = response });
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("deleteUserMarket")]
        public IActionResult deleteUserMarket(UserMarketCreation obj)
        {
            try
            {
                DateTime curDate = DateTime.Today;

                DateTime curTime = DateTime.Now;

                var time = curTime.ToString("HH:mm");
                var found = false;
                var name = 0;
                var newUserMarketID = 0;

                List<UserMarketCreation> appMenuMarket = new List<UserMarketCreation>();
                cmd = "SELECT \"userMarketID\" from tbl_user_market WHERE \"userID\"=" + obj.userID + " and \"shopID\"=" + obj.shopID + "";
                appMenuMarket = (List<UserMarketCreation>)dapperQuery.QryResult<UserMarketCreation>(cmd, _dbCon);

                if (appMenuMarket.Count > 0)
                    name = appMenuMarket[0].userMarketID;

                List<UserMarketCreation> appMenuMarketID = new List<UserMarketCreation>();
                cmd2 = "select \"userMarketID\" from tbl_user_market ORDER BY \"userMarketID\" DESC LIMIT 1";
                appMenuMarketID = (List<UserMarketCreation>)dapperQuery.QryResult<UserMarketCreation>(cmd2, _dbCon);

                if(appMenuMarketID.Count == 0)
                    {
                        newUserMarketID = 1;    
                    }else{
                        newUserMarketID = appMenuMarketID[0].userMarketID+1;
                    }

                int rowAffected = 0;
                var response = "";
                
                if(name > 0)
                {
                    cmd3 = "delete from public.tbl_user_market where \"shopID\" = " + obj.shopID + " and \"userID\" = " + obj.userID + " ";
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
                }
                else
                {
                    if (found == true)
                    {
                        response = "shop not exist";
                    }
                    else
                    {
                        response = "Server Issue";
                    }
                }

                return Ok(new { message = response });
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }
    }
}