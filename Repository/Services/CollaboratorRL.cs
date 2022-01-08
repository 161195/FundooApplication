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
        public bool CollabAdd(CollaboratorModel user, long UserId)
        {
            try
            {
                var notes = this.context.NoteTable.Where(x => x.NoteId == user.NoteId && x.UserId == UserId).SingleOrDefault();
                var mail = this.context.UserTable.Where(x => x.EmailId == user.EmailId).SingleOrDefault();
                if (notes != null && mail != null)
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
        public string RemoveCollaborate(CollaboratorModel collaborate, long UserId)
        {
            try
            {
                var note = this.context.NoteTable.FirstOrDefault(x => x.NoteId == collaborate.NoteId && x.UserId == UserId);
                var UserEnter = this.context.UserTable.FirstOrDefault(x => x.EmailId == collaborate.EmailId);
                if (note != null && UserEnter.EmailId != null)
                {
                    CollabEntity UserRemoved = this.context.CollabEntityTable.FirstOrDefault(x => x.EmailId == collaborate.EmailId && x.NoteId == collaborate.NoteId);
                    if (UserRemoved != null)
                    this.context.CollabEntityTable.Remove(UserRemoved);
                    int result = this.context.SaveChanges();
                    if (result > 0)
                    {
                        return "Collaboration has been removed";
                    }
                    else
                    {
                        return "Collaboration has not been removed ";
                    }
                }
                else
                {
                    return "You have no permission to Collaborate. Invalid ID";
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

    }
}
