using Core.Api.Helpers;
using Core.Api.ViewModels;
using Core.Model;
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
        private readonly AppSettings _appSettings;
        public UserController(IOptions<AppSettings> appSettings, IServiceWrapper serviceWrapper) : base(appSettings)
        {
            _serviceWrapper = serviceWrapper;
            _appSettings = appSettings.Value;

        }
        [Helpers.Authorize]
        [HttpPost("UpdateProfile")]
        public IActionResult UpdateProfile(UpdatePasswordView UpdatePasswordVM)
        {
            var user = _serviceWrapper.userService.GetUserData(CurrentUser);
            try
            {
                if (UpdatePasswordVM.OldPassword != user.Password)
                {
                    return Ok(new { message = "الرقم السرى عير متطابق مع المستخدم" });
                }
                if (user == null)
                    return NotFound();
                else return CheckUserValidateStatus( UpdatePasswordVM);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }


        }

        public IActionResult CheckUserValidateStatus(UpdatePasswordView UpdatePasswordVM)
        {
            var user = _serviceWrapper.userService.GetUserData(CurrentUser);
            if (user != null && user.IsEmailVerified != true)
                return Ok(new TokenVM()
                {
                    UserId = user.UserId,
                    Email = user.Email,
                    Password = user.Password,
                    Name = user.Name,
                    Token = generateJwtToken(user),
                    ImgaePath = _appSettings.ImagePath + user.Image + ".jpg",
                    Message = " هذا المستخدم مسجل من قبل ولكن غير مفعل  عن طريق البريد الالكترونى",
                    IsEmailVerified = user.IsEmailVerified ?? false,
                    CurrentSubscribtion = _serviceWrapper.subscribeRequestService.LastUserSubscribeRequestDate(user.UserId)

                });
            else if (user.IsActive != true)
                return Ok(new { message = " هذا المستخدم مسجل من قبل ولكن غير مفعل عن طريق الادمن" });
            else return SuccessedUserValidateStatus( UpdatePasswordVM);
        }

        public IActionResult SuccessedUserValidateStatus( UpdatePasswordView UpdatePasswordVM)
        {
            var user = _serviceWrapper.userService.GetUserData(CurrentUser);
            string userImage = "UserImage_" + user.UserId;
            SaveImage(UpdatePasswordVM.Image, userImage);
            user.Name = UpdatePasswordVM.Name;
            user.Password = UpdatePasswordVM.Password;
            user.Image = userImage;
            _serviceWrapper.userService.UpdateUser(user);
            return Ok(new TokenVM()
            {
                UserId = user.UserId,
                Email = user.Email,
                Password = user.Password,
                Name = user.Name,
                ImgaePath = _appSettings.ImagePath + user.Image + ".jpg",
                Token = generateJwtToken(user),
                Message = "تم تعديل بنجاح",
                IsEmailVerified = user.IsEmailVerified ?? false,
                CurrentSubscribtion = _serviceWrapper.subscribeRequestService.LastUserSubscribeRequestDate(user.UserId)

            });
        }



        [Helpers.Authorize]
        [HttpPost("UpdateUserLevel")]
        public ActionResult UpdateUserLevel(int ClassNumber, int  CourseId)
        {
            try
            {

                var user = _serviceWrapper.userService.GetUserData(CurrentUser);
                user.CourseId = CourseId;
                user.ClassNumber = ClassNumber;
                _serviceWrapper.userService.UpdateUser(user);
                return Ok(new ResulteModel()
                {
                    Code = 1,
                    Message = "تم تعديل المستوى بنجاح"

                });
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }


        }
    }
}
