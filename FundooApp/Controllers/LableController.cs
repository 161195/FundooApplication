using BuisnessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Entity;
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
        /// <summary>
        /// Add the Lable
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>

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
        /// <summary>
        ///Update or add Notes to Previous lables
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        public IActionResult LableUpdate(LabelModel user)
        {
            try
            {
                var UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                if (this.BL.UpdateLable(user, UserId))
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
        /// <summary>
        /// DeleteLable
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("LableName")]
        public IActionResult DeletedLable1(LabelModel user)
        {
            try
            {               
                if (this.BL.DeleteLable(user))
                {
                    return this.Ok(new { Success = true, message = "lable removed Successfully" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "lable removed Unsuccessfully" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.InnerException });
            }
        }
        /// <summary>
        /// Remove Note from lable
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("RemoveNote")]
        public IActionResult RemoveNoteFromLable(LabelModel user)
        {
            try
            {
                if (this.BL.RemoveNote(user))
                {
                    return this.Ok(new { Success = true, message = "Note removed Successfully from lable" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Note removed UnSuccessfully from lable"});
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.InnerException });
            }
        }
        /// <summary>
        /// Gets the note details by lableName.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public IActionResult GetnotesDetailsByLableName([FromQuery] LabelModel user)
        {
            try
            {
                var lable = this.BL.GetLableRegistrations(user);
                if(lable != null)
                {
                    return this.Ok(new { Success = true, message = "Note fetch Successfully from lable",data=lable});
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Note fetch UnSuccessfully from lable" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.InnerException });
            }
        }
    }
}
