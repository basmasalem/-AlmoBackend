using Core.Api.Helpers;
using Core.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NotificationController : BaseController
    {
        private readonly IServiceWrapper _serviceWrapper;
        public NotificationController(IOptions<AppSettings> appSettings, IServiceWrapper serviceWrapper) : base(appSettings)
        {
            _serviceWrapper = serviceWrapper;
        

        }
        [Helpers.Authorize]
        [HttpGet("GetUserNotifcations")]
        public IActionResult GetUserNotification()
        {
            try
            {

                return Ok( _serviceWrapper.notificationService.GetAllNotifications(CurrentUser).Select(n=>new {
                    NotificationId=n.NotificationId,
                    IsRead=n.IsRead,
                    DateCreated=n.DateCreated,
                    Title=n.Title,
                    Body=n.Body
                })) ;

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }



        }
    }
}
