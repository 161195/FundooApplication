using CommonLayer.Model;
using Repository.Context;
using Repository.Entity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
    public class CollaboratorRL: ICollaboratorRL
    {
        readonly UserContext context;
        public CollaboratorRL(UserContext context)
        {
            this.context = context;            
        }
        public bool CollabAdd(CollaboratorModel user)
        {
            try
            {
                var Cd = this.context.NoteTable.Where(x => x.NoteId == user.NoteId).SingleOrDefault();
                var Cd1 = this.context.UserTable.Where(x => x.EmailId == user.EmailId).SingleOrDefault();
                if (Cd != null && Cd1 != null)
                {
                    CollabEntity AddCollabNote = new CollabEntity();
                    AddCollabNote.NoteId = user.NoteId;
                    AddCollabNote.EmailId = user.EmailId;
                    this.context.CollabEntityTable.Add(AddCollabNote);                                   
                }
                var result = this.context.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
                       
        }

    }
}
