using Core.Service;
using Core.Service.Utilities;
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

        private readonly IEmailSender _emailSender;

        public RequestController(IEmailSender emailSender, ISubscribeRequestService subscribeRequestService) : base(emailSender)
        {
            _subscribeRequestService = subscribeRequestService;

            _emailSender = emailSender;

        }
       
    }
}
