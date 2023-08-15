using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UMISModuleAPI.Models;
using UMISModuleAPI.Services;
using UMISModuleAPI.Entities;
using Microsoft.Extensions.Options;
using UMISModuleAPI.Configuration;
using UMISModuleApi.dto.response;
using UMISModuleApi.dto.request;

namespace UMISModuleApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController : ControllerBase
    {
        private IUserService _userService;
        private string cmd;
        private readonly IOptions<conStr> _dbCon;

        public RoleController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        }

        [HttpGet("getApplicationModule")]
        public IActionResult getApplicationModule()
        {
            try
            {
                cmd = "SELECT applicationModuleID,applicationModuleTitle from application_modules where isDeleted=0";
                var appMenu = dapperQuery.Qry<ApplicationModule>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getMenuRoleByModuleId")]
        public IActionResult getMenurole(int applicationModuleId)
        {
            try
            {
                if(applicationModuleId == 0){
                    cmd = "SELECT * from view_role_detail";
                }else{
                    cmd = "SELECT * from view_role_detail where applicationModuleId="+applicationModuleId+"";
                }
                var appMenu = dapperQuery.Qry<Menu>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }
        
        [HttpGet("getMenu")]
        public IActionResult getMenu()
        {
            try
            {
                cmd = "select * from view_AllMenu";
                var appMenu = dapperQuery.Qry<Menu>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }
        
        [HttpGet("getMenuAll")]
        public IActionResult getMenuAll()
        {
            try
            {
                cmd = "SELECT menuId,menuTitle from view_menu";
                var appMenu = dapperQuery.Qry<Menu>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("getMenuListAllRoles")]
        public IActionResult getMenuListAllRoles(menuList model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_LoadMenuListAll_Roles",model,_dbCon);
                return Ok(new { message = response });
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getAllRole")]
        public IActionResult getAllRole()
        {
            try
            {
                cmd = "SELECT * from view_AllRole ORDER BY roleID ASC";
                var appMenu = dapperQuery.Qry<Roles>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch(Exception e)
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

        [HttpPost("createRole")]
        public IActionResult createRole(roleCreation model)
        {
            try
            {
                var response = dapperQuery.SPReturn("sp_roleCreation",model,_dbCon);

                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }
    }
}