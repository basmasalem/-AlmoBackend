using Core.Admin.Models.ViewModels;
using Core.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Core.Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly IServiceWrapper _serviceWrapper;
        public AccountController(IServiceWrapper serviceWrapper)
        {
            _serviceWrapper = serviceWrapper;

        }
        public IActionResult LogIn()
        {
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
        [HttpPost]
        public IActionResult Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var user = _serviceWrapper.userService.ValidateLogedUser(model.Email, model.Password,0,"").UserData;
                if (user != null)
                {
                    var userClaims = new List<Claim>()
                    {
                        new Claim("UserId",user.UserId.ToString()),
                        new Claim(ClaimTypes.Name,user.Name),                       
                        new Claim(ClaimTypes.Email,user.Email),
                    };

                    var userIdentity = new ClaimsIdentity(userClaims, "User Identity");
                    var userPrincipal = new ClaimsPrincipal(new[] { userIdentity });
                    HttpContext.SignInAsync(userPrincipal);

                    return Json(1);
                }
                else
                {
                    return Json(-1);
                }

            }
            return Json(-1);
        }
    }
}
