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
        private readonly IProblemService _problemsService;
        public ProblemsController(IProblemService problemsService)
        {
            _problemsService = problemsService;

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
            Problems = _problemsService.GetAllProblems().ToPagedList(page, ItemPerPage);
            return PartialView("_ListProblems", Problems);
        }
        [HttpPost]
        public IActionResult SearchProblems(string Name = "", string Email = "", int page = 1)
        {
            IPagedList<Problem> Problemss;
            ViewBag.type = 1;
            ViewBag.index = ItemPerPage * (page - 1) + 1;
            Problemss = _problemsService.GetAllProblems(Name, Email).ToPagedList(page, ItemPerPage);
            return PartialView("_ListProblems", Problemss);
        }
        public IActionResult AddEdit(int? Id)
        {
            Problem model = new Problem() { };
            if (Id.HasValue && Id != 0)
                model = _problemsService.GetProblemData((int)Id);

            return View(model);
        }
        public IActionResult DeleteProblem(int id)
        {
            var problem = _problemsService.GetProblemData(id);
            _problemsService.DeleteProblem(problem);

            return Json("1");
        }
    }
}
