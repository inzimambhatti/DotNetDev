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
    public class CheckoutController : ControllerBase
    {
        
        private string cmd, cmd2, cmd3, cmd4, cmd5,cmd6;
        private readonly IOptions<conStr> _dbCon;

        public CheckoutController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        }

        [HttpGet("getOrderDetail")]
        public IActionResult getOrderDetail(int orderID,int userID)
        {
            try
            {
                cmd = "select * from public.\"view_OrderDetail\" where \"orderID\" = " + orderID + " and \"createdBy\" = " + userID + "";
                var appMenu = dapperQuery.QryResult<OrderInformation>(cmd, _dbCon);

                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getOrderHistory")]
        public IActionResult getOrderHistory(int orderID,int userID)
        {
            try
            {   
                if (orderID == 0)
                {
                    cmd = "select * from public.\"view_shopProductOrderDetail\" where \"userID\" = " + userID + "";    
                }
                else
                {
                    cmd = "select * from public.\"view_shopProductOrderDetail\" where \"orderID\" = " + orderID + " and \"userID\" = " + userID + "";
                }
                var appMenu = dapperQuery.QryResult<OrderHistory>(cmd, _dbCon);

                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getAllOrder")]
        public IActionResult getAllOrder(int userID)
        {
            try
            {
                cmd = "SELECT DISTINCT s.\"shopName\", s.\"shopOwnerName\", ord.\"customerName\", ord.\"orderID\", orddet.\"createdBy\", "+
                        "ord.status, ord.mobile,ord.address,ord.\"orderDate\",orddet.\"userID\",u.\"userFullName\",u.\"userMobile\",u.\"userEmail\",  "+
                        "s.\"userID\" AS \"sellerID\",sum(orddet.price) AS \"totalPrice\",s.\"shopID\",p.\"productTypeID\", 1 as \"userMode\", s.\"shopLogoDoc\",s.\"shopEDocExtension\",u.\"userImageDoc\",u.\"userEDocExtension\" "+
                        ",(select distinct cm.\"roomid\" from tbl_chat_messages cm where cm.\"userid\" = orddet.\"userID\" and cm.\"sellerid\" = s.\"userID\" ORDER BY cm.\"roomid\" ASC LIMIT 1) as \"roomID\" "+
                        "FROM \"tbl_orderDetail\" orddet JOIN tbl_order ord ON ord.\"orderID\" = orddet.\"orderID\" JOIN tbl_products p ON p.\"productID\" = orddet.\"productID\" "+
                        "JOIN tbl_shop_products sp ON sp.\"productID\" = p.\"productID\" JOIN tbl_shops s ON s.\"shopID\" = sp.\"shopID\" JOIN tbl_users u ON u.\"userID\" = orddet.\"userID\" "+
                        "WHERE orddet.\"isDeleted\" = '0'::\"bit\" and s.\"userID\" =  " + userID + " GROUP BY p.\"productTypeID\", s.\"shopID\", s.\"userID\", s.\"shopName\", s.\"shopOwnerName\", ord.\"customerName\", ord.\"orderID\", orddet.\"createdBy\", ord.status, ord.mobile, ord.address, ord.\"orderDate\", orddet.\"userID\", u.\"userFullName\", u.\"userMobile\", u.\"userEmail\" "+
                        ",s.\"shopLogoDoc\",s.\"shopEDocExtension\",u.\"userImageDoc\",u.\"userEDocExtension\" UNION SELECT DISTINCT s.\"shopName\",s.\"shopOwnerName\",ord.\"customerName\",ord.\"orderID\",orddet.\"createdBy\",ord.status, "+
                        "ord.mobile,ord.address,ord.\"orderDate\",orddet.\"userID\",u.\"userFullName\",u.\"userMobile\",u.\"userEmail\",s.\"userID\" AS \"sellerID\", "+
                        "sum(orddet.price) AS \"totalPrice\", s.\"shopID\",p.\"productTypeID\",0 as \"userMode\", s.\"shopLogoDoc\",s.\"shopEDocExtension\",u.\"userImageDoc\",u.\"userEDocExtension\" "+
                        ",(select distinct cm.\"roomid\" from tbl_chat_messages cm where cm.\"userid\" = orddet.\"userID\" and cm.\"sellerid\" = s.\"userID\" ORDER BY cm.\"roomid\" ASC LIMIT 1) as \"roomID\" "+
                        "FROM \"tbl_orderDetail\" orddet JOIN tbl_order ord ON ord.\"orderID\" = orddet.\"orderID\" JOIN tbl_products p ON p.\"productID\" = orddet.\"productID\" JOIN tbl_shop_products sp ON sp.\"productID\" = p.\"productID\" "+
                        "JOIN tbl_users u ON u.\"userID\" = orddet.\"userID\" "+
                        "JOIN tbl_shops s ON s.\"shopID\" = sp.\"shopID\"  WHERE orddet.\"isDeleted\" = '0'::\"bit\" and orddet.\"userID\" =  " + userID + " "+
                        "GROUP BY p.\"productTypeID\", s.\"shopID\", s.\"userID\", s.\"shopName\", s.\"shopOwnerName\", ord.\"customerName\", ord.\"orderID\", orddet.\"createdBy\", ord.status, ord.mobile, ord.address, ord.\"orderDate\", orddet.\"userID\", u.\"userFullName\", u.\"userMobile\", u.\"userEmail\",s.\"shopLogoDoc\",s.\"shopEDocExtension\",u.\"userImageDoc\",u.\"userEDocExtension\"";
                
                var appMenu = dapperQuery.QryResult<AllOrderDetail>(cmd, _dbCon);

                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getNotification")]
        public IActionResult getNotification(int userID)
        {
            try
            {
                cmd = "select * from public.\"view_notify\" where \"userID\"="+userID+"";
                var appMenu = dapperQuery.QryResult<Notification>(cmd, _dbCon);

                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }


        [HttpPost("checkout")]
        public IActionResult checkout(OrderCreation obj)
        {
            try
            {
                DateTime curDate = DateTime.Today;
                DateTime curTime = DateTime.Now;

                var time = curTime.ToString("HH:mm");

                int rowAffected = 0;
                int rowAffected2 = 0;
                int rowAffected3 = 0;
                var response = "";
                int newOrderDetailID = 0;
                int newOrderID = 0;

                List<Order> appMenuOrder = new List<Order>();
                
                List<Order> appMenuOrderID = new List<Order>();
                cmd6 = "select \"orderID\" from tbl_order ORDER BY \"orderID\" DESC LIMIT 1";
                appMenuOrderID = (List<Order>)dapperQuery.QryResult<Order>(cmd6, _dbCon);

                if(appMenuOrderID.Count == 0)
                    {
                        newOrderID = 1;
                    }else{
                        newOrderID = appMenuOrderID[0].orderID+1;
                    }

                cmd = "insert into public.\"tbl_order\" (\"orderID\",\"orderDate\", \"customerName\", \"email\", \"mobile\", \"address\",\"status\", \"createdOn\", \"isDeleted\") values (" + newOrderID + ",'" + curDate + "', '" + obj.customerName + "', '" + obj.email + "', '" + obj.mobile + "', '" + obj.address + "','pend', '" + curDate + "', B'0')";

                using (NpgsqlConnection con = new NpgsqlConnection(_dbCon.Value.dbCon))
                {
                    rowAffected = con.Execute(cmd);
                }

                //confirmation of data saved in order table
                if (rowAffected > 0)
                {
                    
                    //convert string to json data to insert in invoice detail table
                    var invObject = JsonConvert.DeserializeObject<List<OrderDetailCreation>>(obj.json);


                    //saving json data one by one in invoice detail table
                    foreach (var item in invObject)
                    {   
                        List<OrderDetailCreation> appMenuOrderDetailID = new List<OrderDetailCreation>();
                        cmd6 = "select \"orderDetailID\" from public.\"tbl_orderDetail\" ORDER BY \"orderDetailID\" DESC LIMIT 1";
                        appMenuOrderDetailID = (List<OrderDetailCreation>)dapperQuery.QryResult<OrderDetailCreation>(cmd6, _dbCon);

                        if(appMenuOrderDetailID.Count == 0)
                            {
                                newOrderDetailID = 1;
                            }else{
                                newOrderDetailID = appMenuOrderDetailID[0].orderDetailID+1;
                            }

                        // cmd3 = "insert into public.\"invoiceDetail\" (\"invoiceNo\", \"productID\", \"locationID\", \"qty\", \"costPrice\", \"salePrice\", \"debit\", \"credit\", \"discount\", \"productName\", \"coaID\", \"createdOn\", \"createdBy\", \"isDeleted\") values ('" + invoiceNo + "', '" + item.productID + "', '" + item.locationID + "', '" + item.qty + "', '" + item.costPrice + "', '" + item.salePrice + "', 0, '" + item.qty * item.salePrice + "', '" + item.discount + "', '" + item.productName + "', '1', '" + curDate + "', " + obj.userID + ", B'0')";
                        cmd3 = "INSERT INTO public.\"tbl_orderDetail\"(\"orderDetailID\",\"orderID\", \"productID\", qty, price, \"createdOn\",\"createdBy\", \"isDeleted\",\"userID\") VALUES (" + newOrderDetailID + "," + newOrderID + ", " + item.productID + ",  '" + item.qty + "',  '" + item.salePrice + "', '" + curDate + "',  " + obj.userID + ", B'0'," + obj.userID + " )";
                        using (NpgsqlConnection con = new NpgsqlConnection(_dbCon.Value.dbCon))
                        {
                            rowAffected2 = con.Execute(cmd3);
                        }
                            // if (item.productTypeID == 2)
                            // {
                            //     cmd = "INSERT INTO public.\"tbl_slot_appointment\"(\"slotAppointmentID\",\"orderID\", \"productID\", qty, price, \"createdOn\",\"createdBy\", \"isDeleted\",\"userID\") VALUES (" + newOrderDetailID + "," + newOrderID + ", " + item.productID + ",  '" + item.qty + "',  '" + item.salePrice + "', '" + curDate + "',  " + obj.userID + ", B'0'," + obj.userID + " )";
                            //     using (NpgsqlConnection con = new NpgsqlConnection(_dbCon.Value.dbCon))
                            //     {
                            //         rowAffected3 = con.Execute(cmd);
                            //     }
                            // }

                        cmd4 = "SELECT \"productID\", qty FROM \"tbl_products\" where \"productID\" = " + item.productID + " AND \"isDeleted\"::int = 0";
                        var appMenu = (List<Product>)dapperQuery.QryResult<Product>(cmd4, _dbCon);

                        var availQty = appMenu[0].qty - item.qty;

                        cmd5 = "update public.\"tbl_products\" set qty = " + availQty + " where \"productID\" = " + item.productID + ";";


                        using (NpgsqlConnection con = new NpgsqlConnection(_dbCon.Value.dbCon))
                        {
                            rowAffected3 = con.Execute(cmd5);
                        }

                        
                    }

                }

                if (rowAffected > 0 && rowAffected2 > 0)
                {
                    response = "Success";
                }
                else
                {
                    response = "Server Issue";
                }

                return Ok(new { message = response });
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

        [HttpPost("cancelOrder")]
        public IActionResult cancelOrder(OrderCreation obj)
        {
            try
            {
                DateTime curDate = DateTime.Today;
                DateTime curTime = DateTime.Now;

                var time = curTime.ToString("HH:mm");

                int rowAffected = 0;
                var response = "";

                cmd = "update public.\"tbl_order\" set status = 'canl' where \"orderID\" = " + obj.orderID + ";";

                // cmd = "insert into public.\"Order\" (\"orderDate\", \"customerName\", \"email\", \"mobile\", \"address\", \"createdOn\", \"isDeleted\") values ('" + obj.orderDate + "', '" + obj.customerName + "', " + obj.email + ", " + obj.mobile + ", '" + obj.address + "', '" + curDate + "', B'0')";

                using (NpgsqlConnection con = new NpgsqlConnection(_dbCon.Value.dbCon))
                {
                    rowAffected = con.Execute(cmd);
                }

                if (rowAffected > 0)
                {
                    response = "Success";
                }
                else
                {
                    response = "Order did not exists";
                }

                return Ok(new { message = response });
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

        [HttpPost("acceptOrDeclineOrder")]
        public IActionResult acceptOrDeclineOrder(OrderCreation obj)
        {
            try
            {
                DateTime curDate = DateTime.Today;
                DateTime curTime = DateTime.Now;

                var time = curTime.ToString("HH:mm");

                int rowAffected = 0;
                var response = "";

                cmd2 = "update public.\"tbl_order\" set status = '" + obj.status + "' where \"orderID\" = " + obj.orderID + ";";

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
                    response = "Order did not exists";
                }

                return Ok(new { message = response });
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }
        
    }
}