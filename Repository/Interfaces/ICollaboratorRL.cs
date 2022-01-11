﻿using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
   public interface ICollaboratorRL
   {
        public bool CollabAdd(CollaboratorModel user, long UserId);
        public string RemoveCollaborate(CollaboratorModel collaborate, long UserId);
   }
}