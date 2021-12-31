﻿using BuisnessLayer.Interfaces;
using CommonLayer.Model;
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
        public bool Registration(NoteRegistration user)
        {
            try
            {
                return this.NoteRL.Registration(user);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public IEnumerable<Note> GetNoteRegistrations()
        {
            return this.NoteRL.GetNoteRegistrations();
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
    }
    
}
