using BusinessLeyer.Interface;
using CommanLayer;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;

namespace BusinessLeyer.Service
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepository userRepository;
        public UserBusiness(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public UserEntity UserRegistration(RegisterModel registerModel)
        {
            try { return userRepository.UserRegistration(registerModel); } catch (Exception ex) { throw ex; }
        }

        public string UserLogin(LoginModel loginModel)
        {
            try { return userRepository.UserLogin(loginModel); } catch (Exception ex) { throw ex; }
        }

        public bool CheckingEmailExistOrNot(string email)
        {
            try { return userRepository.CheckingEmailExistOrNot(email); } catch (Exception ex) { throw ex; }
        }

        public ForgotPassWordModel UserForgotPassword(string email)
        {
            try { return userRepository.UserForgotPassword(email); } catch(Exception ex) { throw ex; }
        }

        public bool ResetPassword(UserResetPasswordModel userResetPasswordModel, string email)
        {
            try { return userRepository.ResetPassword(userResetPasswordModel, email); } catch (Exception ex) { throw ex; }
        }

    }
}
