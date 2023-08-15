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
    public class ProductController : ControllerBase
    {
        
        private string cmd,cmd2,cmd4,cmd3,cmd5,cmd6;
        private readonly IOptions<conStr> _dbCon;

        public ProductController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        }
    
        [HttpGet("getShopProduct")]
        public IActionResult getShopProduct(int shopID)
        {
            try
            {
                cmd = "SELECT * FROM view_product WHERE \"shopID\" = " + shopID + ";";
                var appMenu = dapperQuery.Qry<Product>(cmd, _dbCon);

                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

        [HttpGet("getMeasurementUnit")]
        public IActionResult getMeasurementUnit()
        {
            try
            {
                cmd = "SELECT * FROM tbl_measurement_unit";
                var appMenu = dapperQuery.Qry<MeasurementUnit>(cmd, _dbCon);

                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

        [HttpGet("getProductType")]
        public IActionResult getProductType()
        {
            try
            {
                cmd = "SELECT * FROM tbl_product_types ;";
                var appMenu = dapperQuery.Qry<ProductType>(cmd, _dbCon);

                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

        [HttpPost("saveProduct")]
        public IActionResult saveProduct(ProductCreation obj)
        {
            try
            {
                DateTime curDate = DateTime.Today;

                DateTime curTime = DateTime.Now;

                var time = curTime.ToString("HH:mm");
                var found = false;
                var name = "";
                var newProductID = 0;
                var newshopProductID = 0;
                var NoCount = 0;
                var response = "";
                var newProductImageID = 0;

                List<Product> appMenuProduct = new List<Product>();
                cmd = "select p.\"productName\" from tbl_products p join tbl_shop_products sp on p.\"productID\" = sp.\"productID\" WHERE p.\"isDeleted\"=\'0\' and p.\"productName\"='"+obj.productName+"' and sp.\"shopID\" = " + obj.shopID + "";
                appMenuProduct = (List<Product>)dapperQuery.QryResult<Product>(cmd, _dbCon);

                if (appMenuProduct.Count > 0)
                    name = appMenuProduct[0].productName;

                List<shopAllow> appMenuProductCount = new List<shopAllow>();
                cmd = "select Count(p.\"productID\") as \"shopCount\" from tbl_products p  join tbl_shop_products sp on p.\"productID\" = sp.\"productID\" WHERE p.\"isDeleted\"=\'0\' and sp.\"shopID\" = " + obj.shopID + " ";
                appMenuProductCount = (List<shopAllow>)dapperQuery.QryResult<shopAllow>(cmd, _dbCon);

                if (appMenuProductCount.Count > 0)
                    NoCount = appMenuProductCount[0].shopCount;

                if (NoCount <= 19)
                {
                    NoCount = NoCount+1;
                    
                    List<Product> appMenuProductID = new List<Product>();
                    cmd2 = "select \"productID\" from tbl_products ORDER BY \"productID\" DESC LIMIT 1";
                    appMenuProductID = (List<Product>)dapperQuery.QryResult<Product>(cmd2, _dbCon);

                    if(appMenuProductID.Count == 0)
                        {
                            newProductID = 1;
                        }else{
                            newProductID = appMenuProductID[0].productID+1;
                        }

                    int rowAffected = 0;
                    int rowAffected2 = 0;
                    
                    
                    if(name=="")
                    {
                        if (obj.qty > 0 && obj.rateOfPurchase > 0)
                        {
                            cmd3 = "insert into public.tbl_products (\"productID\",\"productName\",\"productTypeID\",\"shopCategoryID\",\"rateOfPurchase\",\"rateOfSale\",\"discount\",\"productDescription\",\"createdOn\",\"createdBy\",\"isDeleted\",\"qty\") Values (" + newProductID + ", '" + obj.productName + "', " + obj.productTypeID + ","+obj.shopCategoryID+",'"+obj.rateOfPurchase+"','"+obj.rateOfSale+"','"+obj.discount+"','"+obj.productDescription+"','"+curDate+"',"+obj.userID+",0," + obj.qty + ")";   
                        }
                        else if (obj.qty > 0 && obj.rateOfPurchase == 0)
                        {
                            cmd3 = "insert into public.tbl_products (\"productID\",\"productName\",\"productTypeID\",\"shopCategoryID\",\"rateOfSale\",\"discount\",\"productDescription\",\"createdOn\",\"createdBy\",\"isDeleted\",\"qty\") Values (" + newProductID + ", '" + obj.productName + "', " + obj.productTypeID + ","+obj.shopCategoryID+",'"+obj.rateOfSale+"','"+obj.discount+"','"+obj.productDescription+"','"+curDate+"',"+obj.userID+",0," + obj.qty + ")";
                        }
                        else if (obj.qty == 0 && obj.rateOfPurchase > 0)
                        {
                            cmd3 = "insert into public.tbl_products (\"productID\",\"productName\",\"productTypeID\",\"shopCategoryID\",\"rateOfPurchase\",\"rateOfSale\",\"discount\",\"productDescription\",\"createdOn\",\"createdBy\",\"isDeleted\") Values (" + newProductID + ", '" + obj.productName + "', " + obj.productTypeID + ","+obj.shopCategoryID+",'"+obj.rateOfPurchase+"','"+obj.rateOfSale+"','"+obj.discount+"','"+obj.productDescription+"','"+curDate+"',"+obj.userID+",0)";
                        }
                        else
                        {
                            cmd3 = "insert into public.tbl_products (\"productID\",\"productName\",\"productTypeID\",\"shopCategoryID\",\"rateOfSale\",\"discount\",\"productDescription\",\"createdOn\",\"createdBy\",\"isDeleted\") Values (" + newProductID + ", '" + obj.productName + "', " + obj.productTypeID + ","+obj.shopCategoryID+",'"+obj.rateOfSale+"','"+obj.discount+"','"+obj.productDescription+"','"+curDate+"',"+obj.userID+",0)";
                        }
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
                        var invObject = JsonConvert.DeserializeObject<List<ProductImagesCreation>>(obj.json);

                        foreach (var item in invObject)
                        {
                            List<ProductImagesCreation> appMenuProductImagesID = new List<ProductImagesCreation>();
                            cmd6 = "select \"productImageID\" from public.\"tbl_product_images\" ORDER BY \"productImageID\" DESC LIMIT 1";
                            appMenuProductImagesID = (List<ProductImagesCreation>)dapperQuery.Qry<ProductImagesCreation>(cmd6, _dbCon);

                            if(appMenuProductImagesID.Count == 0)
                                {
                                    newProductImageID = 1;
                                }else{
                                    newProductImageID = appMenuProductImagesID[0].productImageID+1;
                                }

                            if (item.applicationEDocPath != null && item.applicationEDocPath != "")
                            {

                                cmd4 = "insert into public.\"tbl_product_images\" (\"productImageID\",\"productID\",\"productImagePath\",\"productImageExt\",\"createdOn\",\"createdBy\",\"isDeleted\") Values (" + newProductImageID + "," + newProductID + ",'" + item.applicationEDocPath + "','" + item.applicationEdocExtenstion + "','" + curDate + "'," + obj.userID + ",B'0');";

                                using (NpgsqlConnection con = new NpgsqlConnection(_dbCon.Value.dbCon))
                                {
                                    rowAffected2 = con.Execute(cmd4);
                                }

                                if (rowAffected2 > 0)
                                {
                                    dapperQuery.SaveCompressedImage(
                                    item.applicationEDocPath,
                                    newProductImageID.ToString(),
                                    item.applicationEDoc,
                                    item.applicationEdocExtenstion);
                                }
                            }
                        }
                        

                        List<ShopProduct> appMenuShopProductID = new List<ShopProduct>();
                        cmd2 = "select \"shopProductID\" from tbl_shop_products ORDER BY \"shopProductID\" DESC LIMIT 1";
                        appMenuShopProductID = (List<ShopProduct>)dapperQuery.QryResult<ShopProduct>(cmd2, _dbCon);

                        if(appMenuShopProductID.Count == 0)
                        {
                            newshopProductID = 1;
                        }else{
                            newshopProductID = appMenuShopProductID[0].shopProductID+1;
                        }

                        if (obj.measurementUnitID > 0)
                        {
                            cmd5 = "insert into public.tbl_shop_products (\"shopProductID\",\"productID\",\"shopID\",\"measurementUnitID\",\"isDeleted\",\"NoOfItem\") Values (" + newshopProductID + ", " + newProductID + ", " + obj.shopID + "," + obj.measurementUnitID + ",0," + NoCount + ")";    
                        }
                        else
                        {
                            cmd5 = "insert into public.tbl_shop_products (\"shopProductID\",\"productID\",\"shopID\",\"isDeleted\",\"NoOfItem\") Values (" + newshopProductID + ", " + newProductID + ", " + obj.shopID + ",0, " + NoCount + ")";
                        }
                        
                        using (NpgsqlConnection con = new NpgsqlConnection(_dbCon.Value.dbCon))
                        {
                            rowAffected2 = con.Execute(cmd5);
                        }

                        if (rowAffected > 0 && rowAffected2 > 0)
                        {
                            response = "Success";
                            return Ok(new { message = response });
                        }
                        else
                        {
                            response = "Something went wrong";
                            return BadRequest(new { message = response });
                        }
                        
                    }
                    else
                    {
                        if (found == true)
                        {
                            response = "Product already exist";
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

        [HttpPost("updateProduct")]
        public IActionResult updateProduct(ProductCreation obj)
        {
            try
            {
                DateTime curDate = DateTime.Today;

                DateTime curTime = DateTime.Now;

                var time = curTime.ToString("HH:mm");
                var found = false;
                var name = "";

                List<Product> appMenuProduct = new List<Product>();
                cmd = "SELECT \"productName\" from tbl_products WHERE \"isDeleted\"=\'0\' and \"productName\"='"+obj.productName+"'";
                appMenuProduct = (List<Product>)dapperQuery.QryResult<Product>(cmd, _dbCon);

                if (appMenuProduct.Count > 0)
                    name = appMenuProduct[0].productName;

                int rowAffected = 0;
                int rowAffected2 = 0;
                var response = "";

                
                if(obj.productID != 0)
                {
                    cmd3 = "update public.tbl_products set \"productName\" = '" + obj.productName + "',\"productTypeID\" = " + obj.productTypeID + ",\"shopCategoryID\" = " + obj.shopCategoryID + ",\"rateOfPurchase\" = '" + obj.rateOfPurchase + "',\"rateOfSale\" = '" + obj.rateOfSale + "',\"discount\" = '" + obj.discount + "',\"productDescription\" = '" + obj.productDescription + "',\"qty\" = " + obj.qty + " where \"productID\" = " + obj.productID + ";";
                    cmd5 = "update public.tbl_shop_products set \"measurementUnitID\" = " + obj.measurementUnitID + " where \"productID\" = " + obj.productID + " and \"shopID\" = " + obj.shopID + " ";
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

                    using (NpgsqlConnection con = new NpgsqlConnection(_dbCon.Value.dbCon))
                    {
                        rowAffected2 = con.Execute(cmd5);
                    }

                    // if (obj.applicationEDocPath != null && obj.applicationEDocPath != "")
                    // {
                    //     dapperQuery.saveImageFile(
                    //         obj.applicationEDocPath,
                    //         obj.productID.ToString(),
                    //         obj.applicationEDoc,
                    //         obj.applicationEdocExtenstion);
                    // }
                }

                if (rowAffected > 0 && rowAffected2 > 0)
                {
                    
                    response = "Success";
                    return Ok(new { message = response });
                }
                else
                {
                    if (found == true)
                    {
                        response = "Product not found";
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

        [HttpPost("updateProductImages")]
        public IActionResult updateProductImages(ProductCreation obj)
        {
            try
            {
                DateTime curDate = DateTime.Today;

                DateTime curTime = DateTime.Now;

                var time = curTime.ToString("HH:mm");
                var found = false;
                // var name = "";

                int rowAffected = 0;
                int rowAffected2 = 0;
                var response = "";
                var newProductImageID = 0;

                
                if(obj.productID != 0)
                {
                    var invObject = JsonConvert.DeserializeObject<List<ProductImagesCreation>>(obj.json);

                    cmd3 = "delete from public.\"tbl_product_images\" where \"productID\" = " + obj.productID + ";";

                    using (NpgsqlConnection con = new NpgsqlConnection(_dbCon.Value.dbCon))
                    {
                        rowAffected2 = con.Execute(cmd3);
                    }

                    foreach (var item in invObject)
                    {
                        List<ProductImagesCreation> appMenuProductImagesID = new List<ProductImagesCreation>();
                        cmd6 = "select \"productImageID\" from public.\"tbl_product_images\" ORDER BY \"productImageID\" DESC LIMIT 1";
                        appMenuProductImagesID = (List<ProductImagesCreation>)dapperQuery.Qry<ProductImagesCreation>(cmd6, _dbCon);

                        if(appMenuProductImagesID.Count == 0)
                            {
                                newProductImageID = 1;
                            }
                            else
                            {
                                newProductImageID = appMenuProductImagesID[0].productImageID+1;
                            }

                        if (item.applicationEDocPath != null && item.applicationEDocPath != "")
                        {

                            cmd4 = "insert into public.\"tbl_product_images\" (\"productImageID\",\"productID\",\"productImagePath\",\"productImageExt\",\"createdOn\",\"createdBy\",\"isDeleted\") Values (" + newProductImageID + "," + obj.productID + ",'" + item.applicationEDocPath + "','" + item.applicationEdocExtenstion + "','" + curDate + "'," + obj.userID + ",B'0');";

                            using (NpgsqlConnection con = new NpgsqlConnection(_dbCon.Value.dbCon))
                            {
                                rowAffected = con.Execute(cmd4);
                            }
                            if (rowAffected > 0)
                            {
                                dapperQuery.SaveCompressedImage(
                                item.applicationEDocPath,
                                newProductImageID.ToString(),
                                item.applicationEDoc,
                                item.applicationEdocExtenstion);
                            }
                        }
                    }
                }
                else
                {
                    found = true;
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
                        response = "Product not found";
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

        [HttpPost("deleteProduct")]
        public IActionResult deleteProduct(ProductCreation obj)
        {
            try
            {
                DateTime curDate = DateTime.Today;

                DateTime curTime = DateTime.Now;

                var time = curTime.ToString("HH:mm");
                var found = false;
                var name = "";

                List<Product> appMenuProduct = new List<Product>();
                cmd = "SELECT \"productName\" from tbl_products WHERE \"isDeleted\"=\'0\' and \"productName\"='"+obj.productName+"'";
                appMenuProduct = (List<Product>)dapperQuery.QryResult<Product>(cmd, _dbCon);

                if (appMenuProduct.Count > 0)
                    name = appMenuProduct[0].productName;

                int rowAffected = 0;
                int rowAffected2 = 0;
                var response = "";

                
                if(obj.productID != 0)
                {
                    cmd3 = "update public.tbl_products set \"isDeleted\" = 1 where \"productID\" = " + obj.productID + ";";
                    cmd5 = "update public.tbl_shop_products set \"isDeleted\" = 1 where \"productID\" = " + obj.productID + " and \"shopID\" = " + obj.shopID + " ";
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

                    using (NpgsqlConnection con = new NpgsqlConnection(_dbCon.Value.dbCon))
                    {
                        rowAffected2 = con.Execute(cmd5);
                    }

                }

                if (rowAffected > 0 && rowAffected2 > 0)
                {
                    response = "Success";
                    return Ok(new { message = response });
                }
                else
                {
                    if (found == true)
                    {
                        response = "Product not found";
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