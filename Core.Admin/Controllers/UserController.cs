using Core.Model;
using Core.Service;
using Microsoft.AspNetCore.Mvc;
using  X.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Admin.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListUsers(int page = 1)
        
        {
            IPagedList<User> Users;
            ViewBag.type = 1;
            ViewBag.index = ItemPerPage * (page - 1) + 1;
            Users = _userService.GetAllUsers().ToPagedList(page, ItemPerPage);
            return PartialView("_ListUsers", Users);
        }
        [HttpPost]
        public IActionResult SearchUsers(string Name = "", string Email = "", int page = 1)

        {
            IPagedList<User> Users;
            ViewBag.type = 1;
            ViewBag.index = ItemPerPage * (page - 1) + 1;
            Users = _userService.SearchInUsers(Name, Email).ToPagedList(page, ItemPerPage);
            return PartialView("_ListUsers", Users);
        }
        public IActionResult AddEdit(int? Id)
        {
            User model = new User() { IsActive = true, UserTypeId=1 };
            if (Id.HasValue && Id != 0)
                model = _userService.GetUserData((int)Id);

            return View(model);
        }
        [HttpPost]
        public IActionResult AddEdit(User model)
        {
            //validate Article  
            if (!ModelState.IsValid)
                return View("AddEdit", model);

            //save Article into database   
            try
            {
                if (model.UserId == 0)
                    _userService.AddUser(model);
                else
                {
                   
                    _userService.UpdateUser(model);
                }

               
            }
            catch (Exception ex)
            {
                return Json(-1);

            }
            return Json(1);
        }
        public IActionResult DeleteUser(int id)
        {
            var user = _userService.GetUserData(id);
            _userService.DeleteUser(user);
          
            return Json("1");
        }
        public IActionResult ChangeStatus(int id)
        {
            var article = _userService.GetUserData(id);
            article.IsActive = !(article.IsActive??false);
            _userService.UpdateUser(article);
           
            return Json("1");
        }
    }
}
