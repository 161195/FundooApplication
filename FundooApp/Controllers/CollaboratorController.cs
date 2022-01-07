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
                if (this.BL.CollabAdd(user))
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
    }
}
