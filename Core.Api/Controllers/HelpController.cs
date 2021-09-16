
using Core.Api.Helpers;
using Core.Model;
using Core.Service;
using Core.Service.Utilities;
using Microsoft.AspNetCore.Authorization;
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
    public class HelpController : BaseController
    {
        private readonly IServiceWrapper _serviceWrapper;
        public HelpController(IOptions<AppSettings> appSettings, IServiceWrapper serviceWrapper) : base(appSettings)
        {
            _serviceWrapper = serviceWrapper;

        }
        [Microsoft.AspNetCore.Authorization.Authorize]
        [HttpPost("AddHelp")]
        public IActionResult AddHelp(Help HelpVM)
        {
            try
            {
                HelpVM.DateCreated = DateTime.Now;
                HelpVM.UserId = CurrentUser;
                _serviceWrapper.helpService.AddHelp(HelpVM);
                return Ok(new
                {
                    message = "تم الاشتراك بنجاح"
                });

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }



        }
    }
}
