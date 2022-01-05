using CommonLayer.Model;
using Microsoft.EntityFrameworkCore;
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
        readonly UserContext context;
        public NoteRL(UserContext context)
        {
            this.context = context;
        }
        /// <summary>
        /// New Note Registrations for the specified userID.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public bool Registration(NoteRegistration user,long UserId)
        {
            try
            {
                Note newNote = new Note();
                newNote.UserId = UserId;
                newNote.Title = user.Title;
                newNote.Message = user.Message;
                newNote.Reminder = user.Reminder;
                newNote.Color = user.Color;
                newNote.Image = user.Image;
                newNote.IsArchive = user.IsArchive;
                newNote.IsPin = user.IsPin;
                newNote.IsTrash = user.IsTrash;
                newNote.CreatedAt = DateTime.Now;
                newNote.ModifiedAt = DateTime.Now;

                //adding Note data to the database Notetable 
                this.context.NoteTable.Add(newNote);

                int result = this.context.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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
        public IEnumerable<Note> GetNoteRegistrations()
        {
            return context.NoteTable;
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
                throw new Exception(ex.Message);
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
                        message = "Note Restored";
                        return message;
                    }
                    if (note.IsTrash == true)
                    {
                        note.IsTrash = false;
                        this.context.Entry(note).State = EntityState.Modified;
                        this.context.SaveChanges();
                        message = "Note Trashed";
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
        public string AddColor(long NoteId, string color)
        {
            try
            {
                string message;
                var note = this.context.NoteTable.Find(NoteId);
                if (note != null)
                {
                    note.Color = color;
                    this.context.Entry(note).State = EntityState.Modified;
                    this.context.SaveChanges();
                    message = "Color added Successfully for note !";
                    return message;
                }

                return message = "Error While adding color for this note";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

