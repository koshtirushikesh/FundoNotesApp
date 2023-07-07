using CommanLayer;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLeyer.Interface
{
    public interface IUserBusiness
    {
        public UserEntity UserRegistration(RegisterModel registerModel);
        public string UserLogin(LoginModel loginModel);
        public bool CheckingEmailExistOrNot(string email);
        public ForgotPassWordModel UserForgotPassword(string email);
        public bool ResetPassword(UserResetPasswordModel userResetPasswordModel, string email);
    }
}
