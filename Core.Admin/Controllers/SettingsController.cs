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
        private readonly ISettingsService _SettingsService;
        public SettingsController(ISettingsService settingsService)
        {
            _SettingsService = settingsService;

        }
        public IActionResult Index()
        {
            return View(_SettingsService.GetSettingsData()??new Settings());
        }
        [HttpPost]
        public IActionResult AddEdit(Settings model)
        {
       
            if (!ModelState.IsValid)
                return View("AddEdit", model);
     
            try
            {
                if (model.SettingsId == 0)
                    _SettingsService.AddSettings(model);
                else
                {

                    _SettingsService.UpdateSettings(model);
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
