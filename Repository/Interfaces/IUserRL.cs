using CommonLayer.Model;
using Repository.Entity;
using System.Collections.Generic;

namespace Repository.Interfaces
{
    public interface IUserRL
    {
        public bool Registration(UserRegistration User);   //to post new registration data
        IEnumerable<User> GetUserRegistrations();  //to get all registered data
        public LoginResponse GetLogin(UserLogin User1);  //to post login
    }
}

