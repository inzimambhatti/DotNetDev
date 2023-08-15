using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UMISModuleAPI.Models;
using UMISModuleApi.dto.request;
using UMISModuleAPI.Services;
using UMISModuleAPI.Entities;
using UMISModuleApi.Entities;
using Microsoft.Extensions.Options;
using UMISModuleAPI.Configuration;
using UMISModuleApi.dto.response;
using MimeKit;
using MailKit;
using System.Net.Mail;
using Zaabee.SmtpClient;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Web;
using System.Linq;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Npgsql;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Dapper;
using System.Data;
using System.Collections.Generic;

namespace UMISModuleAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private string cmd,cmd2,cmd3;
        private string randomNumber;
        private readonly IOptions<conStr> _dbCon;
        
        public UserController(IUserService userService, IOptions<conStr> dbCon)
        {   
            _userService = userService;
            _dbCon = dbCon;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);
            if (response == null)
                return BadRequest(new { message = "user name or password is incorrect" });

            return Ok(response);
        }

        [HttpPost("GoogleAuthenticate")]
        public IActionResult GoogleAuthenticate(AuthenticateRequest model)
        {
            var response = _userService.GoogleAuthenticate(model);
            if (response == null)
                return BadRequest(new { message = "user name or password is incorrect" });

            return Ok(response);
        }

        [HttpGet("getPassword")]
        public IActionResult getPassword(int userID)
        {
            try
            {
                cmd = "SELECT \"userPassword\" from tbl_users where \"userID\" = " + userID + " ";
                var appMenu = dapperQuery.Qry<Password>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getAppStatus")]
        public IActionResult getAppStatus()
        {
            try
            {
                cmd = "SELECT * from tbl_status";
                var appMenu = dapperQuery.Qry<Status>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getVerifyOTP")]
        public IActionResult getVerifyOTP(string OTP)
        {
            try
            {

                cmd = "SELECT \"otpNo\" from \"OTP\" where \"otpNo\" = '" + OTP + "' ";
                var appMenu = dapperQuery.Qry<OTP>(cmd, _dbCon);

                return Ok(appMenu);    
                
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("getOTP")]
        public IActionResult getOTP(SendEmail obj)
        {
            Random rnd = new Random();
            randomNumber = (rnd.Next(1000,9999)).ToString();
            
            int rowAffected = 0;
            var response = "";
            var newOTPid = 0;

            List<OTP> appMenuUserID = new List<OTP>();
            cmd3 = "select \"otpID\" from public.\"OTP\" ORDER BY \"otpID\" DESC LIMIT 1";
            appMenuUserID = (List<OTP>)dapperQuery.QryResult<OTP>(cmd3, _dbCon);

            if(appMenuUserID.Count == 0)
                {
                    newOTPid = 1;    
                }else{
                    newOTPid = appMenuUserID[0].otpID+1;
                }

            cmd2 = "insert into public.\"OTP\" (\"otpID\",\"otpNo\",\"timestamp\") values (" + newOTPid + ",'" + randomNumber + "',current_timestamp)";

                using (NpgsqlConnection con = new NpgsqlConnection(_dbCon.Value.dbCon))
                {
                    rowAffected = con.Execute(cmd2);
                }

                if (rowAffected > 0)
                {   
                    
                    using (MailMessage mail = new MailMessage())
                        {
                            mail.From = new MailAddress("noreply@mysite.com");
                            mail.To.Add(obj.userEmail);
                            mail.Subject = "Your one time password for verification";
                            mail.Body = "your one time password is "+randomNumber+".";
                            mail.IsBodyHtml = true;

                            //* for setting smtp mail name and port
                            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                            {
                                smtp.UseDefaultCredentials = false;
                                //* for setting sender credentials(email and password) using smtp
                                smtp.Credentials = new System.Net.NetworkCredential("merishoppakistan@gmail.com","imcwhojokkrbkmjt");
                                smtp.EnableSsl = true;
                                
                                smtp.Send(mail);
                            }
                        }
                        
                    response = "Mail Sent!";
                    return Ok(new { message = response });

                }
                else
                {
                    
                    response = "Something went wrong";
                    return BadRequest(new { message = response });

                }

                
        }

        [HttpPost("sendMailFromSite")]
        public IActionResult sendMailFromSite(SendMail obj)
        {
            var response = "";

            using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("noreply@mysite.com");
                    mail.To.Add(obj.ourEmail);
                    mail.Subject = "" + obj.subject + "";
                    mail.Body = "Hi, This is " + obj.userName + ",<br><br> Email : " + obj.userEmail + ",<br><br>" + obj.message + ".";
                    mail.IsBodyHtml = true;

                    //* for setting smtp mail name and port
                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.UseDefaultCredentials = false;
                        //* for setting sender credentials(email and password) using smtp
                        smtp.Credentials = new System.Net.NetworkCredential("hammadhere12@gmail.com","bicmzfivilxrhsla");
                        smtp.EnableSsl = true;
                        
                        smtp.Send(mail);
                    }
                }
                
            response = "Mail Sent!";
            return Ok(new { message = response });

        }

        [HttpGet("Login")]
        [Authorize]
        public async Task Login()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,new AuthenticationProperties()
            {
                RedirectUri = Url.Action("GoogleResponse")
            });
        }

        [HttpGet("GoogleResponse")]
        [Authorize]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var claims = result.Principal.Identities
                .FirstOrDefault().Claims.Select(claim => new
                {
                    claim.Issuer,
                    claim.OriginalIssuer,
                    claim.Type,
                    claim.Value
                });
            return Ok(claims);
        } 

        [HttpGet("Logout")]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        } 

        // [HttpGet("externallogincallback", Name = "externallogincallback")]
        // [AllowAnonymous]
        // public Task<IActionResult> externallogincallback(string returnUrl = null, string remoteError = null)
        // {
        //     //Here we can retrieve the claims
        //     var result =  HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        //     return null;
        // }

        // [HttpPost("send")]
        // public async Task<IActionResult> SendMail([FromForm]MailRequest request)
        // {
        //     try
        //     {
        //         await mailService.SendEmailAsync(request);
        //         return Ok();
        //     }
        //     catch (Exception ex)
        //     {
        //         throw;
        //     }
                
        // }
        
        [HttpGet("getUserDetail")]
        public IActionResult getUserDetail(int userID)
        {
            try
            {
                cmd = "SELECT * from \"view_userDetail\" where \"userID\" = " + userID + "";
                var appMenu = dapperQuery.Qry<userDetail>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }

        [HttpGet("getAllUser")]
        public IActionResult getAllUser()
        {
            try
            {
                cmd = "SELECT * from tbl_users";
                var appMenu = dapperQuery.Qry<userDetail>(cmd, _dbCon);
                return Ok(appMenu);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("saveUser")]
        public IActionResult saveUser(userCreation obj)
        {
            try{
                DateTime curDate = DateTime.Today;

                DateTime curTime = DateTime.Now;
                
                var time = curTime.ToString("HH:mm");

                int rowAffected = 0;
                var response = "";
                var found = false;
                var userName = "";
                var newUserID = 0;
                List<User> appMenuProduct = new List<User>();

                List<User> appMenuUserID = new List<User>();
                cmd3 = "select \"userID\" from public.\"tbl_users\" ORDER BY \"userID\" DESC LIMIT 1";
                appMenuUserID = (List<User>)dapperQuery.QryResult<User>(cmd3, _dbCon);

                if(appMenuUserID.Count == 0)
                    {
                        newUserID = 1;    
                    }else{
                        newUserID = appMenuUserID[0].userID+1;
                    }

                List<userCreation> appMenuUser = new List<userCreation>();
                cmd = "select \"userEmail\" from tbl_users where \"userEmail\"='" + obj.userEmail + "' and \"isDeleted\" = 0";
                appMenuUser = (List<userCreation>)dapperQuery.QryResult<userCreation>(cmd, _dbCon);

                // List<userCreation> appMenuUserID = new List<userCreation>();
                // cmd3 = "select \"userID\" from users ORDER BY \"userID\" DESC LIMIT 1";
                // appMenuUserID = (List<userCreation>)dapperQuery.QryResult<userCreation>(cmd3, _dbCon);
                // newUserID = appMenuUserID[0].userID+1;


                if (appMenuUser.Count > 0)
                        userName = appMenuUser[0].userEmail;

                if(userName=="")
                {
                    cmd2 = "insert into public.\"tbl_users\" (\"userID\",\"userFullName\", \"userEmail\", \"userMobile\",\"userPassword\", \"userMode\", \"genderID\", \"currencyID\",\"isDeleted\") values (" + newUserID + ",'" + obj.userFullName + "','" + obj.userEmail + "','" + obj.userMobile + "', '" + obj.userPassword + "','" + obj.userMode + "','" + obj.genderID + "','" + obj.currencyID + "',0)";
                   
                }
                else
                {
                    found=true;
                }
                if (found == false)
                {
                    using (NpgsqlConnection con = new NpgsqlConnection(_dbCon.Value.dbCon))
                    {
                        rowAffected = con.Execute(cmd2);
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
                        response = "Email already exist";
                    }
                    else
                    {
                        response = "Server Issue";
                    }
                return BadRequest(new { message = response });

                }

            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

        [HttpPost("saveGoogleSignIn")]
        public IActionResult saveGoogleSignIn(GoogleResponse obj)
        {
            try{
                DateTime curDate = DateTime.Today;

                DateTime curTime = DateTime.Now;
                
                var time = curTime.ToString("HH:mm");

                int rowAffected = 0;
                var response = "";
                var found = false;
                var userName = "";
                var newUserID = 0;
                List<User> appMenuProduct = new List<User>();

                List<User> appMenuUserID = new List<User>();
                cmd3 = "select \"userID\" from public.\"tbl_users\" ORDER BY \"userID\" DESC LIMIT 1";
                appMenuUserID = (List<User>)dapperQuery.QryResult<User>(cmd3, _dbCon);

                if(appMenuUserID.Count == 0)
                    {
                        newUserID = 1;    
                    }else{
                        newUserID = appMenuUserID[0].userID+1;
                    }

                List<userCreation> appMenuUser = new List<userCreation>();
                cmd = "select \"userEmail\" from tbl_users where \"userEmail\"='" + obj.email + "' and \"isDeleted\" = 0";
                appMenuUser = (List<userCreation>)dapperQuery.QryResult<userCreation>(cmd, _dbCon);

                if (appMenuUser.Count > 0)
                        userName = appMenuUser[0].userEmail;

                if(userName=="")
                {
                    cmd2 = "insert into public.\"tbl_users\" (\"userID\",\"userFullName\", \"userEmail\",\"isDeleted\") values (" + newUserID + ",'" + obj.userName + "','" + obj.email + "',0)";
                   
                }
                else
                {
                    found=true;
                }
                if (found == false)
                {
                    using (NpgsqlConnection con = new NpgsqlConnection(_dbCon.Value.dbCon))
                    {
                        rowAffected = con.Execute(cmd2);
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
                        response = "Email already exist";
                    }
                    else
                    {
                        response = "Server Issue";
                    }
                return BadRequest(new { message = response });

                }

            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

        [HttpPost("updateUser")]
        public IActionResult updateUser(userCreation obj)
        {
            try{
                DateTime curDate = DateTime.Today;

                DateTime curTime = DateTime.Now;
                
                var time = curTime.ToString("HH:mm");

                int rowAffected = 0;
                var response = "";

                cmd = "update public.\"tbl_users\" set \"userFullName\" = '" + obj.userFullName + "', \"userEmail\" = '" + obj.userEmail +"' ,\"userMobile\" = '"+ obj.userMobile +"', \"genderID\" = '" + obj.genderID + "', \"currencyID\" = '" + obj.currencyID + "' where \"userID\"="+obj.userID+"";
                
                using (NpgsqlConnection con = new NpgsqlConnection(_dbCon.Value.dbCon))
                {
                    rowAffected = con.Execute(cmd);
                }

                if (rowAffected > 0)
                {
                    response = "Success";
                    return Ok(new { message = response });
                }
                else
                {

                   response = "something went wrong";
                    
                    return BadRequest(new { message = response });

                }

            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

        [HttpPost("deleteUser")]
        public IActionResult deleteUser(userCreation obj)
        {
            try{
                DateTime curDate = DateTime.Today;

                DateTime curTime = DateTime.Now;
                
                var time = curTime.ToString("HH:mm");

                int rowAffected = 0;
                int rowAffected2 = 0;
                var response = "";

                cmd = "update public.\"tbl_users\" set \"isDeleted\" = 1 where \"userID\" = " + obj.userID + "";
                cmd2 = "update public.\"tbl_shops\" set \"isDeleted\" = 1 where \"userID\" = " + obj.userID + "";

                using (NpgsqlConnection con = new NpgsqlConnection(_dbCon.Value.dbCon))
                {
                    rowAffected = con.Execute(cmd);
                }

                using (NpgsqlConnection con = new NpgsqlConnection(_dbCon.Value.dbCon))
                {
                    rowAffected2 = con.Execute(cmd2);
                }

                if (rowAffected > 0)
                {
                    response = "Success";
                    return Ok(new { message = response });
                }
                else
                {

                   response = "User not found";
                    
                    return BadRequest(new { message = response });

                }

            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

        [HttpPost("updateProfile")]
        public IActionResult updateProfile(userCreation obj)
        {
            try{
                DateTime curDate = DateTime.Today;

                DateTime curTime = DateTime.Now;
                
                var time = curTime.ToString("HH:mm");

                int rowAffected = 0;
                var response = "";
                var newUserID = 0;

                cmd2 = "UPDATE public.\"tbl_users\" SET \"userImageDoc\"='" + obj.userImageDoc + "',\"userEDocExtension\"='" + obj.applicationEdocExtenstion + "' where \"userID\"=" + obj.userID + "";
            
                using (NpgsqlConnection con = new NpgsqlConnection(_dbCon.Value.dbCon))
                {
                    rowAffected = con.Execute(cmd2);
                }

                if (rowAffected > 0)
                {
                    newUserID = obj.userID;

                    if (obj.applicationEDocPath != null && obj.applicationEDocPath != "")
                    {
                        dapperQuery.saveImageFile(
                            obj.userImageDoc,
                            newUserID.ToString(),
                            obj.applicationEDocPath,
                            obj.applicationEdocExtenstion);
                    }

                    response = "Success";
                    return Ok(new { message = response });

                }
                else
                {
                    
                    response = "Try again";
                    
                return BadRequest(new { message = response });

                }

            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

        [HttpPost("updatePassword")]
        public IActionResult updatePassword(changePassword obj)
        {
            try{
                DateTime curDate = DateTime.Today;

                DateTime curTime = DateTime.Now;
                
                var time = curTime.ToString("HH:mm");

                int rowAffected = 0;
                var response = "";
                

                cmd2 = "UPDATE public.\"tbl_users\" SET \"userPassword\"='" + obj.newPassword + "' where \"userID\"=" + obj.userID + "";
            
                using (NpgsqlConnection con = new NpgsqlConnection(_dbCon.Value.dbCon))
                {
                    rowAffected = con.Execute(cmd2);
                }

                if (rowAffected > 0)
                {
                    response = "Success";
                return Ok(new { message = response });

                }
                else
                {
                    
                    response = "Try again";
                    
                return BadRequest(new { message = response });

                }

            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

        [HttpPost("forgetPassword")]
        public IActionResult forgetPassword(userCreation obj)
        {
            try{
                DateTime curDate = DateTime.Today;

                DateTime curTime = DateTime.Now;
                
                var time = curTime.ToString("HH:mm");

                int rowAffected = 0;
                var response = "";
                
                cmd2 = "UPDATE public.\"tbl_users\" SET \"userPassword\"='"+obj.userPassword+"' where \"userEmail\"='"+obj.userEmail+"'";
            
                using (NpgsqlConnection con = new NpgsqlConnection(_dbCon.Value.dbCon))
                {
                    rowAffected = con.Execute(cmd2);
                }

                if (rowAffected > 0)
                {
                    response = "Success";
                return Ok(new { message = response });

                }
                else
                {
                    
                    response = "Enter correct email";
                    
                return BadRequest(new { message = response });

                }

            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }

    }
}