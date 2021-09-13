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
        public int CurrentUser
        {
            get
            {
                if (User.Identity.IsAuthenticated)
                {
                    var claims = User.Claims;
                    return

                        int.Parse(claims?.FirstOrDefault(x => x.Type.Equals("id", StringComparison.OrdinalIgnoreCase))?.Value);
                    
                }
                else
                {
                    return 0;
                }
            }
        }

    }
}
