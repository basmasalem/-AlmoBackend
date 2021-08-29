using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Admin.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public int ItemPerPage = 15;
 
    }
}
