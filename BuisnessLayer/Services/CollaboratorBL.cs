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
        public bool CollabAdd(CollaboratorModel user)       //to register or post new Collaboration
        {
            try
            {
                return this.CollaboratorRL.CollabAdd(user);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
