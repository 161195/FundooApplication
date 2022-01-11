using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
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
        public bool Registration(NoteRegistration user,long UserId);   //To register new note
        public IEnumerable<Note> GetNoteRegistrations(long UserId);   //To get all notes in table
        public Note GetWithId(long id);   //To get specific note for specific UserID
        public void UpdateNotes(Note BeforeNote, Note AfterNote);  //To update registered data
        public void DeleteNotes(Note user1); //To delete particular notes
        public string PinNote(int id);
        public string ArchiveNote(int id);
        public string TrashOrRestoreNote(int id);
        public string AddColor(long NoteId, string color);
        public bool UploadImage(long noteId, IFormFile noteimage);
    }
}
