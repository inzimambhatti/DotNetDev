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
    public class ElementRulesController : ControllerBase
    {
        private readonly IOptions<conStr> _dbCon;
        private string cmd;

        public ElementRulesController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        }
    
        [HttpGet("getElementRules")]
        public IActionResult getElementRules(int feesElementRulelID)
        {
            try
            {
                // if(feesElementRulelID == 0){
                    cmd = "SELECT * FROM view_elementRulesDetails";
                // }else{
                //     cmd = "SELECT * FROM view_elementRulesDetails where feesElementRulelID = "+feesElementRulelID+"";
                // }
                
                var appMenu = dapperQuery.Qry<ElementRules>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }
        [HttpGet("getElementRulesTitle")]
        public IActionResult getElementRulesTitle(int feesElementTypeID)
        {
            try
            {
                    cmd = "SELECT * FROM view_elementsRuleTitle where feesElementTypeID = "+feesElementTypeID+"";
                
                var appMenu = dapperQuery.Qry<ElementRulesTitle>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getElementRulesDetails")]
        public IActionResult getElementRulesDetails(int feesElementID)
        {
            try
            {
                cmd = "SELECT * FROM view_elementsRule ";
                var appMenu = dapperQuery.Qry<ElementRulesDetails>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("saveElementRules")]
        public IActionResult saveElementRules(ElementRulesCreation model){
            try
            {
               var response = dapperQuery.SPReturn("sp_elementRules", model , _dbCon);
               return Ok(response); 
            }
            catch (Exception e)
            {
                
                return Ok(e);
            }
          }
    }
}