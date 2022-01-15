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
        /// <summary>
        /// Lables the add.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <returns></returns>
        public Lable LableAdd(LabelModel user, long UserId)
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
        /// <summary>
        /// Updates the lable.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <returns></returns>
        public Lable UpdateLable(LabelModel user, long UserId)
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
        /// <summary>
        /// Deletes the lable.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
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
        /// <summary>
        /// Removes the note.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
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
        /// <summary>
        /// Gets the lable registrations.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public IEnumerable<Lable> GetLableRegistrations(LabelModel user)
        {
            return this.LableRL.GetLableRegistrations(user);
        }
    }
}
