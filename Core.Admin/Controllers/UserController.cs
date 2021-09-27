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
        private readonly IServiceWrapper _serviceWrapper;
        public UserController(IServiceWrapper serviceWrapper)
        {
            _serviceWrapper = serviceWrapper;

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
            Users = _serviceWrapper.userService.GetAllUsers().ToPagedList(page, ItemPerPage);
            return PartialView("_ListUsers", Users);
        }
        [HttpPost]
        public IActionResult SearchUsers(string Name = "", string Email = "", int page = 1)

        {
            IPagedList<User> Users;
            ViewBag.type = 1;
            ViewBag.index = ItemPerPage * (page - 1) + 1;
            Users =  _serviceWrapper.userService.SearchInUsers(Name, Email,1).ToPagedList(page, ItemPerPage);
            return PartialView("_ListUsers", Users);
        }
        public IActionResult AddEdit(int? Id)
        {
            User model = new User() { IsActive = true, UserTypeId=1 };
            if (Id.HasValue && Id != 0)
                model =  _serviceWrapper.userService.GetUserData((int)Id);

            return View(model);
        }
        [HttpPost]
        public IActionResult AddEdit(User model)
        {
            if (!ModelState.IsValid)
                return View("AddEdit", model);

            try
            {
                if (model.UserId == 0)
                     _serviceWrapper.userService.AddUser(model);
                else
                {
                   
                     _serviceWrapper.userService.UpdateUser(model);
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
            var user =  _serviceWrapper.userService.GetUserData(id);
             _serviceWrapper.userService.DeleteUser(user);
          
            return Json("1");
        }
        public IActionResult ChangeStatus(int id)
        {
            var article =  _serviceWrapper.userService.GetUserData(id);
            article.IsActive = !(article.IsActive??false);
             _serviceWrapper.userService.UpdateUser(article);
           
            return Json("1");
        }


        public IActionResult SubscribedUsers()
        {
          
            return View();
        }

        public IActionResult ListSubscribedUsers(int page = 1)

        {
            IPagedList<User> SubscribedUsers;
            ViewBag.type = 1;
            ViewBag.index = ItemPerPage * (page - 1) + 1;
            ViewBag.Courses = _serviceWrapper.settingsService.GetAllCourse();
            SubscribedUsers = _serviceWrapper.userService.GetAllSubscribedUsers().ToPagedList(page, ItemPerPage);
            return PartialView("_ListSubscribedUsers", SubscribedUsers);
        }
        [HttpPost]
        public IActionResult SearchSubscribedUsers(string Name = "", string Email = "", int page = 1)

        {
            IPagedList<User> SubscribedUsers;
            ViewBag.type = 1;
            ViewBag.index = ItemPerPage * (page - 1) + 1;
            ViewBag.Courses = _serviceWrapper.settingsService.GetAllCourse();
            SubscribedUsers = _serviceWrapper.userService.SearchInSubscribedUsers(Name, Email, 1).ToPagedList(page, ItemPerPage);
            return PartialView("_ListSubscribedUsers", SubscribedUsers);
        }
        [HttpPost]
        public IActionResult SendNotification(int UserId,string NotificationText)
        {
      
            try
            {
                _serviceWrapper.notificationService.AddNotification(
                    new Notification() 
                    {
                        Body=NotificationText,
                        Title="تنبيه من الادمن",
                        DateCreated=DateTime.Now,
                        ToUserId=UserId
                    });

            }
            catch (Exception ex)
            {
                return Json(-1);

            }
            return Json(1);
        }
    }
}
