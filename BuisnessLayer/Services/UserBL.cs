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
        /// <summary>
        /// Gets the user registrations.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> GetUserRegistrations()       //to get all registered data
        {
            return this.UserRL.GetUserRegistrations();
        }
        /// <summary>
        /// Registrations the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
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
        /// <summary>
        /// Gets the login.
        /// </summary>
        /// <param name="User1">The user1.</param>
        /// <returns></returns>
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
        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public bool ForgetPassword(ForgetPasswordModel model) //To apply for forget password and get the reset token
        {
            try
            {
                return this.UserRL.ForgetPassword(model);
            }
            catch (Exception ex)
            {
                throw;
            }           
        }
        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="reset">The reset.</param>
        /// <param name="email">The email.</param>
        /// <returns></returns>
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
