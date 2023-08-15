using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UMISModuleAPI.Models;
using UMISModuleApi.dto.request;
using UMISModuleAPI.Services;
using UMISModuleAPI.Entities;
using UMISModuleApi.Entities;
using Microsoft.Extensions.Options;
using UMISModuleAPI.Configuration;
using UMISModuleApi.dto.response;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Web;
using System.Linq;
using Npgsql;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Dapper;
using System.Data;
using System.Collections.Generic;

namespace UMISModuleApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class userDeviceController : ControllerBase
    {
        private readonly IOptions<conStr> _dbCon;
        private string cmd,cmd2;

        public userDeviceController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        }

        [HttpPost("saveUserDevice")]
        public IActionResult saveUserDevice(UserDeviceCreation obj)
        {
            try{
                int rowAffected = 0;
                var response = "";
                var found = false;
                var userName = "";
                // var newUserID = 0;

                List<UserDevices> appMenuUser = new List<UserDevices>();
                cmd = "select \"deviceID\" from tbl_user_devices where \"userID\" = " + obj.userID + "";
                appMenuUser = (List<UserDevices>)dapperQuery.QryResult<UserDevices>(cmd, _dbCon);

                if (appMenuUser.Count > 0)
                        userName = appMenuUser[0].deviceID;

                if(userName=="")
                {
                    cmd2 = "insert into public.\"tbl_user_devices\" (\"userID\",\"deviceID\") values (" + obj.userID + ",'" + obj.deviceID + "')";
                }
                else
                {
                    cmd2 = "update public.\"tbl_user_devices\" set \"deviceID\" = '" + obj.deviceID +"' where \"userID\" = " + obj.userID + "";
                }
                if (found == false)
                {
                    using (NpgsqlConnection con = new NpgsqlConnection(_dbCon.Value.dbCon))
                    {
                        rowAffected = con.Execute(cmd2);
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
                        response = "    already exist";
                    }
                    else
                    {
                        response = "Server Issue";
                    }
                return BadRequest(new { message = response });

                }

            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }
    }
}