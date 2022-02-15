using CommonLayer.Model;
using Microsoft.IdentityModel.Tokens;
using Repository.Context;
using Repository.Entity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using Experimental.System.Messaging;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Repository.Services
{
    public class UserRL : IUserRL      //Repository logic
    {        
        IConfiguration _config;
        readonly UserContext context;       
        public UserRL(UserContext context , IConfiguration config)
        {
            this.context = context;
            _config = config;
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
                newUser.Password = encryptpass(user.Password);
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
            try
            {              
                User ValidLogin = this.context.UserTable.Where(X => X.EmailId == User1.EmailId).FirstOrDefault();
                if (Decryptpass(ValidLogin.Password) == User1.Password)
                {                               
                    LoginResponse loginRespo = new LoginResponse();
                    string token = GenerateJWTToken(ValidLogin.EmailId,ValidLogin.UserId);                   
                    loginRespo.jwtToken = token;
                    return loginRespo;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                throw;
            }          
        }       
        /// <summary>
        /// created method to Generate Token
        /// </summary>
        /// <param name="EmailId"></param>
        /// <returns></returns>
        private string GenerateJWTToken(string EmailId, long UserId)
        {
            try
            {
                var loginTokenHandler = new JwtSecurityTokenHandler();
                var loginTokenKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config[("Jwt:key")]));
                var loginTokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Email, EmailId),
                        new Claim("UserId",UserId.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    SigningCredentials = new SigningCredentials(loginTokenKey, SecurityAlgorithms.HmacSha256Signature)
                };
                var token = loginTokenHandler.CreateToken(loginTokenDescriptor);
                return loginTokenHandler.WriteToken(token);

            }    
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Encryptpasses the specified password.
        /// </summary>
        /// <param name="Password">The password.</param>
        /// <returns></returns>
        public string encryptpass(string Password)
        {
            string msg = "";
            byte[] encode = new byte[Password.Length];
            encode = Encoding.UTF8.GetBytes(Password); 
            msg = Convert.ToBase64String(encode); //ToBase64String(Byte[]) Converts an array of 8-bit unsigned integers to its equivalent string representation that is encoded with base-64 digits
            return msg;
        }
        /// <summary>
        /// Decryptpasses the specified encryptpwd.
        /// </summary>
        /// <param name="encryptpwd">The encryptpwd.</param>
        /// <returns></returns>
        private string Decryptpass(string encryptpwd)
        {
            string decryptpwd = string.Empty;
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpwd = new String(decoded_char);
            return decryptpwd;
        }
        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public bool ForgetPassword(ForgetPasswordModel model)
        {
            User ValidLogin = this.context.UserTable.Where(X => X.EmailId == model.EmailId).FirstOrDefault();
            if (ValidLogin.EmailId != null)
            {
                var token = GenerateJWTToken(ValidLogin.EmailId, ValidLogin.UserId);

                new MsmqOperation().Sender(token);
                return true;
            }
            return false;          
        }
        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="reset">The reset.</param>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public bool ResetPassword(ChangePassword reset, string email)
        {
            User ValidLogin = this.context.UserTable.SingleOrDefault(x => x.EmailId == email);
            if (ValidLogin.EmailId != null)
            {
               //context.UserTable.Attach(ValidLogin);
                ValidLogin.Password = encryptpass(reset.ConfirmPassword);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }      
    }    
}
