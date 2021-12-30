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
        public bool Registration(NoteRegistration user);
        public IEnumerable<Note> GetNoteRegistrations();
    }
}
