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
    public class NoteController : ControllerBase
    {
        INoteBL BL;
        public NoteController(INoteBL BL)
        {
            this.BL = BL;
        }
        //[Authorize]
        [HttpPost]                                      //to add new note registration
        public IActionResult UserRegistration(NoteRegistration user)
        {
            try
            {
                if (this.BL.Registration(user))
                {
                    return this.Ok(new { Success = true, message = "Note added Successfully" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Note Added Unsuccessful" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.InnerException });
            }
        }
        [HttpGet("GetAllNoteDetails")]              //get all note registered data
        public IActionResult GetAllUserDetails()
        {
            try
            {
                var noteDetailsList = this.BL.GetNoteRegistrations();
                if (noteDetailsList != null)
                {
                    return this.Ok(new { Success = true, userlist = noteDetailsList });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "No records found" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.InnerException });
            }

        }
    }   
}
