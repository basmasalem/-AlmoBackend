using Core.Api.Helpers;
using Core.Model;
using Core.Service;
using Core.Service.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProblemController : BaseController
    {
        private readonly IServiceWrapper _serviceWrapper;
        public ProblemController(IOptions<AppSettings> appSettings, IServiceWrapper serviceWrapper) : base(appSettings)
        {
            _serviceWrapper = serviceWrapper;

        }
        [Helpers.Authorize]
        [HttpPost("AddProblem")]
        public IActionResult AddProblem(Problem ProblemVM)
        {
            try
            {
                ProblemVM.DateCreated = DateTime.Now;
                ProblemVM.UserId = CurrentUser;
                _serviceWrapper.ProblemService.AddProblem(ProblemVM);
                string ImageName = "ProblemImage_" + ProblemVM.ProblemId;
                SaveImage(ProblemVM.Image,ImageName);
                ProblemVM.Image = ImageName;
                 _serviceWrapper.ProblemService.UpdateProblem(ProblemVM);
                return Ok(new
                {
                    message = "تم ارسال المشكلة بنجاح"
                });

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }



        }
    }
}
