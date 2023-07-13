using BusinessLeyer.Interface;
using CommanLayer;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RepositoryLayer.Entity;
using System;
using System.Threading.Tasks;

namespace FundoNotesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness userBusiness;
        private readonly IBus _bus;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserBusiness userBusiness, IBus bus , ILogger<UserController> logger)
        {
            this.userBusiness = userBusiness;
            this._bus = bus;
            _logger = logger;
        }

        // localhost /api/controlerName/HttpName
        [HttpPost("register")]
        public IActionResult UserRegistration(RegisterModel registerModel)
        {
            try
            {
                if (userBusiness.CheckingEmailExistOrNot(registerModel.Email))
                {
                    return BadRequest(new ResponseModel<UserEntity> { status = false, message = "The email all ready exist / user registration not successfull" });
                }
                else
                {
                    var registration = userBusiness.UserRegistration(registerModel);
                    return Ok(new ResponseModel<UserEntity> { status = true, message = "user registration successfull", response = registration });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("login")]
        public IActionResult UserLogin(LoginModel loginModel)
        {
            try
            {
                var login = userBusiness.UserLogin(loginModel);

                if (login != null)
                {
                    _logger.LogDebug("user id found in database");
                    return Ok(new ResponseModel<string> { status = true, message = "login succesfull", response = login });
                }
                else
                {
                    _logger.LogDebug("user id not found in data base");
                    return BadRequest(new ResponseModel<string> { status = false, message = "login not succesfull" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> UserForgotPassword(string email)
        {
            try
            {
                if (userBusiness.CheckingEmailExistOrNot(email))
                {
                    Send send = new Send();
                    ForgotPassWordModel forgotPasswordModel = userBusiness.UserForgotPassword(email);
                    send.SendingMail(forgotPasswordModel.Email, forgotPasswordModel.Token);

                    Uri uri = new Uri("rabbitmq://localhost/FundoNotesEmail_Queue");
                    var endPoint = await _bus.GetSendEndpoint(uri);

                    await endPoint.Send(forgotPasswordModel);
                    return Ok(new ResponseModel<string> { status = true, message = "email send succesfull", response = email });
                }
                return BadRequest(new ResponseModel<string> { status = false, message = "email not send succesfull", response = email });
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPost("reset-password")]
        public IActionResult ResetPassword(UserResetPasswordModel userResetPasswordModel)
        {
            try
            {
                string email = User.FindFirst("Email").Value;
                if (userResetPasswordModel.Password == userResetPasswordModel.ConfirmPassword)
                {
                    bool result = userBusiness.ResetPassword(userResetPasswordModel, email);
                    if(result)
                    {
                        return Ok(new ResponseModel<string> { status = true, message = "password change succesfull", response = email });
                    }
                }
                return BadRequest(Ok(new ResponseModel<string> { status = true, message = "password is not same as confirm password" }));
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
