using CommonLayer.Model;
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
        private const string key = "fundooapplicationdone"; //for secret key generation

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
                    string token = "";              
                    LoginResponse loginRespo = new LoginResponse();
                    token = GenerateJWTToken(ValidLogin.EmailId);                   
                    loginRespo.UserId = ValidLogin.UserId;
                    loginRespo.EmailId = ValidLogin.EmailId;
                    loginRespo.token = token;
                    return loginRespo;
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
        /// <summary>
        /// created method to Generate Token
        /// </summary>
        /// <param name="EmailId"></param>
        /// <returns></returns>
        private string GenerateJWTToken(string EmailId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
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
                    DeleteUser.Password = encryptpass(ValidUser.Password);
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
            msg = Convert.ToBase64String(encode);
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
    }    
}
