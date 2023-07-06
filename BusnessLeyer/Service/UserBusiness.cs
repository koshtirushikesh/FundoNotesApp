using BusinessLeyer.Interface;
using CommanLayer;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;

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
            return userRepository.UserRegistration(registerModel);
        }

        public string UserLogin(LoginModel loginModel)
        {
            return userRepository.UserLogin(loginModel);
        }

        public bool CheckingEmailExistOrNot(string email)
        {
            return userRepository.CheckingEmailExistOrNot(email);
        }

        public ForgotPassWordModel UserForgotPassword(string email)
        {
            return userRepository.UserForgotPassword(email);
        }

    }
}
