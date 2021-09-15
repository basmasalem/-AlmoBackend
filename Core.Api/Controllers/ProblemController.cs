﻿using Core.Model;
using Core.Service;
using Core.Service.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IProblemService _ProblemService;
        private readonly IEmailSender _emailSender;

        public ProblemController( IEmailSender emailSender, IProblemService ProblemService) : base(emailSender)
        {
            _ProblemService = ProblemService;
            _emailSender = emailSender;

        }
        [Authorize]
        [HttpPost("AddProblem")]
        public IActionResult AddProblem(Problem ProblemVM)
        {
            try
            {
                ProblemVM.DateCreated = DateTime.Now;
                ProblemVM.UserId = CurrentUser;
                _ProblemService.AddProblem(ProblemVM);
                var bytes = Convert.FromBase64String(ProblemVM.Image);// a.base64image 
                                                                  //or full path to file in temp location
                                                                  //var filePath = Path.GetTempFileName();

                // full path to file in current project location
                string filedir = Path.Combine(Directory.GetCurrentDirectory(), "Attachments");
                Debug.WriteLine(filedir);
                Debug.WriteLine(Directory.Exists(filedir));
                if (!Directory.Exists(filedir))
                { //check if the folder exists;
                    Directory.CreateDirectory(filedir);
                }
                string file = Path.Combine(filedir, ProblemVM.ProblemId+".jpg");
                
                if (bytes.Length > 0)
                {
                    using (var stream = new FileStream(file, FileMode.Create))
                    {
                        stream.Write(bytes, 0, bytes.Length);
                        stream.Flush();
                    }
                }
                ProblemVM.Image = ProblemVM.ProblemId + ".jpg";
                _ProblemService.UpdateProblem(ProblemVM);
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