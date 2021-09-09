using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Service
{
   
    public interface ISharedService
    {
        public string SendMAil();
    }
    public class SharedService : ISharedService
    {
        public string SendMAil()
        {
          
            return "";
        }
      
    }
}
