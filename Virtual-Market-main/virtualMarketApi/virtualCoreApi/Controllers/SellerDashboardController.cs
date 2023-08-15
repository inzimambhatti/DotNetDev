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
    public class SellerDashboardController : ControllerBase
    {
        
        private string cmd;
        private readonly IOptions<conStr> _dbCon;

        public SellerDashboardController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        }

        [HttpGet("getSellerDashboard")]
        public IActionResult getSellerDashboard(int userID)
        {
            try
            {
                cmd = "select * from \"view_sellerDashboard\" where \"userID\" = " + userID + " ";
                var appMenu = dapperQuery.Qry<DashboardView>(cmd, _dbCon);

                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

        [HttpGet("getShopGraph")]
        public IActionResult getShopGraph(string fromDate,int shopID)
        {
            try
            {
                cmd = "select * from public.\"fun_dashboardgraph\"('" + fromDate + "') where \"shopID\" = " + shopID + " ";
                var appMenu = dapperQuery.Qry<shopGraph>(cmd, _dbCon);

                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

        [HttpGet("getSellerProfit")]
        public IActionResult getSellerProfit(int userID)
        {
            try
            {
                cmd = "select * from public.\"view_sellerProfit\" where \"userID\" = " + userID + " ";
                var appMenu = dapperQuery.Qry<SellerProfit>(cmd, _dbCon);

                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

        [HttpGet("getActiveOrder")]
        public IActionResult getActiveOrder(int userID)
        {
            try
            {
                cmd = "select * from public.\"view_activeOrders\" where \"userID\" = " + userID + " ";
                var appMenu = dapperQuery.Qry<ActiveOrders>(cmd, _dbCon);

                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

        
        [HttpGet("getCancelOrder")]
        public IActionResult getCancelOrder(int userID)
        {
            try
            {
                cmd = "select * from public.\"view_cancelOrders\" where \"userID\" = " + userID + " ";
                var appMenu = dapperQuery.Qry<CancelOrder>(cmd, _dbCon);

                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

        [HttpGet("getProductSaleCount")]
        public IActionResult getProductSaleCount(int userID)
        {
            try
            {
                cmd = "select * from public.\"view_productCount\" where \"userID\" = " + userID + " ";
                var appMenu = dapperQuery.Qry<ProductSaleCount>(cmd, _dbCon);

                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

        // [HttpGet("getSellerProfit")]
        // public IActionResult getSellerProfit(int userID)
        // {
        //     try
        //     {
        //         cmd = "select * from public.\"view_sellerProfit\" where \"userID\" = " + userID + " ";
        //         var appMenu = dapperQuery.Qry<SellerProfit>(cmd, _dbCon);

        //         return Ok(appMenu);
        //     }
        //     catch (Exception e)
        //     {
        //         return Ok(e);
        //     }

        // }

        [HttpGet("getTotalCustomers")]
        public IActionResult getTotalCustomers(int userID)
        {
            try
            {
                cmd = "select * from public.\"view_totalCustomer\" where \"userID\" = " + userID + " ";
                var appMenu = dapperQuery.Qry<TotalCustomer>(cmd, _dbCon);

                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }


        [HttpGet("getCompletedOrder")]
        public IActionResult getCompletedOrder(int userID)
        {
            try
            {
                cmd = "select * from public.\"view_completedOrder\" where \"userID\" = " + userID + " ";
                var appMenu = dapperQuery.Qry<CompletedOrders>(cmd, _dbCon);

                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }
        
    }
}