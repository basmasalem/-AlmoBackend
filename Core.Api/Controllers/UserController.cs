using Core.Api.Helpers;
using Core.Api.ViewModels;
using Core.Service;
using Core.Service.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Api.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IServiceWrapper _serviceWrapper;
        public UserController(IOptions<AppSettings> appSettings, IServiceWrapper serviceWrapper) : base(appSettings)
        {
            _serviceWrapper = serviceWrapper;

        }
        [Microsoft.AspNetCore.Authorization.Authorize]
        [HttpPost("UpdateProfile")]
        public IActionResult UpdateProfile(UpdatePasswordView UpdatePasswordVM)
        {
            var user = _serviceWrapper.userService.GetUserData(CurrentUser);
            try
            {
                if(UpdatePasswordVM.OldPassword!=user.Password)
                {
                    return Ok(new { message = "الرقم السرى عير متطابق مع المستخدم" });
                }
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
                        CurrentSubscribtion = _serviceWrapper.subscribeRequestService.LastUserSubscribeRequestDate(user.UserId)

                    });
                else if (user.IsActive != true)
                    return Ok(new { message = " هذا المستخدم مسجل من قبل ولكن غير مفعل عن طريق الادمن" });
                else
                {
                    user.Name = UpdatePasswordVM.Name;
                    user.Password = UpdatePasswordVM.Password;
                    _serviceWrapper.userService.UpdateUser(user);
                    return Ok(new TokenVM()
                    {
                        UserId = user.UserId,
                        Email = user.Email,
                        Password = user.Password,
                        Name = user.Name,
                        Token = generateJwtToken(user),
                        Message = "تم تعديل بنجاح",
                        IsEmailVerified = user.IsEmailVerified ?? false,
                        CurrentSubscribtion =  _serviceWrapper.subscribeRequestService.LastUserSubscribeRequestDate(user.UserId)

                    }); ;
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }


        }
    }
}
