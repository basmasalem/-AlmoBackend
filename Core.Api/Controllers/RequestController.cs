using Core.Api.ViewModels;
using Core.Model;
using Core.Service;
using Core.Service.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Api.Controllers
{
   
    [Route("[controller]")]
    [ApiController]
    public class RequestController : BaseController
    {
        private readonly ISubscribeRequestService _subscribeRequestService;
        private readonly ISettingsService _settingsService;

        private readonly IEmailSender _emailSender;

        public RequestController(ISettingsService settingsService,IEmailSender emailSender, ISubscribeRequestService subscribeRequestService) : base(emailSender)
        {
            _subscribeRequestService = subscribeRequestService;
            _settingsService = settingsService;
            _emailSender = emailSender;

        }
        [Authorize]
        [HttpPost("AddRequest")]
        public IActionResult AddRequest(RequestVM requestVM)
        {
            try
            {
                Settings settings = _settingsService.GetSettingsData();
                int monthesNo = requestVM.PackageCode == 1 ? 1 : 3;
                SubscribeRequest subscribeRequest = new SubscribeRequest()
                {
                    FromDate = DateTime.Now,
                    ToDate = DateTime.Now.AddMonths(monthesNo),
                    Cost = requestVM.PackageCode == 1 ? settings.OneMonthCost : settings.ThreeMonthsCost,
                    Period = monthesNo,
                    DateCreated = DateTime.Now,
                    UserCardCVV = requestVM.UserCardCVV,
                    UserCardNumber = requestVM.UserCardNumber,
                    IsActive = true,
                    UserId=CurrentUser
                };
                if(_subscribeRequestService.CheckRequest(CurrentUser, subscribeRequest.FromDate,subscribeRequest.ToDate)!=null)
                {
                    return Ok(new
                    {
                        FromDate = subscribeRequest.FromDate,
                        ToDate = subscribeRequest.ToDate,
                        message = "يوجد اشتراك فى هذا التوقيت"



                    });
                }
                else
                {
                    _subscribeRequestService.AddSubscribeRequest(subscribeRequest);
                    return Ok(new
                    {
                        FromDate = subscribeRequest.FromDate,
                        ToDate = subscribeRequest.ToDate,
                        message = "تم الاشتراك بنجاح"



                    });
                }
               
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            


        }
    }
}
