using Core.Service.Utilities;
using Core.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Api.Controllers
{
  
    public class BaseController : ControllerBase
    {
    
     
        private readonly IEmailSender _emailSender;

        [Obsolete]
        public BaseController(IEmailSender _emailSender)
        {
            this._emailSender = _emailSender;
     
        }
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


        public string createEmailBody(string title, EmailModel model, string page)
        {

            string body = string.Empty;
            body = model.code; 
            //var file = System.IO.Path.Combine(server, page);
            ////using streamreader for reading my htmltemplate   
            //using (StreamReader reader = new StreamReader(file))
            //{
            //    body = reader.ReadToEnd();
            //}

            //body = body.Replace("{UserName}", model.UserName); //replacing the required things  

            //body = body.Replace("{Title}", title);

            //body = body.Replace("{message}", model.Body);

            //body = body.Replace("{Subject}", model.Subject);
            //body = body.Replace("{StartTime}", model.StartTime);
            //body = body.Replace("{EndTime}", model.EndTime);



            return body;

        }
    }
}
