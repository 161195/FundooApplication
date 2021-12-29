using Repository.Context;
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
    }
}
