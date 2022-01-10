using BuisnessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LableController : ControllerBase
    {
         ILableBL BL;
        public LableController(ILableBL BL)
        {
            this.BL = BL;
        }
       
        [Authorize]
        [HttpPost]
        public IActionResult LableRegister(LabelModel user)
        {
            try
            {
                var UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                if (this.BL.LableAdd(user, UserId))
                {
                    return this.Ok(new { Success = true, message = "lable added to note Successfully" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "lable added Unsuccessfully" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.InnerException });
            }
        }
    }
}
