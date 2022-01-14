using CommonLayer.Model;
using CommonLayer.Model.ResponseModel;
using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
   public interface ICollaboratorRL
   {
        public CollaboratorResponse CollabAdd(CollaboratorModel user, long UserId);
        public string RemoveCollaborate(CollaboratorModel collaborate, long UserId);
   }
}
