using Core.Model;
using Core.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Core.Admin.Controllers
{
    public class ProblemsController : BaseController
    {
        private readonly IServiceWrapper _serviceWrapper;
        public ProblemsController(IServiceWrapper serviceWrapper)
        {
            _serviceWrapper = serviceWrapper;

        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ListProblems(int page = 1)
        {
            IPagedList<Problem> Problems;
            ViewBag.type = 1;
            ViewBag.index = ItemPerPage * (page - 1) + 1;
            Problems = _serviceWrapper.ProblemService.GetAllProblems().ToPagedList(page, ItemPerPage);
            return PartialView("_ListProblems", Problems);
        }
        [HttpPost]
        public IActionResult SearchProblems(string Name = "", string Email = "", int page = 1)
        {
            IPagedList<Problem> Problemss;
            ViewBag.type = 1;
            ViewBag.index = ItemPerPage * (page - 1) + 1;
            Problemss =  _serviceWrapper.ProblemService.GetAllProblems(Name, Email).ToPagedList(page, ItemPerPage);
            return PartialView("_ListProblems", Problemss);
        }
        public IActionResult AddEdit(int? Id)
        {
            Problem model = new Problem() { };
            if (Id.HasValue && Id != 0)
                model =  _serviceWrapper.ProblemService.GetProblemData((int)Id);

            return View(model);
        }
        public IActionResult DeleteProblem(int id)
        {
            var problem =  _serviceWrapper.ProblemService.GetProblemData(id);
             _serviceWrapper.ProblemService.DeleteProblem(problem);

            return Json("1");
        }
    }
}
