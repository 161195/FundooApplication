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
        public bool Registration(NoteRegistration user, long UserId)
        {
            try
            {
                return this.NoteRL.Registration(user,UserId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public IEnumerable<Note> GetNoteRegistrations(long UserId)
        {
            return this.NoteRL.GetNoteRegistrations(UserId);
        }
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
        public string AddColor(long NoteId, string color)
        {
            try
            {
                string message = this.NoteRL.AddColor(NoteId, color);
                return message;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
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


