using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newestTool.helper
{
    internal class convertToCookieHelper
    {
        public static Cookie convertToCookie(string cookie)
        {
            return new Cookie("ASP.NET_SessionId", cookie);
        }
    }
}
