using CommonLayer.Model;
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
        public bool Registration(NoteRegistration user)
        {
            try
            {
                Note newNote = new Note();
                newNote.UserId = user.UserId;
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
    }
}
