using Core.Model;
using Core.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Core.Admin.Controllers
{
    public class RequestsController : BaseController
    {
        private readonly IServiceWrapper _serviceWrapper;
        public RequestsController(IServiceWrapper serviceWrapper)
        {
            _serviceWrapper = serviceWrapper;

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListRequests(int page = 1)

        {
            IPagedList<SubscribeRequest> SubscribeRequest;
            ViewBag.type = 1;
            ViewBag.index = ItemPerPage * (page - 1) + 1;
            SubscribeRequest = _serviceWrapper.subscribeRequestService.GetAllSubscribeRequests().ToPagedList(page, ItemPerPage);
            return PartialView("_ListRequests", SubscribeRequest);
        }
        [HttpPost]
        public IActionResult SearchRequests(DateTime? FromDate, DateTime? ToDate, string Name = "", string Email = "", int page = 1)

        {
            IPagedList<SubscribeRequest> SubscribeRequests;
            ViewBag.type = 1;
            ViewBag.index = ItemPerPage * (page - 1) + 1;
            SubscribeRequests =  _serviceWrapper.subscribeRequestService.SearchInSubscribeRequests(Name, Email,FromDate,ToDate).ToPagedList(page, ItemPerPage);
            return PartialView("_ListRequests", SubscribeRequests);
        }
        public IActionResult AddEdit(int? Id)
        {
            SubscribeRequest model = new SubscribeRequest() { };
            if (Id.HasValue && Id != 0)
                model =  _serviceWrapper.subscribeRequestService.GetSubscribeRequestData((int)Id);

            return View(model);
        }
        [HttpPost]
        public IActionResult AddEdit(SubscribeRequest model)
        {
            //validate Article  
            if (!ModelState.IsValid)
                return View("AddEdit", model);

            //save Article into database   
            try
            {
                if (model.UserId == 0)
                     _serviceWrapper.subscribeRequestService.AddSubscribeRequest(model);
                else
                {

                     _serviceWrapper.subscribeRequestService.UpdateSubscribeRequest(model);
                }


            }
            catch (Exception ex)
            {
                return Json(-1);

            }
            return Json(1);
        }
        public IActionResult DeleteRequest(int id)
        {
            var subscribeRequest =  _serviceWrapper.subscribeRequestService.GetSubscribeRequestData(id);
             _serviceWrapper.subscribeRequestService.DeleteSubscribeRequest(subscribeRequest);

            return Json("1");
        }
        public IActionResult ChangeStatus(int id)
        {
            var subscribeRequest =  _serviceWrapper.subscribeRequestService.GetSubscribeRequestData(id);
            subscribeRequest.IsActive = !(subscribeRequest.IsActive ?? false);
             _serviceWrapper.subscribeRequestService.UpdateSubscribeRequest(subscribeRequest);

            return Json("1");
        }
    }
}
