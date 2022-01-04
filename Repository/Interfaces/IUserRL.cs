using CommonLayer.Model;
using Repository.Entity;
using System.Collections.Generic;

namespace Repository.Interfaces
{
    public interface IUserRL
    {
        public bool Registration(UserRegistration user);   //to post new registration data
        IEnumerable<User> GetUserRegistrations();  //to get all registered data
        public LoginResponse GetLogin(UserLogin User1);  //to post login
        public bool UserDelete(deleteOperation user);//to delete registered data
        public bool ForgetPassword(string email);//to generate token and send it to mail
        public bool ResetPassword(ChangePassword reset, string email);
       
    }
}

