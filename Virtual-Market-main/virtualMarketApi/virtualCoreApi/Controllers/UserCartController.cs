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
    public class UserCartController : ControllerBase
    {
        
        private string cmd,cmd2,cmd3;
        private readonly IOptions<conStr> _dbCon;

        public UserCartController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        }

        [HttpGet("getUserCart")]
        public IActionResult getUserCart(int userID)
        {
            try
            {
                cmd = "select * from public.\"view_userCart\" where \"userID\" = " + userID + " ";
                var response = dapperQuery.Qry<UserCart>(cmd, _dbCon);

                return Ok(response);
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

        [HttpPost("saveUserCart")]
        public IActionResult saveUserCart(UserCartCreation obj)
        {
            try
            {
                DateTime curDate = DateTime.Today;

                DateTime curTime = DateTime.Now;

                var time = curTime.ToString("HH:mm");
                var found = false;
                var name = 0;
                var newUserCartID = 0;
                var ShopProductID = 0;

                
                List<UserCart> appMenuShopProductID = new List<UserCart>();
                cmd = "SELECT \"shopProductID\" from tbl_shop_products WHERE \"productID\"=" + obj.productID + " and \"shopID\"=" + obj.shopID + "";
                appMenuShopProductID = (List<UserCart>)dapperQuery.QryResult<UserCart>(cmd, _dbCon);

                if (appMenuShopProductID.Count > 0)
                    ShopProductID = appMenuShopProductID[0].shopProductID;

                List<UserCart> appMenuShop = new List<UserCart>();
                cmd = "SELECT \"userCartID\" from tbl_user_cart WHERE \"userID\"=" + obj.userID + " and \"shopProductID\"=" + ShopProductID + "";
                appMenuShop = (List<UserCart>)dapperQuery.QryResult<UserCart>(cmd, _dbCon);

                if (appMenuShop.Count > 0)
                    name = appMenuShop[0].userCartID;
                
                List<UserCart> appMenuShopID = new List<UserCart>();
                cmd2 = "select \"userCartID\" from tbl_user_cart ORDER BY \"userCartID\" DESC LIMIT 1";
                appMenuShopID = (List<UserCart>)dapperQuery.QryResult<UserCart>(cmd2, _dbCon);

                if(appMenuShopID.Count == 0)
                    {
                        newUserCartID = 1;    
                    }else{
                        newUserCartID = appMenuShopID[0].userCartID+1;
                    }

                int rowAffected = 0;
                var response = "";
                
                if(name == 0)
                {
                    cmd3 = "insert into public.tbl_user_cart (\"userCartID\",\"userID\",\"shopProductID\") Values (" + newUserCartID + "," + obj.userID + ", " + ShopProductID + ")";
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
                        response = "Product already exists in Cart";
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

        [HttpPost("deleteUserCart")]
        public IActionResult deleteUserCart(UserCartCreation obj)
        {
            try
            {
                DateTime curDate = DateTime.Today;

                DateTime curTime = DateTime.Now;

                var time = curTime.ToString("HH:mm");
                var found = false;
                var name = 0;
                //var newUserFavoriteID = 0;
                var ShopProductID = 0;

                
                List<UserCart> appMenuShopProductID = new List<UserCart>();
                cmd = "SELECT \"shopProductID\" from tbl_shop_products WHERE \"productID\"=" + obj.productID + " and \"shopID\"=" + obj.shopID + "";
                appMenuShopProductID = (List<UserCart>)dapperQuery.QryResult<UserCart>(cmd, _dbCon);

                if (appMenuShopProductID.Count > 0)
                    ShopProductID = appMenuShopProductID[0].shopProductID;


                List<UserCart> appMenuShop = new List<UserCart>();
                cmd = "SELECT \"userCartID\" from tbl_user_cart WHERE \"userID\"=" + obj.userID + " and \"shopProductID\"=" + ShopProductID + "";
                appMenuShop = (List<UserCart>)dapperQuery.QryResult<UserCart>(cmd, _dbCon);

                if (appMenuShop.Count > 0)
                    name = appMenuShop[0].userCartID;

                
                int rowAffected = 0;
                var response = "";
                
                if(name >= 0)
                {
                    cmd3 = "delete from public.tbl_user_cart where \"userID\" = " + obj.userID + " and \"shopProductID\" = " + ShopProductID + " ";
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
                        response = "Product not exists in Cart";
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

        [HttpPost("deleteAllUserCart")]
        public IActionResult deleteAllUserCart(UserCartCreation obj)
        {
            try
            {
                DateTime curDate = DateTime.Today;

                DateTime curTime = DateTime.Now;

                var time = curTime.ToString("HH:mm");
                var found = false;
                // var name = 0;
                //var newUserFavoriteID = 0;
                // var ShopProductID = 0;
                
                int rowAffected = 0;
                var response = "";
                
                cmd3 = "delete from public.tbl_user_cart where \"userID\" = " + obj.userID + " ";
                
                using (NpgsqlConnection con = new NpgsqlConnection(_dbCon.Value.dbCon))
                {
                    rowAffected = con.Execute(cmd3);
                }

                if (rowAffected > 0)
                {
                    response = "Success";
                }
                else
                {
                    if (found == true)
                    {
                        response = "Product not exist in Cart";
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