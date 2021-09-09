using Core.Service;
using Core.Service.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SettingsController : BaseController
    {
        private readonly ISettingsService _settingsService;

        private readonly IEmailSender _emailSender;

        public SettingsController(IEmailSender emailSender, ISettingsService settingsService) : base(emailSender)
        {
            _settingsService = settingsService;
          
            _emailSender = emailSender;

        }
        [HttpGet("PrivacyPolcy")]
      
        public IActionResult PrivacyPolcy()
        {
            try
            {
                var obj = _settingsService.GetSettingsData();
                if(!string.IsNullOrEmpty(obj.PrivacyPolcy))
                obj.PrivacyPolcy = Regex.Replace(obj.PrivacyPolcy, "<.*?>", String.Empty);
                return Ok(obj.PrivacyPolcy);
            }
            catch (Exception ex)
            {
              
                return StatusCode(500,ex.Message);
            }
        }
        [HttpGet("TermsAndConditions")]

        public IActionResult TermsAndConditions()
        {
            try
            {
                var obj = _settingsService.GetSettingsData();
                if (!string.IsNullOrEmpty(obj.TermsAndConditions))
                    obj.TermsAndConditions = Regex.Replace(obj.TermsAndConditions, "<.*?>", String.Empty);
                return Ok(obj.TermsAndConditions);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }
    }
}
