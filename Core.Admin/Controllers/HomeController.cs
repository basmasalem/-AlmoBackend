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
            ViewBag.UsersLast24hr = _serviceWrapper.userService.GetAllUsers().Where(c=>c.ReservationDate.Value >=DateTime.Now.AddHours(-24)).Count();
            ViewBag.UaersAvarageInDay = (int)_serviceWrapper.userService.GetAllUsers().GroupBy(d=>d.ReservationDate.Value.Date).Select(s => s.Count()).Average();
            ViewBag.RequestAvarageInDay = (int)_serviceWrapper.subscribeRequestService.GetAllSubscribeRequests().GroupBy(d => d.DateCreated.Value.Date).Select(s => s.Count()).Average();
            ViewBag.Requests = _serviceWrapper.subscribeRequestService.GetAllSubscribeRequests().Count();
            ViewBag.NewRequests = _serviceWrapper.subscribeRequestService.GetAllSubscribeRequests().Where(c => c.DateCreated.Value >= DateTime.Now.AddHours(-24)).Count();
            ViewBag.Earnings =  _serviceWrapper.subscribeRequestService.GetAllSubscribeRequests().Sum(s=>s.Cost);
            ViewBag.Problems =  _serviceWrapper.ProblemService.GetAllProblems().Count();
            ViewBag.Messages = _serviceWrapper.helpService.GetAllHelps().Count();
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
