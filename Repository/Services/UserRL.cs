﻿using CommonLayer.Model;
using Microsoft.IdentityModel.Tokens;
using Repository.Context;
using Repository.Entity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Repository.Services
{
    public class UserRL : IUserRL      //Repository logic
    {
        private const string Secret = "fundooapplicationdone"; //for secret key generation

        readonly UserContext context;  
        public UserRL(UserContext context)
        {
            this.context = context;
        }
        /// <summary>
        /// to get all the registered data
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> GetUserRegistrations()    
        {
            return context.UserTable;
        }
        /// <summary>
        /// to register new entry in registration
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public bool Registration(UserRegistration user)    
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
        /// <summary>
        /// to check login and password & Get the login.
        /// </summary>
        /// <param name="User1">The user1.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public LoginResponse GetLogin(UserLogin User1)  
        {
            LoginResponse loginRespo = new LoginResponse();
            string token = "";
            try
            {
                User ValidLogin = this.context.UserTable.Where(X => X.EmailId == User1.EmailId && X.Password == User1.Password).FirstOrDefault();
                if(ValidLogin != null)
                {
                    token = GenerateJWTToken(ValidLogin.EmailId);
                    loginRespo.FirstName = ValidLogin.FirstName;
                    loginRespo.LastName = ValidLogin.LastName;
                    loginRespo.Createdat = ValidLogin.Createdat;
                    loginRespo.Modified = ValidLogin.Modified;
                    loginRespo.Id = ValidLogin.Id;
                    loginRespo.EmailId = ValidLogin.EmailId;
                    loginRespo.token = token;
                }
            }
            catch(ArgumentException ex)
            {
                throw new Exception(ex.Message);
            }
            return loginRespo;
        }
        /// <summary>
        /// created method to Generate Token
        /// </summary>
        /// <param name="EmailId"></param>
        /// <returns></returns>
        private string GenerateJWTToken(string EmailId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
            new Claim("EmailId",EmailId)         
            };
            var token = new JwtSecurityToken("Mayuri",EmailId,
              claims,
              expires: DateTime.Now.AddMinutes(20),
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        /// <summary>
        /// to delete registered data
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public bool UserDelete(deleteOperation user)
        {
            try
            {
                User ValidUser = this.context.UserTable.Where(X => X.FirstName == user.FirstName && X.LastName == user.LastName).FirstOrDefault();
                if (ValidUser != null)
                {
                    User DeleteUser = new User();
                    DeleteUser.FirstName = ValidUser.FirstName;
                    DeleteUser.LastName = ValidUser.LastName;
                    DeleteUser.Password = ValidUser.Password;
                    DeleteUser.EmailId = ValidUser.EmailId;
                    DeleteUser.Createdat = DateTime.Now;
                    DeleteUser.Modified = DateTime.Now;
                }
                //Deleting user details from the database user table 
                this.context.UserTable.Remove(ValidUser);

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
    }    
}