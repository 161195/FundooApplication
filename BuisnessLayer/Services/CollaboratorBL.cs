using BuisnessLayer.Interfaces;
using CommonLayer.Model;
using CommonLayer.Model.ResponseModel;
using Repository.Entity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Services
{
    public class CollaboratorBL: ICollaboratorBL
    {
        ICollaboratorRL CollaboratorRL;
        public CollaboratorBL(ICollaboratorRL CollaboratorRL)
        {
            this.CollaboratorRL = CollaboratorRL;
        }
        /// <summary>
        /// Collabs the add.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <returns></returns>
        public CollaboratorResponse CollabAdd(CollaboratorModel user,long UserId)       //to register or post new Collaboration
        {
            try
            {
                return this.CollaboratorRL.CollabAdd(user, UserId);
            }
            catch (Exception ex)
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
                return this.CollaboratorRL.RemoveCollaborate(collaborate, UserId);
            }
            catch (Exception e)
            {
                throw;
            }

        }
    }
}
