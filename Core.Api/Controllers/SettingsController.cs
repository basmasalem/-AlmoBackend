using Core.Api.Helpers;
using Core.Service;
using Core.Service.Utilities;
using Core.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace Core.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class SettingsController : BaseController
    {
        private readonly IServiceWrapper _serviceWrapper;
        public SettingsController(IOptions<AppSettings> appSettings, IServiceWrapper serviceWrapper) : base(appSettings)
        {
            _serviceWrapper = serviceWrapper;

        }

        [HttpGet]
        public IActionResult Content()
        {
            try
            {
                var obj = _serviceWrapper.settingsService.GetSettingsData();
                if(!string.IsNullOrEmpty(obj.PrivacyPolcy))
                obj.PrivacyPolcy = Regex.Replace(obj.PrivacyPolcy, "<.*?>", String.Empty);
                if (!string.IsNullOrEmpty(obj.StudyPlan))
                    obj.StudyPlan = Regex.Replace(obj.StudyPlan, "<.*?>", String.Empty);
                if (!string.IsNullOrEmpty(obj.TermsAndConditions))
                    obj.TermsAndConditions = Regex.Replace(obj.TermsAndConditions, "<.*?>", String.Empty);
                if (!string.IsNullOrEmpty(obj.AboutApp))
                    obj.AboutApp = Regex.Replace(obj.AboutApp, "<.*?>", String.Empty);
                if (!string.IsNullOrEmpty(obj.Credits))
                    obj.Credits = Regex.Replace(obj.Credits, "<.*?>", String.Empty);
            
                return Ok( new { 
                        TermsAndConditions=obj.TermsAndConditions, 
                        PrivacyPolcy=obj.PrivacyPolcy,                  
                        StudyPlan=obj.StudyPlan, 
                        AboutApp=obj.AboutApp, 
                        Credits=obj.Credits });
            }
            catch (Exception ex)
            {
              
                return StatusCode(500,ex.Message);
            }
        }

        [HttpGet("Packages")]

        public IActionResult Packages()
        {
            try
            {
                var obj = _serviceWrapper.settingsService.GetSettingsData();
              
                return Ok(new List<PackagesVM>() {
                    new PackagesVM() {Code = 1,Title = "OneMonthCost", Cost=obj.OneMonthCost },
                    new PackagesVM() {Code = 2,Title = "ThreeMonthsCost", Cost = obj.ThreeMonthsCost } 
                });
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }
    }
}
