using Core.Model;
using Core.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Admin.Controllers
{
    public class SettingsController : Controller
    {
        private readonly IServiceWrapper _serviceWrapper;
        public SettingsController(IServiceWrapper serviceWrapper)
        {
            _serviceWrapper = serviceWrapper;

        }
        public IActionResult Index()
        {
            return View(_serviceWrapper.settingsService.GetSettingsData()??new Settings());
        }
        [HttpPost]
        public IActionResult AddEdit(Settings model)
        {
       
            if (!ModelState.IsValid)
                return View("AddEdit", model);
     
            try
            {
                if (model.SettingsId == 0)
                    _serviceWrapper.settingsService.AddSettings(model);
                else
                {

                    _serviceWrapper.settingsService.UpdateSettings(model);
                }

            }
            catch (Exception ex)
            {
                return Json(-1);

            }
            return Json(1);
        }
    }
}
