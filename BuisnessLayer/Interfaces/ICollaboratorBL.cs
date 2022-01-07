using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Interfaces
{
    public interface ICollaboratorBL
    {
        public bool CollabAdd(CollaboratorModel user);
    }
}
