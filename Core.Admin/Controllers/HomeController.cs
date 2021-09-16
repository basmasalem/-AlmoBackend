using Core.Admin.Models;
using Core.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Admin.Controllers
{
    public class HomeController : BaseController
    {

        private readonly IServiceWrapper _serviceWrapper;
        public HomeController(IServiceWrapper serviceWrapper)
        {
            _serviceWrapper = serviceWrapper;

        }

        public IActionResult Index()
        {
            ViewBag.Users = _serviceWrapper.userService.GetAllUsers().Count();
            ViewBag.Requests = _serviceWrapper.subscribeRequestService.GetAllSubscribeRequests().Count();
            ViewBag.Earnings =  _serviceWrapper.subscribeRequestService.GetAllSubscribeRequests().Sum(s=>s.Cost);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
