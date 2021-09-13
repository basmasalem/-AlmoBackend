
using Core.Api.Helpers;
using Core.Api.ViewModels;
using Core.Model;
using Core.Service;
using Core.Service.Utilities;
using Core.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;
      
        private readonly ISubscribeRequestService _subscribeRequestService;
        private readonly AppSettings _appSettings;
        private readonly IEmailSender _emailSender;

        public AccountController(IOptions<AppSettings> appSettings, IEmailSender _emailSender,IUserService userService, ISubscribeRequestService subscribeRequestService):base(_emailSender)
        {
            _userService = userService;
            _subscribeRequestService = subscribeRequestService;
           
            _appSettings = appSettings.Value;
            this._emailSender = _emailSender;
     
        }
        [HttpPost("LogIn")]
        public  IActionResult LogIn(LoginVM loginVM)
        {
            var user =  _userService.ValidateUser(loginVM.Email, loginVM.Password);
            try
            {      

                if (user == null)
                    return NotFound();
                else if (user != null && user.IsEmailVerified != true)
                    return Ok(new TokenVM()
                    {
                        UserId = user.UserId,
                        Email = user.Email,
                        Password = user.Password,
                        Name = user.Name,
                        Token = generateJwtToken(user),
                        Message = " هذا المستخدم مسجل من قبل ولكن غير مفعل  عن طريق البريد الالكترونى",
                        IsEmailVerified = user.IsEmailVerified ?? false,
                        CurrentSubscribtion = _subscribeRequestService.LastUserSubscribeRequestDate(user.UserId)

                    });
                else if (user.IsActive!=true) 
                    return Ok(new { message = " هذا المستخدم مسجل من قبل ولكن غير مفعل عن طريق الادمن" }); 
                else
                {
                    return Ok(new TokenVM()
                    {
                        UserId=user.UserId,
                        Email = user.Email,
                        Password = user.Password,
                        Name = user.Name,
                        Token = generateJwtToken(user),
                        Message = "تم تسجيل الدخول بنجاح",
                        IsEmailVerified=user.IsEmailVerified??false,
                        CurrentSubscribtion=_subscribeRequestService.LastUserSubscribeRequestDate(user.UserId)

                    }); ;
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }


        }
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(RegisterVM registerVM)
        {
            var user = _userService.ValidateUser(registerVM.Email, registerVM.Password);
            string OTP = new Random().Next(0000, 9999).ToString();
            try
            {
                if (user != null && user.IsEmailVerified != true)
                    return Ok(new { message = " هذا المستخدم مسجل من قبل ولكن عير مفعل  عن طريق البريد الالكترونى" });
                if (user !=null && user.IsActive != true)
                    return Ok(new{ message= "هذا المستخدم مسجل من قبل ولكن عير مفعل" });
               else if (user != null)
                    return Ok(new { message = "هذا المستخدم مسجل من قبل" });
               
                else
                {
                    
                    user = _userService.AddUser(new Model.User() {IsEmailVerified=false, IsActive = true, Email = registerVM.Email, Name = registerVM.Name, Password = registerVM.Password });              
                    await _emailSender.SendEmailAsync(user.Email, "كود التفعيل", createEmailBody("كود التفعيل", new EmailModel()
                    {
                        Subject = "",
                        UserName = user.Name,
                        code = OTP
                    }, ""));
                    return Ok(new TokenVM()
                    {
                        UserId = user.UserId,
                        Email = user.Email,
                        Password = user.Password,
                        Name = user.Name,
                        Token = generateJwtToken(user),
                        IsEmailVerified = user.IsEmailVerified ?? false,
                        Message ="تم التسجل بنجاح برجاء تفعيل الحساب",
                        OTP = OTP



                }); ;
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }


        }
        [HttpPost("SendOTP")]
        public async Task<IActionResult> SendOTP (int userId)
        {
            var user = _userService.GetUserData(userId);
            string OTP = new Random().Next(0000, 9999).ToString();
            try
            {
                if (user != null && user.IsEmailVerified != true)
                {
                    var Res = await _emailSender.SendEmailAsync(user.Email, "كود التفعيل", createEmailBody("كود التفعيل", new EmailModel()
                    {
                        Subject = "",
                        UserName = user.Name,
                        code = OTP
                    }, ""));
                    return Ok(new
                    {
                       
                        OTP = OTP,
                        Message = " تم ارسال كود التفعيل بنجاح برجاء مراجعة البريد الالكترونى"

                    });
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }


        }
        [HttpPost("AccountActivation")]
        public IActionResult AccountActivation(int userId)
        {
            var user = _userService.GetUserData(userId);
            try
            {
                if (user != null && user.IsEmailVerified != true)
                {
                    user.IsEmailVerified = true;
                    _userService.UpdateUser(user);
                    return Ok(new
                    {

                        Code = 1,
                        Message = "تم التفعيل بنجاح"

                    });
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }


        }
  
        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.UserId.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}
