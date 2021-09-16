using Core.Api.Helpers;
using Core.Api.ViewModels;
using Core.Model;
using Core.Service;
using Core.Service.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;

namespace Core.Api.Controllers
{
   
    [Route("[controller]")]
    [ApiController]
    public class RequestController : BaseController
    {
        private readonly IServiceWrapper _serviceWrapper;
        public RequestController(IOptions<AppSettings> appSettings, IServiceWrapper serviceWrapper) : base(appSettings)
        {
            _serviceWrapper = serviceWrapper;

        }
        [Microsoft.AspNetCore.Authorization.Authorize]
        [HttpPost("AddRequest")]
        public IActionResult AddRequest(RequestVM requestVM)
        {
            try
            {
                Settings settings = _serviceWrapper.settingsService.GetSettingsData();
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
                if(_serviceWrapper.subscribeRequestService.CheckRequest(CurrentUser, subscribeRequest.FromDate,subscribeRequest.ToDate)!=null)
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
                    _serviceWrapper.subscribeRequestService.AddSubscribeRequest(subscribeRequest);
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
