
using Core.Api.ViewModels;
using Core.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ISubscribeRequestService _subscribeRequestService;
        public AccountController(IUserService userService, ISubscribeRequestService subscribeRequestService)
        {
            _userService = userService;
            _subscribeRequestService = subscribeRequestService;

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
                    return Ok(new { message = " هذا المستخدم مسجل من قبل ولكن عير مفعل  عن طريق البريد الالكترونى" });
                else if (user.IsActive!=true) 
                    return Ok(new { message = "هذا المستخدم مسجل من قبل ولكن عير مفعل" }); 
                else
                {
                    return Ok(new TokenVM()
                    {
                        UserId=user.UserId,
                        Email = user.Email,
                        Password = user.Password,
                        Name = user.Name,
                        Token = GenerateToken(user.Email, user.Password),
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
        public IActionResult Register(RegisterVM registerVM)
        {
            var user = _userService.ValidateUser(registerVM.Email, registerVM.Password);
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
                    return Ok(new TokenVM()
                    {
                        UserId = user.UserId,
                        Email = user.Email,
                        Password = user.Password,
                        Name = user.Name,
                        Token = GenerateToken(user.Email, user.Password),
                        IsEmailVerified = user.IsEmailVerified ?? false,
                        Message ="تم التسجل بنجاح برجاء تفعيل الحساب"

                    }); ;
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
            var user = _userService.GetUserDate(userId);
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
        public string GenerateToken(string UserName, string Password)
        {

            return Base64Encode(UserName + ":" + Password + ":" + DateTime.Now.AddDays(7).ToShortDateString());
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}
