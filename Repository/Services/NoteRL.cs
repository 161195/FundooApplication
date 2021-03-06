using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository.Context;
using Repository.Entity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
    public class NoteRL : INoteRL      
    {      
        IConfiguration _config;
        readonly UserContext context;
        public NoteRL(UserContext context, IConfiguration config)
        {
            this.context = context;
            _config = config;
            
        }
        /// <summary>
        /// New Note Registrations for the specified userID.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public NoteRegistration Registration(NotesModel model,long UserId)
        {
            try
            {
                Note note = new()
                {
                    NoteId = model.NoteId,
                    Title = model.Title,
                    Message = model.Message,
                    CreatedAt = model.CreatedAt,
                    UserId =UserId
                 
                };
                this.context.Add(note);
                this.context.SaveChanges();

                NoteRegistration newNote = new NoteRegistration();
                newNote.UserId = UserId;
                newNote.Title = note.Title;
                newNote.Message = note.Message;
                newNote.Reminder = note.Reminder;
                newNote.Color = note.Color;
                newNote.Image = note.Image;
                newNote.IsArchive = note.IsArchive;
                newNote.IsPin = note.IsPin;
                newNote.IsTrash = note.IsTrash;
                newNote.CreatedAt = DateTime.Now;
                newNote.ModifiedAt = DateTime.Now;
                //adding Note data to the database Notetable 
                return newNote;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// Gets the note registrations.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Note> GetNoteRegistrations(long UserId)
        {
             return this.context.NoteTable.Where(i => i.UserId == UserId);         
        }
        /// <summary>
        /// Gets the note info with ID.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Note GetWithId(long id)
        {
            try
            {
                return this.context.NoteTable.FirstOrDefault(i => i.NoteId == id);
            }
            catch (Exception)
            {
                throw;
            }
        }
      
        /// <summary>
        /// Updates the notes.
        /// </summary>
        /// <param name="APerson">a person.</param>
        /// <param name="person">The person.</param>
        public void UpdateNotes(Note BeforeNote, Note AfterNote)
        {
            try
            {
                BeforeNote.Title = AfterNote.Title;
                BeforeNote.Message = AfterNote.Message;
                BeforeNote.Color = AfterNote.Color;
                BeforeNote.Image = AfterNote.Image;
                BeforeNote.IsArchive = AfterNote.IsArchive;
                BeforeNote.IsPin = AfterNote.IsPin;
                BeforeNote.IsTrash = AfterNote.IsTrash;
                this.context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void DeleteNotes(Note user1)
        {
            try
            {
                this.context.NoteTable.Remove(user1);
                this.context.SaveChanges();
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
        /// <exception cref="System.Exception"></exception>
        public string PinNote(int id)
        {
            try
            {                  
                var note = this.context.NoteTable.FirstOrDefault(x => x.NoteId == id);
                if (note.IsPin == false)
                {                                     
                    note.IsPin = true;
                    this.context.SaveChanges();                   
                    return "Note Pinned";
                }
                else
                {
                    note.IsPin = false;
                    this.context.SaveChanges();
                    return "Note UnPinned";
                }                   
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }               
        /// <summary>
        /// Method to Archive or unarchive the note
        /// </summary>
        /// <param name="id">note id</param>
        /// <returns>string message</returns>
        public string ArchiveNote(int id)
        {
            try
            {
                var note = this.context.NoteTable.FirstOrDefault(x => x.NoteId == id);
                if (note.IsArchive == false)
                {
                    note.IsArchive = true;
                    this.context.SaveChanges();
                    return "Note Is Archive";
                }
                else
                {
                    note.IsArchive = false;
                    this.context.SaveChanges();
                    return "Note Is UnArchive";
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }      
        /// <summary>
        /// Method to Trash Or Restore Note
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns>string message</returns>
        public string TrashOrRestoreNote(int id)
        {
            try
            {
                string message;
                var note = this.context.NoteTable.Where(x => x.NoteId == id).SingleOrDefault();
                if (note != null)
                {
                    if (note.IsTrash == false)
                    {
                        note.IsTrash = true;
                        this.context.Entry(note).State = EntityState.Modified;
                        this.context.SaveChanges();
                        message = "Note Trashed";
                        return message;
                    }
                    if (note.IsTrash == true)
                    {
                        note.IsTrash = false;
                        this.context.Entry(note).State = EntityState.Modified;
                        this.context.SaveChanges();
                        message = "Note Restored";
                        return message;
                    }
                }

                return message = "Unable to Restore or Trash note.";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Method to add color for note
        /// </summary>
        /// <param name="id">note id</param>
        /// <param name="color">color name</param>
        /// <returns>string message</returns>
        public Note AddColor(long NoteId, ColorModel model)
        {
            try
            {
               // colorModel message;
                var note = this.context.NoteTable.FirstOrDefault (x=> x.NoteId==NoteId);
                if (note != null)
                {
                    note.Color = model.Color;
                    this.context.SaveChanges();
                    return note;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Uploads the image.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="noteimage">The noteimage.</param>
        /// <returns></returns>
        public bool UploadImage(long noteId, IFormFile noteimage) //IFormFile transmitted files in HTTP request. 
        {
            try
            {
                var notes = this.context.NoteTable.Where(x => x.NoteId == noteId).SingleOrDefault();
                if (notes != null)
                {
                    Account account = new Account //Its cloudinary account
                    (
                    _config["CloudinaryAccount:CloudName"],
                    _config["CloudinaryAccount:ApiKey"],
                    _config["CloudinaryAccount:ApiSecret"]
                    );
                    var path = noteimage.OpenReadStream(); //for reading uploaded file
                    Cloudinary cloudinary = new Cloudinary(account);
                    ImageUploadParams uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(noteimage.FileName, path)
                    };
                    var uploadResult = cloudinary.Upload(uploadParams);
                    notes.Image = uploadResult.Url.ToString();
                    context.Entry(notes).State = EntityState.Modified; //updating that url to image column in DB 
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

