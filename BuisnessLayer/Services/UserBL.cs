using BuisnessLayer.Interfaces;
using CommonLayer.Model;
using Repository.Entity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;

namespace BuisnessLayer.Services
{
    public class UserBL : IUserBL      //buisness logic     
    {
        IUserRL UserRL;
        public UserBL(IUserRL userRL)  
        {
            this.UserRL = userRL;
        }

        public IEnumerable<User> GetUserRegistrations()       //to get all registered data
        {
            return this.UserRL.GetUserRegistrations();
        }     
        public bool Registration(UserRegistration user)       //to register or post new data
        {         
            try
            {
                return this.UserRL.Registration(user);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public LoginResponse GetLogin(UserLogin User1)   //to post emailid and password-login part
        {
            try
            {
                return this.UserRL.GetLogin(User1);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool UserDelete(deleteOperation user)  //to delete registered data
        {
            try
            {
                return this.UserRL.UserDelete(user);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool ForgetPassword(string email)
        {
            try
            {
                return this.UserRL.ForgetPassword(email);
            }
            catch (Exception ex)
            {
                throw;
            }           
        }
        public bool ResetPassword(ChangePassword reset, string email)
        {
            try
            {
                return this.UserRL.ResetPassword(reset, email);  
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
