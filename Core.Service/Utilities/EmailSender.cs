
//using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Service.Utilities
{
    public class EmailSender: IEmailSender
    {


        public EmailSender()
        {
           
        }

        //public AuthMessageSenderOptions Options { get; } //set only via Secret Manager

        public Task<Response> SendEmailAsync(string email, string subject, string message)
        {
            return Execute( subject, message, email);
        }

        public Task <Response> Execute( string subject, string message, string email)
        {
            var client = new SendGridClient("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");
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("basma.ideasgate@gmail.com", "AlmoApp"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));


            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }


    }
}
