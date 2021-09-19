using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;



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
        public string SendNotfiction(string DeviceToken, string title, string msg, string url = "")

        {

            if (!string.IsNullOrEmpty(DeviceToken))
            {
                string serverKey = "AAAAGZGxIxU:APA91bFsePaVJC1UJkZCNRcsZ4TTIBbYOZoM7JUiiQDJpIpU4T55iC2tDWeWfIYGrL2HYKlbXC9j4KE8tJfKIwWeHe7DpO83RPyWvXRmOfAKTN9M6v-ut66PvCFdmjr6GS3yhXEUpCs0";
                string senderId = "109818487573";
                string webAddr = "https://fcm.googleapis.com/fcm/send";
                var result = "-1";
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Headers.Add(string.Format("Authorization: key={0}", serverKey));
                httpWebRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                httpWebRequest.Method = "POST";

                var payload = new
                {
                    to = DeviceToken,
                    priority = "high",
                    content_available = true,
                    notification = new
                    {
                        body = msg,
                        title = title,
                        click_action = "FCM_PLUGIN_ACTIVITY",  //Must be present for Android
         
                    },
                    data = new
                    {
                        param1 = url

                    },
                    android = new
                    {
                        notification = new
                        {
                            sound = "default",
                            click_action = "FCM_PLUGIN_ACTIVITY",
              
                        }
                    }
                };
                var serializer = new JavaScriptSerializer();
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = serializer.Serialize(payload);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
                return result;
            }
            else
                return null;

        }

    }
}
