using Core.Api.Helpers;
using Core.Model;
using Core.Service;
using Core.Service.Utilities;
using Core.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Api.Controllers
{
  
    public class BaseController : ControllerBase
    {


        private readonly AppSettings _appSettings;
        
        public BaseController(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
   

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
        public string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.UserId.ToString()), new Claim("password", user.Password.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public void SaveImage(string Image,string  Name)
        {
            var bytes = Convert.FromBase64String(Image);// a.base64image 
                                                                  //or full path to file in temp location
                                                                  //var filePath = Path.GetTempFileName();

            // full path to file in current project location
            string filedir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Attachments");
            Debug.WriteLine(filedir);
            Debug.WriteLine(Directory.Exists(filedir));
            if (!Directory.Exists(filedir))
            { //check if the folder exists;
                Directory.CreateDirectory(filedir);
            }
            string file = Path.Combine(filedir, Name + ".jpg");
            if (System.IO.File.Exists(file))
                System.IO.File.Delete(file);
            if (bytes.Length > 0)
            {
                using (var stream = new FileStream(file, FileMode.Create))
                {
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Flush();
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
