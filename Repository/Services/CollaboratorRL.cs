using CommonLayer.Model;
using CommonLayer.Model.ResponseModel;
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
        /// <summary>
        /// Collabs the add.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <returns></returns>
        public CollaboratorResponse CollabAdd(CollaboratorModel user, long UserId)
        {
            try
            {
                var notes = this.context.NoteTable.Where(x => x.NoteId == user.NoteId && x.UserId == UserId).FirstOrDefault();
                var mail = this.context.UserTable.Where(x => x.EmailId == user.EmailId).FirstOrDefault();
                if (notes != null && mail != null)
                {
                    Collaborator AddCollabNote = new Collaborator();
                    AddCollabNote.NoteId = user.NoteId;
                    AddCollabNote.EmailId = user.EmailId;
                    AddCollabNote.UserId = UserId;
                    this.context.CollabEntityTable.Add(AddCollabNote);
                    var result = this.context.SaveChanges();
                    Collaborator respo = this.context.CollabEntityTable.Where(x => x.EmailId == user.EmailId && x.NoteId==user.NoteId).FirstOrDefault();
                    CollaboratorResponse response = new CollaboratorResponse();
                    response.EmailId = respo.EmailId;
                    response.NoteId = respo.NoteId;
                    response.UserId = respo.UserId;
                    response.CollabsId = respo.CollabsId;
                    return response;
                }               
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
                       
        }
        /// <summary>
        /// Removes the collaborate.
        /// </summary>
        /// <param name="collaborate">The collaborate.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <returns></returns>
        public string RemoveCollaborate(CollaboratorModel collaborate, long UserId)
        {
            try
            {
                var note = this.context.NoteTable.FirstOrDefault(x => x.NoteId == collaborate.NoteId && x.UserId == UserId);
                var UserEnter = this.context.UserTable.FirstOrDefault(x => x.EmailId == collaborate.EmailId);
                if (note != null && UserEnter.EmailId != null)
                {
                    Collaborator UserRemoved = this.context.CollabEntityTable.FirstOrDefault(x => x.EmailId == collaborate.EmailId && x.NoteId == collaborate.NoteId);
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
                throw;
            }

        }

    }
}
