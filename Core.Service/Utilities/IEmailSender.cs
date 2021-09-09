using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Service.Utilities
{
    public interface IEmailSender
    {

        Task <Response> SendEmailAsync(string email, string subject, string htmlMessage);


    }
}
