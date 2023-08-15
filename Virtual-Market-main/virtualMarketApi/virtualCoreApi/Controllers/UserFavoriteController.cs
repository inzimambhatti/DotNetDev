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
    public class UserFavoriteController : ControllerBase
    {
        
        private string cmd,cmd2,cmd3;
        private readonly IOptions<conStr> _dbCon;

        public UserFavoriteController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        }

        [HttpGet("getUserFavorite")]
        public IActionResult getUserFavorite(int userID)
        {
            try
            {
                cmd = "SELECT * FROM \"view_userFavorite\" WHERE \"userID\" = " + userID + ";";
                var appMenu = dapperQuery.Qry<Shop>(cmd, _dbCon);

                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

        [HttpGet("getRecommended")]
        public IActionResult getRecommended(int userID)
        {
            try
            {
                cmd = "SELECT * FROM \"view_userFavorite\" WHERE \"userID\" = " + userID + ";";
                var appMenu = dapperQuery.Qry<Shop>(cmd, _dbCon);

                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

        [HttpPost("saveUserFavorite")]
        public IActionResult saveUserFavorite(UserFavoriteCreation obj)
        {
            try
            {
                DateTime curDate = DateTime.Today;

                DateTime curTime = DateTime.Now;

                var time = curTime.ToString("HH:mm");
                var found = false;
                var name = 0;
                var newUserFavoriteID = 0;

                List<UserFavorite> appMenuShop = new List<UserFavorite>();
                cmd = "SELECT \"userFavoriteID\" from tbl_user_favorite WHERE \"userID\"=" + obj.userID + " and \"shopID\"=" + obj.shopID + "";
                appMenuShop = (List<UserFavorite>)dapperQuery.QryResult<UserFavorite>(cmd, _dbCon);

                if (appMenuShop.Count > 0)
                    name = appMenuShop[0].userFavoriteID;

                List<UserFavorite> appMenuShopID = new List<UserFavorite>();
                cmd2 = "select \"userFavoriteID\" from tbl_user_favorite ORDER BY \"userFavoriteID\" DESC LIMIT 1";
                appMenuShopID = (List<UserFavorite>)dapperQuery.QryResult<UserFavorite>(cmd2, _dbCon);

                if(appMenuShopID.Count == 0)
                    {
                        newUserFavoriteID = 1;    
                    }else{
                        newUserFavoriteID = appMenuShopID[0].userFavoriteID+1;
                    }

                int rowAffected = 0;
                var response = "";
                
                if(name == 0)
                {
                    cmd3 = "insert into public.tbl_user_favorite (\"userFavoriteID\",\"isFavorite\",\"userID\",\"shopID\") Values (" + newUserFavoriteID + ",B'1', " + obj.userID + ", " + obj.shopID + ")";
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
                        response = "Favorite already exist";
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

        [HttpPost("deleteUserFavorite")]
        public IActionResult deleteUserFavorite(UserFavoriteCreation obj)
        {
            try
            {
                DateTime curDate = DateTime.Today;

                DateTime curTime = DateTime.Now;

                var time = curTime.ToString("HH:mm");
                var found = false;
                var name = 0;
                var newUserFavoriteID = 0;

                List<UserFavorite> appMenuShop = new List<UserFavorite>();
                cmd = "SELECT \"userFavoriteID\" from tbl_user_favorite WHERE \"userID\"=" + obj.userID + " and \"shopID\"=" + obj.shopID + "";
                appMenuShop = (List<UserFavorite>)dapperQuery.QryResult<UserFavorite>(cmd, _dbCon);

                if (appMenuShop.Count > 0)
                    name = appMenuShop[0].userFavoriteID;

                List<UserFavorite> appMenuShopID = new List<UserFavorite>();
                cmd2 = "select \"userFavoriteID\" from tbl_user_favorite ORDER BY \"shopID\" DESC LIMIT 1";
                appMenuShopID = (List<UserFavorite>)dapperQuery.QryResult<UserFavorite>(cmd2, _dbCon);

                if(appMenuShopID.Count == 0)
                    {
                        newUserFavoriteID = 1;    
                    }else{
                        newUserFavoriteID = appMenuShopID[0].userFavoriteID+1;
                    }

                int rowAffected = 0;
                var response = "";
                
                if(name >= 0)
                {
                    cmd3 = "delete from public.tbl_user_favorite where \"shopID\" = " + obj.shopID + " and \"userID\" = " + obj.userID + " ";
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
                        response = "Favorite not exist";
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