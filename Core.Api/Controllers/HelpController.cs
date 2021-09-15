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
    public class HelpController : BaseController
    {
        private readonly IHelpService _helpService;
        private readonly IEmailSender _emailSender;

        public HelpController(ISettingsService settingsService, IEmailSender emailSender, IHelpService helpService) : base(emailSender)
        {
            _helpService = helpService;
            _emailSender = emailSender;

        }
        [Authorize]
        [HttpPost("AddHelp")]
        public IActionResult AddHelp(Help HelpVM)
        {
            try
            {
                HelpVM.DateCreated = DateTime.Now;
                HelpVM.UserId = CurrentUser;
                _helpService.AddHelp(HelpVM);
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
