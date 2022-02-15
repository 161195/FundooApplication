using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Model
{
    public class NotesModel
    {
        public long NoteId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }  
        public DateTime? CreatedAt { get; set; }
        public long UserId { get; set; }

    }
}
