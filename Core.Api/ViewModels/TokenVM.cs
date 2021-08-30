using Core.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Api.ViewModels
{
    public class TokenVM 
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public bool IsEmailVerified { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
        public SubscribeRequestVM CurrentSubscribtion { get; set; }
    }
}
