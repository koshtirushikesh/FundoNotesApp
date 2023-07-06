using CommanLayer;
using MassTransit;
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
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class UserRepository : IUserRepository
    {
        private readonly FundoContext fundoContext;
        private readonly IConfiguration configuration;
        private readonly IBus _bus;

        public UserRepository(FundoContext fundoContext, IConfiguration configuration, IBus bus)
        {
            this.fundoContext = fundoContext;
            this.configuration = configuration;
            _bus = bus;
        }

        public UserEntity UserRegistration(RegisterModel registerModel)
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


        public bool CheckingEmailExistOrNot(string email)
        {
            var result = fundoContext.User.Where(x => x.Email == email).FirstOrDefault();

            if (result == null)
                return false;
            else
                return true;
        }

        public ForgotPassWordModel UserForgotPassword(string email)
        {
            var result = fundoContext.User.Where(x => x.Email == email).FirstOrDefault();
            
            ForgotPassWordModel forgotPassWordModel = new ForgotPassWordModel();
            forgotPassWordModel.Email = result.Email;
            forgotPassWordModel.Token = GenerateToken(result.Email, result.UserID);
            forgotPassWordModel.UserID = result.UserID;
            return forgotPassWordModel;
        }

        public string GenerateToken(string email, int userId)
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
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
