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
    public class ServiceController : ControllerBase
    {
        
        private string cmd,cmd2,cmd3;
        private readonly IOptions<conStr> _dbCon;

        public ServiceController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        }

        [HttpGet("getShopServices")]
        public IActionResult getShopServices(int userID)
        {
            try
            {
                cmd = "select * from public.\"view_personalBalance\" where \"userID\" = " + userID + " ";
                var appMenu = dapperQuery.Qry<DashboardView>(cmd, _dbCon);

                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

        [HttpGet("getBookedServices")]
        public IActionResult getBookedServices(int userID)
        {   
            try
            {
                
                cmd = "SELECT DISTINCT s.\"shopName\",s.\"shopOwnerName\",u.\"userID\",u.\"userFullName\", "+
                        "u.\"userMobile\",u.\"userEmail\",s.\"userID\" AS \"sellerID\",p.\"rateOfSale\" AS \"totalPrice\", s.\"shopID\",p.\"productTypeID\",sa.\"slotBookingID\",sa.\"bookingDate\",sa.\"isAppointed\", "+
                        "sa.\"bookingEndTime\"::text AS \"bookingEndTime\",sa.\"bookingStartTime\"::text AS \"bookingStartTime\",sa.status,p.\"productName\",1 as \"userMode\" ,s.\"shopAddress\",ua.\"userAddress\",s.\"shopMobile\",u.\"userMobile\" "+
                        ",avg(sr.\"shopRating\")::numeric(10,2) AS \"shopRating\",u.\"userImageDoc\",u.\"userEDocExtension\",s.\"shopLogoDoc\",s.\"shopEDocExtension\", "+
                        "(select distinct  cm.\"roomid\" from tbl_chat_messages cm where cm.\"userid\" = u.\"userID\" and cm.\"sellerid\" = s.\"userID\") as \"roomID\" "+
                        "FROM tbl_slot_appointment sa JOIN tbl_products p ON p.\"productID\" = sa.\"productID\" JOIN tbl_shop_products sp ON sp.\"productID\" = p.\"productID\" JOIN tbl_shops s ON s.\"shopID\" = sp.\"shopID\" "+
                        "JOIN tbl_users u ON u.\"userID\" = sa.\"userID\" left JOIN tbl_user_address as ua on ua.\"userID\" = u.\"userID\" left join tbl_shop_rating sr on sr.\"shopID\" = s.\"shopID\"  where s.\"userID\" = " + userID + " "+
                        "GROUP BY sa.\"slotBookingID\", p.\"rateOfSale\", p.\"productTypeID\", s.\"shopID\",s.\"userID\", s.\"shopName\", s.\"shopOwnerName\", u.\"userFullName\", u.\"userMobile\", u.\"userEmail\", sa.\"bookingDate\", sa.\"isAppointed\", "+
                        "sa.\"bookingStartTime\", sa.\"bookingEndTime\", sa.status, u.\"userID\", p.\"productName\" ,s.\"shopAddress\",ua.\"userAddress\",s.\"shopMobile\",u.\"userMobile\",u.\"userImageDoc\",u.\"userEDocExtension\",s.\"shopLogoDoc\",s.\"shopEDocExtension\" "+
                        "UNION "+
                        "SELECT DISTINCT s.\"shopName\",s.\"shopOwnerName\",u.\"userID\",u.\"userFullName\",u.\"userMobile\",u.\"userEmail\",s.\"userID\" AS \"sellerID\",p.\"rateOfSale\" AS \"totalPrice\",s.\"shopID\",p.\"productTypeID\", "+
                        "sa.\"slotBookingID\",sa.\"bookingDate\",sa.\"isAppointed\",sa.\"bookingEndTime\"::text AS \"bookingEndTime\",sa.\"bookingStartTime\"::text AS \"bookingStartTime\",sa.status,p.\"productName\",0 as \"userMode\" ,s.\"shopAddress\" "+
                        ",ua.\"userAddress\",s.\"shopMobile\",u.\"userMobile\",avg(sr.\"shopRating\")::numeric(10,2) AS \"shopRating\",u.\"userImageDoc\",u.\"userEDocExtension\",s.\"shopLogoDoc\",s.\"shopEDocExtension\" "+
                        ",(select distinct cm.\"roomid\" from tbl_chat_messages cm where cm.\"userid\" = u.\"userID\" and cm.\"sellerid\" = s.\"userID\") as \"roomID\" "+
                        "FROM tbl_slot_appointment sa JOIN tbl_products p ON p.\"productID\" = sa.\"productID\" JOIN tbl_shop_products sp ON sp.\"productID\" = p.\"productID\" JOIN tbl_shops s ON s.\"shopID\" = sp.\"shopID\" "+
                        "JOIN tbl_users u ON u.\"userID\" = sa.\"userID\" left JOIN tbl_user_address as ua on ua.\"userID\" = u.\"userID\" left join tbl_shop_rating sr on sr.\"shopID\" = s.\"shopID\" where sa.\"userID\" = " + userID + " "+
                        "GROUP BY sa.\"slotBookingID\", p.\"rateOfSale\", p.\"productTypeID\", s.\"shopID\",s.\"userID\", s.\"shopName\", s.\"shopOwnerName\", u.\"userFullName\", u.\"userMobile\", u.\"userEmail\", sa.\"bookingDate\", sa.\"isAppointed\", sa.\"bookingStartTime\", sa.\"bookingEndTime\", sa.status, u.\"userID\", p.\"productName\" "+
                        ",s.\"shopAddress\",ua.\"userAddress\",s.\"shopMobile\",u.\"userMobile\",u.\"userImageDoc\",u.\"userEDocExtension\",s.\"shopLogoDoc\",s.\"shopEDocExtension\"";    
                

                var response = dapperQuery.Qry<SlotBooking>(cmd,_dbCon);
                return Ok(response);

            }
            catch (Exception e)
            {
                return Ok(e);
            }
            
        }

        [HttpPost("saveServiceBooking")]
        public IActionResult saveServiceBooking(BookingCreation obj)
        {
            try
            {
                DateTime curDate = DateTime.Today;

                DateTime curTime = DateTime.Now;

                var time = curTime.ToString("HH:mm");
                var found = false;
                var name = false;
                var newServiceID = 0;
                //var newshopProductID = 0;

                List<SlotBooking> appMenuService = new List<SlotBooking>();
                cmd = "select \"isAppointed\" from tbl_slot_appointment where \"isAppointed\" = B'1' and \"productID\" = " + obj.productID + " and \"bookingDate\" = '"+ obj.bookingDate +"' and ((\"bookingStartTime\" between '" + obj.bookingStartTime + "' and '" + obj.bookingEndTime + "') OR (\"bookingEndTime\" between '" + obj.bookingStartTime + "' and '" + obj.bookingEndTime + "'))";
                appMenuService = (List<SlotBooking>)dapperQuery.QryResult<SlotBooking>(cmd, _dbCon);

                if (appMenuService.Count > 0)
                    name = appMenuService[0].isAppointed;

                List<SlotBooking> appMenuServiceID = new List<SlotBooking>();
                cmd2 = "select \"slotBookingID\" from tbl_slot_appointment ORDER BY \"slotBookingID\" DESC LIMIT 1";
                appMenuServiceID = (List<SlotBooking>)dapperQuery.QryResult<SlotBooking>(cmd2, _dbCon);

                if(appMenuServiceID.Count == 0)
                    {
                        newServiceID = 1;
                    }else{
                        newServiceID = appMenuServiceID[0].slotBookingID+1;
                    }

                int rowAffected = 0;
                var response = "";
                
                if(name == false)
                {
                    cmd3 = "insert into public.tbl_slot_appointment (\"slotBookingID\",\"bookingDate\",\"isAppointed\",\"userID\",\"productID\",\"bookingStartTime\",\"bookingEndTime\",\"status\") Values (" + newServiceID + ", '" + obj.bookingDate + "', B'1',"+ obj.userID +",'" + obj.productID + "','" + obj.bookingStartTime + "','" + obj.bookingEndTime + "','pend')";
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
                        response = "Slot already booked, Please try different time.";
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

        [HttpPost("acceptOrDeclineOrder")]
        public IActionResult acceptOrDeclineOrder(BookingCreation obj)
        {
            try
            {
                DateTime curDate = DateTime.Today;
                DateTime curTime = DateTime.Now;

                var time = curTime.ToString("HH:mm");

                int rowAffected = 0;
                var response = "";

                cmd2 = "update public.tbl_slot_appointment set \"isAppointed\" = B'" + obj.isAppointed + "' , \"status\" = '" + obj.status + "' where \"slotBookingID\" = " + obj.slotBookingID + "";

                // cmd = "insert into public.\"Order\" (\"orderDate\", \"customerName\", \"email\", \"mobile\", \"address\", \"createdOn\", \"isDeleted\") values ('" + obj.orderDate + "', '" + obj.customerName + "', " + obj.email + ", " + obj.mobile + ", '" + obj.address + "', '" + curDate + "', B'0')";

                using (NpgsqlConnection con = new NpgsqlConnection(_dbCon.Value.dbCon))
                {
                    rowAffected = con.Execute(cmd2);
                }

                if (rowAffected > 0)
                {
                    response = "Success";
                }
                else
                {
                    response = "Service did not exists";
                }

                return Ok(new { message = response });
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

        [HttpPost("cancelBooking")]
        public IActionResult cancelBooking(BookingCreation obj)
        {   
            try
            {
                int rowAffected = 0;
                var response = "";

                cmd = "update public.tbl_slot_appointment set \"isAppointed\" = B'0' , \"status\" = '" + obj.status + "' where \"slotBookingID\" = " + obj.slotBookingID + "";

                using(NpgsqlConnection con =  new NpgsqlConnection(_dbCon.Value.dbCon))
                    {
                        rowAffected = con.Execute(cmd);
                    }            

                if (rowAffected > 0)
                {
                    response = "Success";
                    return Ok(new{message = response});
                }
                else
                {
                    response = "Record does not exists";
                    return BadRequest(new{message = response});
                }
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("saveService")]
        public IActionResult saveService(ServiceCreation obj)
        {
            try
            {
                DateTime curDate = DateTime.Today;

                DateTime curTime = DateTime.Now;

                var time = curTime.ToString("HH:mm");
                var found = false;
                var name = "";
                var newServiceID = 0;
                var response = "";
                //var newshopProductID = 0;
                var NoCount = 0;

                List<Service> appMenuService = new List<Service>();
                cmd = "select s.\"serviceTitle\" from tbl_shop_services s WHERE s.\"isDeleted\"=\'0\' and s.\"serviceTitle\"='"+obj.serviceTitle+"' and s.\"shopID\" = " + obj.shopID + "";
                appMenuService = (List<Service>)dapperQuery.QryResult<Service>(cmd, _dbCon);

                if (appMenuService.Count > 0)
                    name = appMenuService[0].serviceTitle;

                List<shopAllow> appMenuProductCount = new List<shopAllow>();
                cmd = "select count(\"shopServiceID\") as \"shopCount\" from tbl_shop_services p WHERE p.\"isDeleted\"=\'0\' and p.\"shopID\" = " + obj.shopID + " ";
                appMenuProductCount = (List<shopAllow>)dapperQuery.QryResult<shopAllow>(cmd, _dbCon);

                if (appMenuProductCount.Count > 0)
                    NoCount = appMenuProductCount[0].shopCount;

                if (NoCount <= 2)
                {

                    NoCount = NoCount+1;
                    List<Service> appMenuServiceID = new List<Service>();
                    cmd2 = "select \"shopServiceID\" from tbl_shop_services ORDER BY \"shopServiceID\" DESC LIMIT 1";
                    appMenuServiceID = (List<Service>)dapperQuery.QryResult<Service>(cmd2, _dbCon);

                    if(appMenuServiceID.Count == 0)
                        {
                            newServiceID = 1;
                        }else{
                            newServiceID = appMenuServiceID[0].shopServiceID+1;
                        }

                    int rowAffected = 0;
                    
                    
                    if(name=="")
                    {
                        cmd3 = "insert into public.tbl_shop_services (\"shopServiceID\",\"serviceTitle\",\"pricePerHour\",\"serviceDescription\",\"serviceEDoc\",\"serviceEDocExtension\",\"createdOn\",\"createdBy\",\"isDeleted\",\"shopID\",\"NoOfItem\") Values (" + newServiceID + ", '" + obj.serviceTitle + "', " + obj.pricePerHour + ",'" + obj.serviceDescription + "','" + obj.applicationEDocPath + "','" + obj.applicationEdocExtenstion + "','" + curDate + "'," + obj.userID + ",B'0'," + obj.shopID + ", " + NoCount + ")";
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
                                newServiceID.ToString(),
                                obj.applicationEDoc,
                                obj.applicationEdocExtenstion);
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
                            response = "Service already exists";
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


        [HttpPost("updateService")]
        public IActionResult updateService(ServiceCreation obj)
        {
            try
            {
                DateTime curDate = DateTime.Today;

                DateTime curTime = DateTime.Now;

                var time = curTime.ToString("HH:mm");
                var found = false;
                // var name = "";

                // List<Product> appMenuProduct = new List<Product>();
                // cmd = "SELECT \"productName\" from tbl_products WHERE \"isDeleted\"=\'0\' and \"productName\"='"+obj.productName+"'";
                // appMenuProduct = (List<Product>)dapperQuery.QryResult<Product>(cmd, _dbCon);

                // if (appMenuProduct.Count > 0)
                //     name = appMenuProduct[0].productName;

                int rowAffected = 0;
                var response = "";

                
                if(obj.shopServiceID != 0)
                {
                    cmd3 = "update public.tbl_shop_services set \"serviceTitle\" = '" + obj.serviceTitle + "',\"pricePerHour\" = " + obj.pricePerHour + ",\"serviceDescription\" = " + obj.serviceDescription + ",\"serviceEDoc\" = '" + obj.applicationEDocPath + "',\"serviceEDocExtension\" = '" + obj.applicationEdocExtenstion + "' where \"shopServiceID\" = " + obj.shopServiceID + ";";
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
                            obj.shopServiceID.ToString(),
                            obj.applicationEDoc,
                            obj.applicationEdocExtenstion);
                    }
                }

                if (rowAffected > 0 )
                {
                    
                    response = "Success";
                    return Ok(new { message = response });
                }
                else
                {
                    if (found == true)
                    {
                        response = "Service not found";
                        return BadRequest(new { message = response });
                    }
                    else
                    {
                        response = "Server Issue";
                        return BadRequest(new { message = response });
                    }
                }
                
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("deleteService")]
        public IActionResult deleteService(ServiceCreation obj)
        {
            try
            {
                DateTime curDate = DateTime.Today;

                DateTime curTime = DateTime.Now;

                var time = curTime.ToString("HH:mm");
                var found = false;
                // var name = "";

                int rowAffected = 0;
                var response = "";

                
                if(obj.shopServiceID != 0)
                {
                    cmd3 = "update public.tbl_shop_services set \"isDeleted\" = B'1' where \"shopServiceID\" = " + obj.shopServiceID + ";";
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
                        response = "Service not found";
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