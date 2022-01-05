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
        ///// <summary>
        ///// Method implementation to get pinned note
        ///// </summary>
        ///// <returns>pinned note</returns>
        //public IEnumerable<Notes> GetPinnedNote()
        //{
        //    try
        //    {
        //        IEnumerable<Notes> result;
        //        var note = this.context.NotesTable.Where(x => x.IsPin == true);
        //        result = note;
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
        public string PinNote(int id)
        {
            try
            {
                string message;
                var newNote = new Note() { NoteId = id };
                var note = this.context.NoteTable.FirstOrDefault(x => x.NoteId == id).IsPin;
                if (note == false)
                {

                    var pinNote = this.context.NoteTable.FirstOrDefault(x => x.NoteId == id).IsPin == true;
                    var pinThisNote = context.NoteTable.FirstOrDefault(u => u.NoteId == id);
                    pinThisNote.IsPin = pinNote;
                    this.context.SaveChanges();

                    message = "Note Pinned";
                    return message;

                }
                return message = "Note is unpinned by default.";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }
        public string UnpinNote(int id)
        {
            try
            {
                string message;
                var newNote = new Note() { NoteId = id };
                var note = this.context.NoteTable.FirstOrDefault(x => x.NoteId == id).IsPin;
                if (note == true)
                {
                    var unpinNote = this.context.NoteTable.FirstOrDefault(x => x.NoteId == id).IsPin == false;
                    var unpinThisNote = context.NoteTable.FirstOrDefault(u => u.NoteId == id);
                    unpinThisNote.IsPin = unpinNote;
                    this.context.SaveChanges();
                    message = "Note Unpinned";
                    return message;
                }
                return message = "Note is unpinned by default.";
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
                string message;
                var note = this.context.NoteTable.FirstOrDefault(x => x.NoteId == id).IsArchive;
                if (note == false)
                {
                    var archiveNote = this.context.NoteTable.FirstOrDefault(x => x.NoteId == id).IsArchive == true;
                    var archiveThisNote = context.NoteTable.FirstOrDefault(u => u.NoteId == id);
                    archiveThisNote.IsArchive = archiveNote;
                    this.context.SaveChanges();
                    message = "Note Archived";
                    return message;
                }

                return message = "Unable to archive note.";
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
        public string UnarchiveNote(int id)
        {
            try
            {
                string message;
                var note = this.context.NoteTable.FirstOrDefault(x => x.NoteId == id).IsArchive;
                if (note == true)
                {
                    var unarchiveNote = this.context.NoteTable.FirstOrDefault(x => x.NoteId == id).IsArchive == false;
                    var unarchiveThisNote = context.NoteTable.FirstOrDefault(u => u.NoteId == id);
                    unarchiveThisNote.IsArchive = unarchiveNote;
                    this.context.SaveChanges();
                    message = "Note Unarchived";
                    return message;
                }

                return message = "Unable to unarchive note.";
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

