using BuisnessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Repository.Entity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Services
{
    public class NoteBL :INoteBL
    {
        INoteRL NoteRL;
        public NoteBL(INoteRL noteRL)
        {
            this.NoteRL = noteRL;
        }
        /// <summary>
        /// Registrations the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <returns></returns>
        public NoteRegistration Registration(NotesModel model, long UserId)
        {
            try
            {
                return this.NoteRL.Registration(model, UserId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// Gets the note registrations.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <returns></returns>
        public IEnumerable<Note> GetNoteRegistrations(long UserId)
        {
            return this.NoteRL.GetNoteRegistrations(UserId);
        }
        /// <summary>
        /// Gets the with identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Note GetWithId(long id)
        {
            try
            {
                return this.NoteRL.GetWithId(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Updates the notes.
        /// </summary>
        /// <param name="BeforeNote">The before note.</param>
        /// <param name="AfterNote">The after note.</param>
        public void UpdateNotes(Note BeforeNote, Note AfterNote)
        {
            try
            {
                this.NoteRL.UpdateNotes(BeforeNote, AfterNote);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Deletes the notes.
        /// </summary>
        /// <param name="user1">The user1.</param>
        public void DeleteNotes(Note user1)
        {
            try
            {
                this.NoteRL.DeleteNotes(user1);
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
        public string PinNote(int id)
        {
            try
            {
                var note = this.NoteRL.PinNote(id);
                return note;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// Archives the note.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public string ArchiveNote(int id)
        {
            try
            {
                var note = this.NoteRL.ArchiveNote(id);
                return note;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Trashes the or restore note.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public string TrashOrRestoreNote(int id)
        {
            try
            {
                var note = this.NoteRL.TrashOrRestoreNote(id);
                return note;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Adds the color.
        /// </summary>
        /// <param name="NoteId">The note identifier.</param>
        /// <param name="color">The color.</param>
        /// <returns></returns>
        public Note AddColor(long NoteId, ColorModel model)
        {
            try
            {
               return this.NoteRL.AddColor(NoteId, model);
               
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// Uploads the image.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="noteimage">The noteimage.</param>
        /// <returns></returns>
        public bool UploadImage(long noteId, IFormFile noteimage)
        {
            try
            {
                return this.NoteRL.UploadImage(noteId, noteimage);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}


