using BuisnessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Repository.Context;
using Repository.Entity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FundooApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IMemoryCache memoryCache;
        private readonly UserContext context;
        private readonly IDistributedCache distributedCache;
        IUserBL BL;
        public UserController(IUserBL BL, IMemoryCache memoryCache, UserContext context, IDistributedCache distributedCache)
        {
            this.BL = BL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this.context = context;

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
        /// <summary>
        /// Get all registered details
        /// </summary>
        /// <returns></returns>
        //[AllowAnonymous]
        [Authorize]
        [HttpGet("GetDetails")]             
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
        /// <summary>
        /// Gets the login.
        /// </summary>
        /// <param name="user1">The user1.</param>
        /// <returns></returns>
        [HttpPost("Login")]                             
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
        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("forgetPassword")]
        public IActionResult ForgetPassword(ForgetPasswordModel model)
        {
            try
            {               
                if (this.BL.ForgetPassword(model))
                {
                    return Ok(new { Success = true, message = "Password Reset Mail Sent" });
                }
                return BadRequest(new { Success = false, message = "Invalid Credentials for reset password" });
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="reset">The reset.</param>
        /// <returns></returns>
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
        /// <summary>
        /// Gets all users using redis cache.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("redis")]
        public async Task<IActionResult> GetUserRegistrationsRedisCache()
        {
            var cacheKey = "UserDetailsList";
            string serializedNotesList;
            IEnumerable<User> UserDetailsList = new List<User>();        
            var redisUserList = await distributedCache.GetAsync(cacheKey);
            if (redisUserList != null)
            {
                serializedNotesList = Encoding.UTF8.GetString(redisUserList);
                UserDetailsList = JsonConvert.DeserializeObject<List<User>>(serializedNotesList);
            }
            else
            {
                UserDetailsList = (IEnumerable<User>)BL.GetUserRegistrations();
                serializedNotesList = JsonConvert.SerializeObject(UserDetailsList);
                redisUserList = Encoding.UTF8.GetBytes(serializedNotesList);
                var options = new DistributedCacheEntryOptions()
                 .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                 .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisUserList, options);
            }
            return Ok(UserDetailsList);                    
        }
    }
}
