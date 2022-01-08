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
    public class CollaboratorBL: ICollaboratorBL
    {
        ICollaboratorRL CollaboratorRL;
        public CollaboratorBL(ICollaboratorRL CollaboratorRL)
        {
            this.CollaboratorRL = CollaboratorRL;
        }
        public bool CollabAdd(CollaboratorModel user,long UserId)       //to register or post new Collaboration
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
        public string RemoveCollaborate(CollaboratorModel collaborate, long UserId)
        {
            try
            {
                return this.CollaboratorRL.RemoveCollaborate(collaborate, UserId);
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }

        }
    }
}
