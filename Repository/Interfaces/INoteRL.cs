using CommonLayer.Model;
using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface INoteRL
    {
        public bool Registration(NoteRegistration user,long UserId);  //To register new note
        public IEnumerable<Note> GetNoteRegistrations(long UserId);   //to get all registered notes
        public Note GetWithId(long id);  //To get specific note for specific UserID
        public void UpdateNotes(Note BeforeNote, Note AfterNote); //To update registered data
        public void DeleteNotes(Note user1);      
        public string PinNote(int id);    
        public string ArchiveNote(int id);
        public string TrashOrRestoreNote(int id);
        public string AddColor(long NoteId, string color);
    }
}
