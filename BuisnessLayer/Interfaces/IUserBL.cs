using CommonLayer.Model;
using Repository.Entity;
using System.Collections.Generic;

namespace BuisnessLayer.Interfaces
{
    public interface IUserBL
    {      
        public bool Registration(UserRegistration user);   //to post new registration data        
        public IEnumerable<User> GetUserRegistrations();   //to get all registered data
        public LoginResponse GetLogin(UserLogin User1);  //to post login
        public bool UserDelete(deleteOperation user);   //to delete registered data
    }
}
