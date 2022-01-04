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
        [AllowAnonymous]
        //[Authorize]
        [HttpGet("GetAllUserDetails")]              //get all registered data
        public IActionResult GetAllUserDetails()
        {
            try
            {
                var userDetailsList = this.BL.GetUserRegistrations();
                if (userDetailsList != null)
                {
                    return this.Ok(new { Success = true, userlist = userDetailsList });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "No user records found" });
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
                return Ok(new { Success = true, message = "Login Successful", UserLoginInfo = result });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Success = false, message = e.Message });
            }
        }
        [HttpPost]
        [Route("forgetPassword")]
        public IActionResult ForgetPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email should not be null or empty");
            }

            try
            {
                if (this.BL.ForgetPassword(email))
                {
                    return Ok(new { Success = true, message = "Reset password link send on Email Successfully" });
                }
                else
                {
                    return Ok(new { Success = true, message = "Error in send Reset password link" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Success = false, message = e.Message });
            }
        }
        [Authorize]
        [HttpPut]
        [Route("ResetPassword/")]
        public IActionResult ResetPassword(ChangePassword reset)
        {
            if(reset.NewPassWord==reset.ConfirmPassword)
            {
                var email = User.FindFirst(ClaimTypes.Email).Value.ToString(); 
                if (this.BL.ResetPassword(reset, email))
                {
                    return Ok(new { Success = true, message = "password Reset Successfully" });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Password Reset Unsuccesfully!" });
                }
            }
            else
            {
                return BadRequest(new { Success = false, message = "NewPassword does not matches with ConfirmPassword" });
            }          
        }
    }
}
