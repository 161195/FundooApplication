using BuisnessLayer.Instance;
using CommonLayer.Model;
using Repository.Instance;
using System;

namespace BuisnessLayer.Services
{
    public class UserBL : IUserBL
    {
        IUserRL UserRL;
        public UserBL(IUserRL userRL)
        {
            this.UserRL = userRL;
        }
        public bool Registration(UserRegistration user)
        {
            // throw new NotImplementedException();
            try
            {
                return this.UserRL.Registration(user);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

       
    }
}
