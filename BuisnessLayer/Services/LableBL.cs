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
    }
}
