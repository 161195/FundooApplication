using BuisnessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FundooApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollaboratorController : ControllerBase
    {
        ICollaboratorBL BL;
        public CollaboratorController(ICollaboratorBL BL)
        {
            this.BL = BL;
        }
        [Authorize]
        [HttpPost]                               
        public IActionResult CollabRegister(CollaboratorModel user)
        {
            try
            {
                var UserId= Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                if (this.BL.CollabAdd(user,UserId))
                {
                    return this.Ok(new { Success = true, message = "Note Collaboration Successfully" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Note Collaboration Unsuccessfully" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.InnerException });
            }
        }
        [Authorize]
        [HttpPut]
        [Route("RemoveUSer")]
        public IActionResult RemoveCollaborateWithUser(CollaboratorModel collaborate)
        {
            try
            {
                long UserId = Convert.ToInt32(User.FindFirst("UserId").Value);
                string response = this.BL.RemoveCollaborate(collaborate, UserId);
                if (response != null)
                    return this.Ok(new { Message = response });
                else
                    return this.BadRequest(new { Status = false, Message = response });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}
