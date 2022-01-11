using BuisnessLayer.Interfaces;
using CommonLayer.Model;
using Repository.Entity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Services
{
    public class LableBL:ILableBL
    {
        ILableRL LableRL;
        public LableBL(ILableRL LableRL)
        {
            this.LableRL = LableRL;
        }
        public bool LableAdd(LabelModel user, long UserId)
        {
            try
            {
                return this.LableRL.LableAdd(user, UserId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool UpdateLable(LabelModel user, long UserId)
        {
            try
            {
                return this.LableRL.UpdateLable(user, UserId);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public bool DeleteLable(LabelModel user)
        {
            try
            {
               return this.LableRL.DeleteLable(user);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool RemoveNote(LabelModel user)
        {
            try
            {
                return this.LableRL.RemoveNote(user);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<Lable> GetLableRegistrations(LabelModel user)
        {
            return this.LableRL.GetLableRegistrations(user);
        }

    }
}
