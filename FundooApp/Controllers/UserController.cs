using BuisnessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository.Entity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace FundooApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  
    public class UserController : ControllerBase
    {
        //private readonly IConfiguration _configuration;
        //public UserController(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}

        IUserBL BL;
        public UserController(IUserBL BL)
        {
            this.BL = BL;
        }
        [HttpPost]                                      //to add new registration
        public IActionResult UserRegistration(UserRegistration user)
        {
            try
            {
                if (this.BL.Registration(user))
                {
                    return this.Ok(new { Success = true, message = "Registration Successful" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Registration Unsuccessful" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.InnerException });
            }
        }
        [HttpGet("GetAllUserDetails")]              //get all registered data
        public IActionResult GetAllUserDetails()
        {
            try
            {
                var userDetailsList= this.BL.GetUserRegistrations();
                if (userDetailsList != null)
                {
                    return this.Ok(new { Success = true, userlist=userDetailsList });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message ="No user records found"});
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.InnerException });
            }

        }     
        [HttpPost("Login")]                             //post login Details
        public IActionResult GetLogin(UserLogin user1)
        {          
            try
            {
                LoginResponse result = this.BL.GetLogin(user1);
                if (result.EmailId == null)
                {
                    return BadRequest(new { Success = false, message = "Email or Password Not Found" });
                }            
                return Ok(new { Success = true, message = "Login Successful", UserLoginInfo = result});
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Success = false, message = e.Message });
            }
        }
        [Authorize]
        [HttpDelete("Delete")]                                      //to delete existing registration
        public IActionResult UserDelete(deleteOperation user)
        {
            try
            {
                if (this.BL.UserDelete(user))
                {
                    return this.Ok(new { Success = true, message = "Deleted" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "No Such Registration Found" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.InnerException });
            }
        }
    }
}
