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
using System.Threading.Tasks;

namespace virtualCoreApi.Controllers
{   
    [ApiController]
    [Route("[controller]")]
    public class ShopController : ControllerBase
    {
        
        private string cmd,cmd2,cmd3,cmd4;
        private readonly IOptions<conStr> _dbCon;

        public ShopController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        }

        [HttpPost("pay")]
        public Task<dynamic> pay(PaymentModel pm)
        {
            return MakePayment.PayAsync(pm.cardnumber,pm.month,pm.year,pm.cvc,pm.value);
        }

        [HttpGet("getCity")]
        public IActionResult getCity(int countryID)
        {
            try
            {
                cmd = "SELECT * FROM tbl_city WHERE \"countryID\" = " + countryID + ";";
                var appMenu = dapperQuery.Qry<Cities>(cmd, _dbCon);

                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

        [HttpGet("getShopPlan")]
        public IActionResult getShopPlan()
        {
            try
            {
                cmd = "SELECT * FROM public.tbl_shop_plan ";
                var appMenu = dapperQuery.Qry<ShopPlan>(cmd, _dbCon);

                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

        [HttpGet("getProductPlan")]
        public IActionResult getProductPlan()
        {
            try
            {
                cmd = "SELECT * FROM public.tbl_product_plan ";
                var appMenu = dapperQuery.Qry<ProductPlan>(cmd, _dbCon);

                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

        [HttpGet("getShop")]
        public IActionResult getShop(int shopID)
        {
            try
            {
                cmd = "SELECT * FROM view_shop WHERE \"shopID\" = " + shopID + ";";
                var appMenu = dapperQuery.Qry<Shop>(cmd, _dbCon);

                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

        [HttpGet("getShopList")]
        public IActionResult getShopList()
        {
            try
            {
                cmd = "SELECT * FROM view_shop;";
                var appMenu = dapperQuery.Qry<Shop>(cmd, _dbCon);

                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

        [HttpGet("getUserShops")]
        public IActionResult getUserShops(int userID)
        {
            try
            {
                cmd = "SELECT * FROM view_shop WHERE \"userID\" = " + userID + " ;";
                var appMenu = dapperQuery.Qry<Shop>(cmd, _dbCon);

                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

        [HttpGet("getShopCategory")]
        public IActionResult getShopCategory()
        {
            try
            {
                cmd = "SELECT sc.\"shopCategoryID\",sc.\"shopCategoryTitle\",sc.\"shopTypeID\",st.\"shopTypeTitle\" FROM tbl_shop_categories sc left join tbl_shop_types st on st.\"shopTypeID\" = sc.\"shopTypeID\";";
                var appMenu = dapperQuery.Qry<ShopCategory>(cmd, _dbCon);

                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

        [HttpGet("getShopType")]
        public IActionResult getShopType()
        {
            try
            {
                cmd = "SELECT * FROM tbl_shop_types;";
                var appMenu = dapperQuery.Qry<ShopType>(cmd, _dbCon);

                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

        [HttpPost("saveShop")]
        public IActionResult saveShop(ShopCreation obj)
        {
            try
            {
                DateTime curDate = DateTime.Today;

                DateTime curTime = DateTime.Now;

                var time = curTime.ToString("HH:mm");
                var found = false;
                var name = "";
                var newShopID = 0;
                var NoCount = 0;

                List<Shop> appMenuShop = new List<Shop>();
                cmd = "SELECT \"shopName\" from tbl_shops WHERE \"isDeleted\"=\'0\' and \"shopName\"='" + obj.shopName + "'";
                appMenuShop = (List<Shop>)dapperQuery.QryResult<Shop>(cmd, _dbCon);

                if (appMenuShop.Count > 0)
                    name = appMenuShop[0].shopName;

                List<shopAllow> appMenuShopCount = new List<shopAllow>();
                cmd = "SELECT Count(\"shopID\") as \"shopCount\" from tbl_shops WHERE \"isDeleted\"=\'0\' and \"userID\"='" + obj.userID + "'";
                appMenuShopCount = (List<shopAllow>)dapperQuery.QryResult<shopAllow>(cmd, _dbCon);

                if (appMenuShopCount.Count > 0)
                    NoCount = appMenuShopCount[0].shopCount;

                List<Shop> appMenuShopID = new List<Shop>();
                cmd2 = "select \"shopID\" from tbl_shops ORDER BY \"shopID\" DESC LIMIT 1";
                appMenuShopID = (List<Shop>)dapperQuery.QryResult<Shop>(cmd2, _dbCon);

                if(appMenuShopID.Count == 0)
                    {
                        newShopID = 1;
                    }else{
                        newShopID = appMenuShopID[0].shopID+1;
                    }

                int rowAffected = 0;
                int rowAffected2 = 0;
                var response = "";
                if (NoCount == 0 )
                {
                    NoCount = NoCount+1;
                
                    if(name=="")
                    {
                        cmd3 = "insert into public.tbl_shops (\"shopID\",\"shopName\",\"shopOwnerName\",\"shopTypeID\",\"shopCategoryID\",\"userID\",\"shopDescription\",\"shopLogoDoc\",\"shopEDocExtension\",\"shopOpenTime\",\"shopClosingTime\",\"cityID\",\"currencyID\",\"shopAddress\",\"shopMobile\",\"createdOn\",\"createdBy\",\"isDeleted\",\"NoOfItem\") Values (" + newShopID + ", '" + obj.shopName + "','" + obj.shopOwnerName + "', " + obj.shopTypeID + ","+obj.shopCategoryID+","+obj.userID+",'"+obj.shopDescription+"','"+obj.applicationEDocPath+"','" + obj.applicationEdocExtenstion + "','"+obj.shopOpenTime+"','"+obj.shopClosingTime+"'," + obj.cityID + ",1,'"+obj.shopAddress+"','"+obj.shopMobile+"','"+curDate+"',"+obj.userID+",0," + NoCount + ")";
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

                        if (obj.applicationEDocPath != null && obj.applicationEDocPath != "")
                        {
                            dapperQuery.saveImageFile(
                                obj.applicationEDocPath,
                                newShopID.ToString(),
                                obj.applicationEDoc,
                                obj.applicationEdocExtenstion);
                        }
                    }

                    if (rowAffected > 0)
                    {
                        cmd4 = "update public.\"tbl_users\" set \"userMode\" = 1 where \"userID\" = " + obj.userID + ";";

                        using (NpgsqlConnection con = new NpgsqlConnection(_dbCon.Value.dbCon))
                        {
                            rowAffected2 = con.Execute(cmd4);
                        }

                        response = "Success";
                        return Ok(new { message = response });
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
                        return BadRequest(new { message = response });
                    }
                }
                else
                {
                    
                    response = "Please Upgrade your plan";
                    return BadRequest(new { message = response });
                }
                
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("updateShop")]
        public IActionResult updateShop(ShopCreation obj)
        {
            try
            {
                DateTime curDate = DateTime.Today;

                DateTime curTime = DateTime.Now;

                var time = curTime.ToString("HH:mm");
                var found = false;
                var name = "";

                List<Shop> appMenuShop = new List<Shop>();
                cmd = "SELECT \"shopName\" from tbl_shops WHERE \"isDeleted\"=\'0\' and \"shopName\"='"+obj.shopName+"'";
                appMenuShop = (List<Shop>)dapperQuery.QryResult<Shop>(cmd, _dbCon);

                if (appMenuShop.Count > 0)
                    name = appMenuShop[0].shopName;

                int rowAffected = 0;
                var response = "";

                
                if(obj.shopID != 0)
                {
                    cmd3 = "update public.tbl_shops set \"shopName\" = '" + obj.shopName + "',\"shopOwnerName\" = '" + obj.shopOwnerName + "',\"shopTypeID\" = " + obj.shopTypeID + ",\"shopCategoryID\" = " + obj.shopCategoryID + ",\"shopDescription\" = '" + obj.shopDescription + "',\"shopLogoDoc\" = '" + obj.applicationEDocPath + "',\"shopEDocExtension\" = '" + obj.applicationEdocExtenstion + "',\"shopOpenTime\" = '" + obj.shopOpenTime + "',\"shopClosingTime\" = '" + obj.shopClosingTime + "',\"cityID\" = " + obj.cityID + ",\"shopAddress\" = '" + obj.shopAddress + "',\"shopMobile\" = '" + obj.shopMobile + "',\"modifiedOn\" = '" + curDate + "',\"modifiedBy\" = " + obj.userID + " where \"userID\" = " + obj.userID + " and \"shopID\" = " + obj.shopID + "";
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

                    if (obj.applicationEDocPath != null && obj.applicationEDocPath != "")
                    {
                        dapperQuery.saveImageFile(
                            obj.applicationEDocPath,
                            obj.shopID.ToString(),
                            obj.applicationEDoc,
                            obj.applicationEdocExtenstion);
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
                        response = "Shop not found";
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

        [HttpGet("getShopReviews")]
        public IActionResult getShopReviews(int shopID)
        {
            try
            {
                cmd = "select * from public.view_shop_reviews where \"shopID\" = " + shopID + ";";
                var appMenu = dapperQuery.Qry<ShopReviews>(cmd, _dbCon);

                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

        [HttpPost("saveShopRating")]
        public IActionResult saveShopRating(ShopRatingCreation obj)
        {
            try
            {
                DateTime curDate = DateTime.Today;

                DateTime curTime = DateTime.Now;

                var time = curTime.ToString("HH:mm");
                var found = false;
                var newShopRatingID = 0;

                List<ShopRatingCreation> appMenuShopID = new List<ShopRatingCreation>();
                cmd2 = "select \"shopRatingID\" from tbl_shop_rating ORDER BY \"shopRatingID\" DESC LIMIT 1";
                appMenuShopID = (List<ShopRatingCreation>)dapperQuery.QryResult<ShopRatingCreation>(cmd2, _dbCon);

                if(appMenuShopID.Count == 0)
                    {
                        newShopRatingID = 1;
                    }else{
                        newShopRatingID = appMenuShopID[0].shopRatingID+1;
                    }

                int rowAffected = 0;
                var response = "";

                
                if(obj.shopID != 0 && obj.userID != 0)
                {
                    cmd3 = "Insert into public.tbl_shop_rating(\"shopRatingID\",\"shopRating\",\"comment\",\"shopID\",\"userID\") values(" + newShopRatingID + "," + obj.shopRating + ",'" + obj.comment + "'," + obj.shopID + "," + obj.userID + ")";
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
                        response = "Shop not found";
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


        [HttpPost("saveShopClicks")]
        public IActionResult saveShopClicks(ShopRatingCreation obj)
        {
            try
            {
                DateTime curDate = DateTime.Today;

                DateTime curTime = DateTime.Now;

                var time = curTime.ToString("HH:mm");
                var found = false;

                List<ShopRatingCreation> appMenuShopID = new List<ShopRatingCreation>();
                cmd2 = "select \"userID\" from tbl_shop_clicks where \"userID\" = " + obj.userID + " and \"shopID\" = " + obj.shopID + "";
                appMenuShopID = (List<ShopRatingCreation>)dapperQuery.QryResult<ShopRatingCreation>(cmd2, _dbCon);

                int rowAffected = 0;
                var response = "";

                
                if(appMenuShopID.Count == 0)
                {
                    cmd3 = "Insert into public.tbl_shop_clicks(\"shopID\",\"userID\",\"isClicked\") values(" + obj.shopID + "," + obj.userID + ",B'1')";
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
                        response = "Click already exists";
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


        [HttpPost("deleteShop")]
        public IActionResult deleteShop(ShopCreation obj)
        {
            try
            {
                DateTime curDate = DateTime.Today;

                DateTime curTime = DateTime.Now;

                var time = curTime.ToString("HH:mm");
                var found = false;
                var name = "";

                List<Shop> appMenuShop = new List<Shop>();
                cmd = "SELECT \"shopName\" from tbl_shops WHERE \"isDeleted\"=\'0\' and \"shopName\"='"+obj.shopName+"'";
                appMenuShop = (List<Shop>)dapperQuery.QryResult<Shop>(cmd, _dbCon);

                if (appMenuShop.Count > 0)
                    name = appMenuShop[0].shopName;

                int rowAffected = 0;
                var response = "";

                
                if(obj.shopID != 0)
                {
                    cmd3 = "update public.tbl_shops set \"isDeleted\" = 1 where \"userID\" = " + obj.userID + " and \"shopID\" = " + obj.shopID + "";
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
                        response = "Shop not found";
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