using CommonLayer.Model;
using Repository.Context;
using Repository.Entity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Services
{
    public class UserRL : IUserRL      //Repository logic
    {
        readonly UserContext context;
        public UserRL(UserContext context)
        {
            this.context = context;
        }
        public IEnumerable<User> GetUserRegistrations()     //to get all the registered data
        {
            return context.UserTable.ToList();
        }      
        public bool Registration(UserRegistration user)    //to register new entry in registration
        {
            try
            {
                User newUser = new User();
                newUser.FirstName = user.FirstName;
                newUser.LastName = user.LastName;
                newUser.Password = user.Password;
                newUser.EmailId = user.EmailId;
                newUser.Createdat = DateTime.Now;
                newUser.Modified = DateTime.Now;

                //adding user details to the database user table 
                this.context.UserTable.Add(newUser);

                int result = this.context.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public LoginResponse GetLogin(UserLogin User1)  //to check login and password
        {
            try
            {
                User ValidLogin = this.context.UserTable.Where(X => X.EmailId == User1.EmailId && X.Password == User1.Password).FirstOrDefault();
                if (ValidLogin.Id != 0 && ValidLogin.EmailId != null)
                {
                    LoginResponse loginResponse = new LoginResponse();
                    loginResponse.Id = ValidLogin.Id;
                    loginResponse.FirstName = ValidLogin.FirstName;
                    loginResponse.LastName = ValidLogin.LastName;
                    loginResponse.EmailId = ValidLogin.EmailId;
                    loginResponse.Createdat = ValidLogin.Createdat;
                    loginResponse.Modified = ValidLogin.Modified;
                    return loginResponse;
                }
                else
                {
                    return null;
                }
            }
            catch(ArgumentException ex)
            {
                throw new Exception(ex.Message);
            }
        }           
    }
}
