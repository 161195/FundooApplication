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
    public class LableRL : ILableRL
    {
        readonly UserContext context;
        public LableRL(UserContext context)
        {
            this.context = context;
        }
        public bool LableAdd(LabelModel user, long UserId)
        {
            try
            {
                var userid = this.context.UserTable.Where(x => x.UserId == UserId).SingleOrDefault();
                //var Lable = this.context.UserTable.Where(x => x.UserId == UserId).SingleOrDefault();
                if (userid != null)
                {
                    Lable AddLableToNote = new Lable();
                    AddLableToNote.NoteId = user.NoteId;
                    AddLableToNote.Lables = user.Lables;
                    AddLableToNote.UserId = UserId;
                    this.context.LableTable.Add(AddLableToNote);
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
    }
}

