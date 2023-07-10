using CommanLayer;
using RepositoryLayer.Entity;

namespace RepositoryLayer.Interface
{
    public interface IUserRepository
    {
        public UserEntity UserRegistration(RegisterModel registerModel);
        public string UserLogin(LoginModel loginModel);
        public bool CheckingEmailExistOrNot(string email);
        public ForgotPassWordModel UserForgotPassword(string email);
        public bool ResetPassword(UserResetPasswordModel userResetPasswordModel, string email);
    }
}
