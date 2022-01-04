using CommonLayer.Model;
using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Interfaces
{
    public interface INoteBL
    {
        public bool Registration(NoteRegistration user);   //To register new note
        public IEnumerable<Note> GetNoteRegistrations();   //To get all notes in table
        public Note GetWithId(long id);   //To get specific note for specific UserID
        public void UpdateNotes(Note BeforeNote, Note AfterNote);  //To update registered data
        public void DeleteNotes(Note person); //To delete particular notes
    }
}
