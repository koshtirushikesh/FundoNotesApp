using BusinessLeyer.Interface;
using CommanLayer;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
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

        public UserController(IUserBusiness userBusiness, IBus bus)
        {
            this.userBusiness = userBusiness;
            this._bus = bus;
        }

        // localhost /api/controlerName/HttpName
        [HttpPost("register")]
        public IActionResult UserRegistration(RegisterModel registerModel)
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

        [HttpPost("login")]
        public IActionResult UserLogin(LoginModel loginModel)
        {
            var login = userBusiness.UserLogin(loginModel);

            if (login != null)
            {
                return Ok(new ResponseModel<string> { status = true, message = "login succesfull", response = login });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { status = false, message = "login not succesfull" });
            }
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> UserForgotPassword(string email)
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
    }
}
