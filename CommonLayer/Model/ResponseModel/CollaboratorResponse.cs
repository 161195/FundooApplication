using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Model.ResponseModel
{
    public class CollaboratorResponse
    {
        public long CollabsId { get; set; }
        public long NoteId { get; set; }     
        public long UserId { get; set; }
        public string EmailId { get; set; }
    }
}
