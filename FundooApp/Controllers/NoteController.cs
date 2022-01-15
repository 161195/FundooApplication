using BuisnessLayer.Interfaces;
using BuisnessLayer.Services;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Repository.Context;
using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FundooApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NoteController : ControllerBase
    {
        private readonly IMemoryCache memoryCache;
        private readonly UserContext context;
        private readonly IDistributedCache distributedCache;
        
        INoteBL BL;
        public NoteController(INoteBL BL, IMemoryCache memoryCache, UserContext context, IDistributedCache distributedCache)
        {
            this.BL = BL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this.context = context;
        }
        /// <summary>
        /// Notes the registrations.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        [HttpPost]                                      
        public IActionResult NoteRegistrations(NoteRegistration user)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                if (this.BL.Registration(user,UserId))
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
                return this.BadRequest(new { success = false, message = ex.Message, InnerException=ex.InnerException });
            }
        }
        /// <summary>
        /// Gets all note details.
        /// </summary>
        /// <returns></returns>
        [HttpGet]              
        public IActionResult GetAllNoteDetails()
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var noteDetailsList = this.BL.GetNoteRegistrations(UserId);
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

        /// <summary>
        /// Gets the with identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]  
        public IActionResult GetNoteDetailsById(long id)
        {
            try
            {
                Note note = this.BL.GetWithId(id);
                if (note == null)
                {
                    return BadRequest(new { Success = false, message = "No Notes With Particular Id " });
                }
                return Ok(new { Success = true, message = "Notes Available with Entered Id ", note });
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates the notes.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="note">The note.</param>
        /// <returns></returns>
        [HttpPut("{id}")]   
        public IActionResult UpdateNotes(long id, Note note)
        {
            try
            {
                Note updateNotes = BL.GetWithId(id);
                if (updateNotes == null)
                {
                    return BadRequest(new { Success = false, message = "No Notes are there with this Id" });
                }
                BL.UpdateNotes(updateNotes, note);
                return Ok(new { Success = true, message = "Update Sucessful",data=updateNotes});
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes the notes.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}/Delete")]
        public IActionResult DeleteNotes(long id)
        {
            try
            {
                Note note = this.BL.GetWithId(id);
                if (note == null)
                {
                    return BadRequest(new { Success = false, message = "Entered NoteId not found" });
                }
                BL.DeleteNotes(note);
                return Ok(new { Success = true, message = "Deleted notes from DataBase" });
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Pins the note.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}/Pin")]
        public IActionResult PinNote(int id)
        {
            try
            {
                var result = this.BL.PinNote(id);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = result, Data = result });
                }

                return this.BadRequest(new { Status = false, Message = result });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Archives the note.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}/Archive")]
        public IActionResult ArchiveNote(int id)
        {
            try
            {
                var result = this.BL.ArchiveNote(id);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = result, Data = result });
                }

                return this.BadRequest(new { Status = false, Message = result });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Trashes the or restore note.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}/Trash")]
        public IActionResult TrashOrRestoreNote(int id)
        {
            try
            {
                var result = this.BL.TrashOrRestoreNote(id);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = result});
                }

                return this.BadRequest(new { Status = false, Message = result });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Changes the color.
        /// </summary>
        /// <param name="NoteId">The note identifier.</param>
        /// <param name="color">The color.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{NoteId}/{color}")]
        public IActionResult ChangeColor(long NoteId, string color)
        {
            try
            {
                var message = this.BL.AddColor(NoteId, color);
                if (message.Equals("New Color has set to this note !"))
                {
                    return this.Ok(new { Status = true, Message = message, Data = color});
                }

                return this.BadRequest(new { Status = true, Message = message });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        /// <summary>
        /// Uploads the image.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="image">The image.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("uploadImage")]
        public IActionResult UploadImage(long noteId, IFormFile image)
        {
            try
            {
                bool result = this.BL.UploadImage(noteId, image);
                if (result.Equals(true))
                {
                    return this.Ok(new { Status = true, Message = "Upload Image Successfully", Data = noteId });
                }

                return this.BadRequest(new { Status = false, Message = result });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, Message = ex.Message, InnerException = ex.InnerException });
            }
        }
        /// <summary>
        /// Gets all notes using redis cache.
        /// </summary>
        /// <returns></returns>
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllNotesUsingRedisCache()
        {
            long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
            var cacheKey = "noteDetailsList";
            string serializedNotesList;
            IEnumerable<Note> noteDetailsList = new List<Note>();
            var redisNotesList = await distributedCache.GetAsync(cacheKey);
            if (redisNotesList != null)
            {
                serializedNotesList = Encoding.UTF8.GetString(redisNotesList);
                noteDetailsList = JsonConvert.DeserializeObject<List<Note>>(serializedNotesList);
            }
            else
            {               
                noteDetailsList = (IEnumerable<Note>)BL.GetNoteRegistrations(UserId);
                serializedNotesList = JsonConvert.SerializeObject(noteDetailsList);
                redisNotesList = Encoding.UTF8.GetBytes(serializedNotesList);
                var options = new DistributedCacheEntryOptions()
                 .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                 .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisNotesList, options);
            }
            return Ok(noteDetailsList);
        }
    }
}
        


