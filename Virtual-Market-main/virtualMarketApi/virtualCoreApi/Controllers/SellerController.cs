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
    public class SellerController : ControllerBase
    {
        
        private string cmd;
        private readonly IOptions<conStr> _dbCon;

        public SellerController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        }
    
        [HttpGet("getCustomerOfSeller")]
        public IActionResult getCustomerOfSeller(int sellerID)
        {
            try
            {
                cmd = "select * from public.\"view_customerOfSeller\" where \"sellerID\" = " + sellerID + "";
                var appMenu = dapperQuery.QryResult<CustomerOfSeller>(cmd, _dbCon);

                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getSellerOfCustomer")]
        public IActionResult getSellerOfCustomer(int customerID)
        {
            try
            {
                cmd = "select * from public.\"view_sellerOfCustomer\" where \"customerID\" = " + customerID + "";
                var appMenu = dapperQuery.QryResult<SellerOfCustomer>(cmd, _dbCon);

                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

    }
}