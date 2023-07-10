using CommanLayer;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;


namespace RepositoryLayer.Service
{
    public class UserRepository : IUserRepository
    {
        private readonly FundoContext fundoContext;
        private readonly IConfiguration configuration; // JWT token

        public UserRepository(FundoContext fundoContext, IConfiguration configuration)
        {
            this.fundoContext = fundoContext;
            this.configuration = configuration;
        }

        public UserEntity UserRegistration(RegisterModel registerModel)
        {
            try
            {
                var result = fundoContext.User.Where(x => x.Email == registerModel.Email).FirstOrDefault();

                UserEntity userEntity = new UserEntity();

                userEntity.FirstName = registerModel.FirstName;
                userEntity.LastName = registerModel.LastName;
                userEntity.Email = registerModel.Email;
                userEntity.Password = EncodePasswordToBase64(registerModel.Password);

                fundoContext.Add(userEntity);
                fundoContext.SaveChanges();

                return userEntity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

        public string UserLogin(LoginModel loginModel)
        {
            try
            {
                var encodedPassword = EncodePasswordToBase64(loginModel.Password);
                var result = fundoContext.User.Where(x => x.Email == loginModel.Email && x.Password == encodedPassword).FirstOrDefault();

                if (result == null)
                {
                    return null;
                }
                else
                {
                    var token = GenerateToken(result.Email, result.UserID);
                    return token;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CheckingEmailExistOrNot(string email)
        {
            try
            {
                var result = fundoContext.User.Where(x => x.Email == email).FirstOrDefault();

                if (result == null)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ForgotPassWordModel UserForgotPassword(string email)
        {
            try
            {
                var result = fundoContext.User.Where(x => x.Email == email).FirstOrDefault();

                ForgotPassWordModel forgotPassWordModel = new ForgotPassWordModel();
                forgotPassWordModel.Email = result.Email;
                forgotPassWordModel.Token = GenerateToken(result.Email, result.UserID);
                forgotPassWordModel.UserID = result.UserID;

                return forgotPassWordModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ResetPassword(UserResetPasswordModel userResetPasswordModel, string email)
        {
            try
            {
                var result = fundoContext.User.Where(x => x.Email == email).FirstOrDefault();
                result.Password = EncodePasswordToBase64(userResetPasswordModel.ConfirmPassword);

                fundoContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        public string GenerateToken(string email, int userId)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var claims = new[]
                {
                new Claim("Email",email),
                new Claim("UserID",userId.ToString())
                };
                var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                    configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
