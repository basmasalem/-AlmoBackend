using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ViewModel
{
   public class EmailModel
    {
      public  string code { get; set; }
        public string UserName { get; set; }
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
