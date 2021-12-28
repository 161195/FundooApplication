using BuisnessLayer.Interfaces;
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
    }
}
