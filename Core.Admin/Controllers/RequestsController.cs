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
        private readonly ISubscribeRequestService _subscribeRequestService;
        public RequestsController(ISubscribeRequestService subscribeRequestService)
        {
            _subscribeRequestService = subscribeRequestService;

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
            SubscribeRequest = _subscribeRequestService.GetAllSubscribeRequests().ToPagedList(page, ItemPerPage);
            return PartialView("_ListRequests", SubscribeRequest);
        }
        [HttpPost]
        public IActionResult SearchRequests(string Name = "", string Email = "", int page = 1)

        {
            IPagedList<SubscribeRequest> SubscribeRequests;
            ViewBag.type = 1;
            ViewBag.index = ItemPerPage * (page - 1) + 1;
            SubscribeRequests = _subscribeRequestService.SearchInSubscribeRequests(Name, Email).ToPagedList(page, ItemPerPage);
            return PartialView("_ListUsers", SubscribeRequests);
        }
        public IActionResult AddEdit(int? Id)
        {
            SubscribeRequest model = new SubscribeRequest() { };
            if (Id.HasValue && Id != 0)
                model = _subscribeRequestService.GetSubscribeRequestData((int)Id);

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
                    _subscribeRequestService.AddSubscribeRequest(model);
                else
                {

                    _subscribeRequestService.UpdateSubscribeRequest(model);
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
            var subscribeRequest = _subscribeRequestService.GetSubscribeRequestData(id);
            _subscribeRequestService.DeleteSubscribeRequest(subscribeRequest);

            return Json("1");
        }
        public IActionResult ChangeStatus(int id)
        {
            var subscribeRequest = _subscribeRequestService.GetSubscribeRequestData(id);
            subscribeRequest.IsActive = !(subscribeRequest.IsActive ?? false);
            _subscribeRequestService.UpdateSubscribeRequest(subscribeRequest);

            return Json("1");
        }
    }
}
