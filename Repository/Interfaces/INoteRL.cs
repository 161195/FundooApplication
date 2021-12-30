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
        public bool Registration(NoteRegistration user);
        public IEnumerable<Note> GetNoteRegistrations();
    }
}
