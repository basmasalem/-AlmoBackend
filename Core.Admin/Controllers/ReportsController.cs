using Core.Model;
using Core.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Admin.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IServiceWrapper _serviceWrapper;
        public ReportsController(IServiceWrapper serviceWrapper)
        {
            _serviceWrapper = serviceWrapper;

        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ListReports(int page = 1)
        {   
          List<SubscribeRequest>  SubscribeReport = _serviceWrapper.subscribeRequestService.GetAllSubscribeRequests();
            return PartialView("_ListReports", SubscribeReport);
        }
        [HttpPost]
        public IActionResult SearchReports(DateTime? FromDate,DateTime? ToDate, string Name = "", string Email = "")

        {
            List<SubscribeRequest> SubscribeReports;          
            SubscribeReports = _serviceWrapper.subscribeRequestService.SearchInSubscribeRequests(Name, Email, FromDate, ToDate);
            return PartialView("_ListReports", SubscribeReports);
        }
    }
}
