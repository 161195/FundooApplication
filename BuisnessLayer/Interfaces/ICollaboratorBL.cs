using CommonLayer.Model;
using CommonLayer.Model.ResponseModel;
using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Interfaces
{
    public interface ICollaboratorBL
    {
        public CollaboratorResponse CollabAdd(CollaboratorModel user,long UserId);
        public string RemoveCollaborate(CollaboratorModel collaborate, long UserId);
    }
}
