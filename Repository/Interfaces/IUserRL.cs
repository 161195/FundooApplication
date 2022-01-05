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
        public bool ForgetPassword(ForgetPasswordModel model); //To apply for forget password and get the reset token
        public bool ResetPassword(ChangePassword reset, string email); //To reset existing password
       
    }
}

