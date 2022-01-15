using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
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
        public void DeleteNotes(Note user1);    //To delete notes  
        public string PinNote(int id); //to pin unpin notes   
        public string ArchiveNote(int id); //to archive unarchive notes
        public string TrashOrRestoreNote(int id); //to trash untrash notes
        public string AddColor(long NoteId, string color);//to color notes
        public bool UploadImage(long noteId, IFormFile noteimage); //Add image to notes
    }
}
