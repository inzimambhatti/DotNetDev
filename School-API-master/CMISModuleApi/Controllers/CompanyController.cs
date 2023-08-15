using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CMISModuleApi.Services;
using Microsoft.Extensions.Options;
using CMISModuleApi.Configuration;
using CMISModuleApi.Entities;
using Dapper;
using CMISModuleApi.dto.request;
using System.Data;
using Newtonsoft.Json;

namespace CMISModuleApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly IOptions<conStr> _dbCon;
        private string cmd;

        public CompanyController(IOptions<conStr> dbCon)
        {
            _dbCon = dbCon;
        }

        [HttpGet("getCompanyList")]
        public IActionResult getCompanyList()
        {
            try
            {
                cmd = "SELECT * FROM view_companyList";
                var appMenu = dapperQuery.Qry<company>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getCompanyDetail")]
        public IActionResult getCompanyDetail(int company_id)
        {
            try
            {
                cmd = "SELECT * FROM view_companyDetail where company_id="+company_id+"";
                var appMenu = dapperQuery.Qry<companyDetail>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

        [HttpGet("getCompanyType")]
        public IActionResult getCompanyType()
        {
            try
            {
                cmd = "SELECT * FROM tbl_company_type where isDeleted=0";
                var appMenu = dapperQuery.Qry<companyType>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

        [HttpGet("getCountry")]
        public IActionResult getCountry()
        {
            try
            {
                cmd = "SELECT * FROM tbl_country where isDeleted = 0";
                var appMenu = dapperQuery.Qry<Country>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

        [HttpGet("getCity")]
        public IActionResult getCity(int countryID)
        {
            try
            {
                cmd = "SELECT * FROM tbl_city where isDeleted = 0 and countory_id = " + countryID + ";";
                var appMenu = dapperQuery.Qry<City>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

        [HttpPost("saveCompany")]
        public IActionResult saveCompany(companyCreation model)
        {
            try
            {

                var response = dapperQuery.SPReturn("sp_companyCrud", model, _dbCon);
                var data = response.Select(row => new { res = row.ToString() });
                bool result = data.First().res.Contains("Success");

                if (result == true && (model.company_picture_path != null && model.company_picture_path != "" && model.company_picture_path != "null"))
                {

                    var companyId = data.First().res.Split("|||")[1];

                    dapperQuery.saveImageFile(
                        model.company_picture_path,
                        companyId,
                        model.company_picture,
                        model.company_picture_extension);
                }

                return Ok(response);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }
    }
}