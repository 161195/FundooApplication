using BuisnessLayer.Interfaces;
using CommonLayer.Model;
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
        public bool Registration(NoteRegistration user)
        {
            try
            {
                return this.NoteRL.Registration(user);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
