using CommonLayer.Model;
using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Interfaces
{
    public interface ILableBL
    {
        public Lable LableAdd(LabelModel user, long UserId);//create lable name
        public Lable UpdateLable(LabelModel user, long UserId);//add note to lable or multiple notes
        public bool DeleteLable(LabelModel user);//delete lable directly 
        public bool RemoveNote(LabelModel user);//remove specific note
        public IEnumerable<Lable> GetLableRegistrations(LabelModel user);//get all notes in the lable
    }
}
